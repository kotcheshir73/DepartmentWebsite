using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using TicketModels.Enums;
using TicketModels.Models;
using TicketServiceImplementations.Helpers;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceImplementations.Implementations
{
    public class TicketProcess : ITicketProcess
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        public ResultService LoadQuestions(TicketProcessLoadQuestionsBindingModel model)
        {
            try
            {
                List<string> questions = new List<string>();
                using (StreamReader reader = new StreamReader(model.FileName))
                {
                    string str;
                    while ((str = reader.ReadLine()) != null)
                    {
                        questions.Add(str);
                    }
                }

                if (questions.Count > 0)
                {
                    int counter = 0;
                    using (var context = new DepartmentDbContext())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var question in questions)
                                {
                                    context.ExaminationTemplateBlockQuestions.Add(
                                        TicketModelFacotryFromBindingModel.CreateExaminationTemplateBlockQuestion(new ExaminationTemplateBlockQuestionSetBindingModel
                                        {
                                            ExaminationTemplateBlockId = model.ExaminationTemplateBlockId,
                                            QuestionText = question,
                                            QuestionNumber = counter++
                                        }));
                                    context.SaveChanges();
                                }

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                return ResultService.Error(ex, ResultServiceStatusCode.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }

            return ResultService.Success();
        }

        public ResultService<TicketTemplateViewModel> LoadTemplate(TicketProcessLoadTemplateBindingModel model)
        {
            if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
            {
                throw new Exception("Нет доступа на чтение данных по записям {0}");
            }
            // переводим doc-файл в формат xml и обрабатываем его
            object missing = System.Reflection.Missing.Value;
            Application wordApp = new Application
            {
                Visible = false,
                ScreenUpdating = false
            };

            Object xmlFormat = WdSaveFormat.wdFormatXML;
            Object docFile = model.FileName;
            string fileXML = model.FileName + ".xml";
            Object xmlFile = fileXML;
            try
            {
                Document doc = wordApp.Documents.Open(ref docFile, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

                doc.SaveAs(ref xmlFile, ref xmlFormat, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

                doc.Close(ref missing, ref missing, ref missing);
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            finally
            {
                wordApp.Quit(WdSaveOptions.wdPromptToSaveChanges, WdOriginalFormat.wdWordDocument, Type.Missing);
            }

            string text = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(fileXML))
                {
                    text = reader.ReadToEnd();
                }
                File.Delete(fileXML);
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }

            TicketTemplate ticketTemplate = new TicketTemplate
            {
                TemplateName = model.TemplateName,
                TicketTemplateBodies = new List<TicketTemplateBody>()
            };
            var body = XMLParser.GetBody(text, ticketTemplate.Id);
            ticketTemplate.TicketTemplateBodies.Add(body);

            return ResultService<TicketTemplateViewModel>.Success(TicketModelFactoryToViewModel.CreateTicketTemplate(ticketTemplate));
        }

        public ResultService MakeTickets(TicketProcessMakeTicketsBindingModel model)
        {
            Random random = new Random();
            try
            {
                using (var context = new DepartmentDbContext())
                {
                    #region Получаем вопросы
                    // получаем список блоков, которые входят в билет и вопросы к ним
                    Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>> constQuestionDisctionary = new Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>>();

                    Dictionary<ExaminationTemplateBlock, int> counterForBlocks = new Dictionary<ExaminationTemplateBlock, int>();


                    Dictionary<ExaminationTemplateBlock, Dictionary<ExaminationTemplateBlock, int>> counterForRandomBlocks = new Dictionary<ExaminationTemplateBlock, Dictionary<ExaminationTemplateBlock, int>>();

                    Dictionary<string, Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>>> constBlockRandomQuestionDisctionary = new Dictionary<string, Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>>>();
                    Dictionary<string, Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>>> duplicateRanodm = new Dictionary<string, Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>>>();

                    // выбираем блоки, которые должны быть в билетах
                    var blocks = context.ExaminationTemplateBlocks
                        .Where(x => x.ExaminationTemplateId == model.ExaminationTemplateId && !x.IsDeleted && x.CountQuestionInTicket > 0)
                        .OrderBy(x => x.BlockName)
                        .ToList();
                    foreach (var block in blocks)
                    {
                        // если блок - выбор из нескольких
                        if (block.IsCombine)
                        {
                            counterForRandomBlocks.Add(block, new Dictionary<ExaminationTemplateBlock, int>());
                            var tags = block.CombineBlocks.Split(',');
                            Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>> temp = new Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>>();

                            foreach (var tag in tags)
                            {
                                var randBlock = context.ExaminationTemplateBlocks.FirstOrDefault(x => x.ExaminationTemplateId == model.ExaminationTemplateId && !x.IsDeleted && x.QuestionTagInTemplate == tag);
                                if (randBlock == null)
                                {
                                    return ResultService.Error("Error", $"Не найден блок вопросов с тегом {tag}", ResultServiceStatusCode.NotFound);
                                }
                                counterForRandomBlocks[block].Add(randBlock, 0);
                                temp.Add(randBlock, context.ExaminationTemplateBlockQuestions.Where(x => x.ExaminationTemplateBlockId == randBlock.Id && !x.IsDeleted).OrderBy(x => x.QuestionNumber).ToList());
                            }

                            constBlockRandomQuestionDisctionary.Add(block.BlockName, temp);
                            duplicateRanodm.Add(block.BlockName, GetDublicateDictionary(temp));
                            constQuestionDisctionary.Add(block, new List<ExaminationTemplateBlockQuestion>());
                        }
                        else
                        {
                            constQuestionDisctionary.Add(block, context.ExaminationTemplateBlockQuestions.Where(x => x.ExaminationTemplateBlockId == block.Id && !x.IsDeleted).OrderBy(x => x.QuestionNumber).ToList());
                            if (block.CountQuestionInTicket > constQuestionDisctionary[block].Count)
                            {
                                throw new Exception($"Вопросов в блоке {block.BlockName} меньше ({constQuestionDisctionary[block].Count}), чем требуется в билете ({block.CountQuestionInTicket})");
                            }
                            counterForBlocks.Add(block, 0);
                        }
                    }
                    #endregion

                    var dublicate = GetDublicateDictionary(constQuestionDisctionary);
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            #region
                            bool stopCreate = false;
                            if (model.HowCreateTickets == HowCreateTickets.ПоКоличествуБилетов)
                            {
                                if (!model.CountTickets.HasValue || model.CountTickets == 0)
                                {
                                    throw new Exception("Не указано количество вопросов");
                                }
                            }
                            else if (model.HowCreateTickets == HowCreateTickets.ПоВыбранномуБлоку)
                            {
                                if (!model.SelectedBlock.HasValue)
                                {
                                    throw new Exception("Не выбран блок");
                                }
                            }
                            for (int i = 0; !stopCreate; ++i)
                            {
                                if (model.HowCreateTickets == HowCreateTickets.ПоКоличествуБилетов && i >= model.CountTickets)
                                {
                                    stopCreate = true;
                                    break;
                                }
                                ExaminationTemplateTicket ticket = TicketModelFacotryFromBindingModel.CreateExaminationTemplateTicket(new ExaminationTemplateTicketSetBindingModel
                                {
                                    ExaminationTemplateId = model.ExaminationTemplateId,
                                    TicketNumber = i + 1
                                });

                                ticket.ExaminationTemplateTicketQuestions = new List<ExaminationTemplateTicketQuestion>();
                                // порядковый номер вопроса в билете
                                int order = 0;

                                foreach (var elem in dublicate)
                                {
                                    if (elem.Key.IsCombine)
                                    {
                                        #region Рандомный блок
                                        // если для рандомного блока не хватает блоков под вопросы
                                        if (model.HowUseBlock[elem.Key.BlockName] == HowUseExaminationBlock.СбросПередБилетомПриНехваткиБлоков && elem.Key.CountQuestionInTicket > duplicateRanodm[elem.Key.BlockName].Count)
                                        {
                                            if (model.HowCreateTickets == HowCreateTickets.ПокаВозможноСоздавать)
                                            {
                                                stopCreate = true;
                                                break;
                                            }
                                            else if (model.HowCreateTickets == HowCreateTickets.ПоВыбранномуБлоку && elem.Key.Id == model.SelectedBlock)
                                            {
                                                stopCreate = true;
                                                break;
                                            }
                                            else
                                            {
                                                duplicateRanodm[elem.Key.BlockName].Clear();
                                                duplicateRanodm[elem.Key.BlockName] = GetDublicateDictionary(constBlockRandomQuestionDisctionary[elem.Key.BlockName]);
                                                var keys = counterForRandomBlocks[elem.Key].Keys.ToList();
                                                for (int k = 0; k < keys.Count; ++k)
                                                {
                                                    counterForRandomBlocks[elem.Key][keys[k]] = 0;
                                                }
                                            }
                                        }
                                        // если для рандомного блока не хватает вопросов по всем оставшимся блокам
                                        if (model.HowUseBlock[elem.Key.BlockName] == HowUseExaminationBlock.СбросПередБилетомПриНехваткиВопросов && elem.Key.CountQuestionInTicket > duplicateRanodm[elem.Key.BlockName].Sum(x => x.Value.Count))
                                        {
                                            if (model.HowCreateTickets == HowCreateTickets.ПокаВозможноСоздавать)
                                            {
                                                stopCreate = true;
                                                break;
                                            }
                                            else if (model.HowCreateTickets == HowCreateTickets.ПоВыбранномуБлоку && elem.Key.Id == model.SelectedBlock)
                                            {
                                                stopCreate = true;
                                                break;
                                            }
                                            else
                                            {
                                                duplicateRanodm[elem.Key.BlockName].Clear();
                                                duplicateRanodm[elem.Key.BlockName] = GetDublicateDictionary(constBlockRandomQuestionDisctionary[elem.Key.BlockName]);
                                                var keys = counterForRandomBlocks[elem.Key].Keys.ToList();
                                                for (int k = 0; k < keys.Count; ++k)
                                                {
                                                    counterForRandomBlocks[elem.Key][keys[k]] = 0;
                                                }
                                            }
                                        }

                                        // вытаскиваем вопросы
                                        for (int j = 0; j < elem.Key.CountQuestionInTicket && !stopCreate; ++j)
                                        {
                                            // если для блока указано обновлять список, как только закончатся вопросы
                                            if (model.HowUseBlock[elem.Key.BlockName] == HowUseExaminationBlock.СбросПриОкончанииСписка && duplicateRanodm[elem.Key.BlockName].Sum(x => x.Value.Count) == 0)
                                            {
                                                duplicateRanodm[elem.Key.BlockName].Clear();
                                                duplicateRanodm[elem.Key.BlockName] = GetDublicateDictionary(constBlockRandomQuestionDisctionary[elem.Key.BlockName]);
                                            }
                                            // выбираем случайный блок
                                            var indexBlock = random.Next(0, duplicateRanodm[elem.Key.BlockName].Count);
                                            var randBlock = duplicateRanodm[elem.Key.BlockName].Keys.ToList()[indexBlock];

                                            // ищем вопрос, котрого еще нет в билете
                                            int index = -1;
                                            while (index < 0 || ticket.ExaminationTemplateTicketQuestions.Exists(x => x.ExaminationTemplateBlockQuestionId == duplicateRanodm[elem.Key.BlockName][randBlock][index].Id))
                                            {
                                                if (model.HowGetQuestionFromBlock[elem.Key.BlockName] == HowGetQuestionFromExaminationBlock.РандомныйВопрос)
                                                {
                                                    // берем рандомный вопрос, котрого еще не было в этом билете
                                                    index = random.Next(0, elem.Value.Count);
                                                }
                                                else if (model.HowGetQuestionFromBlock[elem.Key.BlockName] == HowGetQuestionFromExaminationBlock.ПоСписку)
                                                {
                                                    // берем вопрос по списку
                                                    index = counterForRandomBlocks[elem.Key][randBlock]++;
                                                }
                                                if (index >= duplicateRanodm[elem.Key.BlockName][randBlock].Count)
                                                {
                                                    throw new Exception($"В блоке {elem.Key.BlockName} кончились вопросы. Требуется вопрос под номером {index}");
                                                }
                                            }

                                            ExaminationTemplateTicketQuestion ticketQuestion = CreateQuestion(duplicateRanodm[elem.Key.BlockName][randBlock][index].Id, ticket.Id, elem.Key.Id, order++);

                                            ticket.ExaminationTemplateTicketQuestions.Add(ticketQuestion);

                                            duplicateRanodm[elem.Key.BlockName][randBlock].RemoveAt(index);

                                            // если блок вопросов пуст, удаляем его
                                            if (duplicateRanodm[elem.Key.BlockName][randBlock].Count == 0)
                                            {
                                                duplicateRanodm[elem.Key.BlockName].Remove(randBlock);
                                            }
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region простой блок
                                        // если для блока указано обновлять список, если для билета не хватает вопросов
                                        // если требуемое количество вопросов в билете превышает доступное, то обновляем список
                                        if (model.HowUseBlock[elem.Key.BlockName] == HowUseExaminationBlock.СбросПередБилетом && elem.Key.CountQuestionInTicket > elem.Value.Count)
                                        {
                                            if (model.HowCreateTickets == HowCreateTickets.ПокаВозможноСоздавать)
                                            {
                                                stopCreate = true;
                                                break;
                                            }
                                            else if (model.HowCreateTickets == HowCreateTickets.ПоВыбранномуБлоку && elem.Key.Id == model.SelectedBlock)
                                            {
                                                stopCreate = true;
                                                break;
                                            }
                                            else
                                            {
                                                elem.Value.Clear();
                                                elem.Value.AddRange(GetDuplicateList(constQuestionDisctionary[elem.Key]));
                                                counterForBlocks[elem.Key] = 0;
                                            }
                                        }

                                        // вытаскиваем вопросы
                                        for (int j = 0; j < elem.Key.CountQuestionInTicket && !stopCreate; ++j)
                                        {
                                            // если для блока указано обновлять список, как только закончатся вопросы
                                            if (model.HowUseBlock[elem.Key.BlockName] == HowUseExaminationBlock.СбросПриОкончанииСписка && elem.Value.Count == 0)
                                            {
                                                elem.Value.AddRange(GetDuplicateList(constQuestionDisctionary[elem.Key]));
                                                counterForBlocks[elem.Key] = 0;
                                            }
                                            int index = -1;

                                            // ищем вопрос, котрого еще нет в билете
                                            while (index < 0 || ticket.ExaminationTemplateTicketQuestions.Exists(x => x.ExaminationTemplateBlockQuestionId == elem.Value[index].Id))
                                            {
                                                if (model.HowGetQuestionFromBlock[elem.Key.BlockName] == HowGetQuestionFromExaminationBlock.РандомныйВопрос)
                                                {
                                                    // берем рандомный вопрос, котрого еще не было в этом билете
                                                    index = random.Next(0, elem.Value.Count);
                                                }
                                                else if (model.HowGetQuestionFromBlock[elem.Key.BlockName] == HowGetQuestionFromExaminationBlock.ПоСписку)
                                                {
                                                    // берем вопрос по списку
                                                    index = counterForBlocks[elem.Key]++;
                                                }
                                                if (index >= elem.Value.Count)
                                                {
                                                    throw new Exception($"В блоке {elem.Key.BlockName} кончились вопросы. Требуется вопрос под номером {index}");
                                                }
                                            }

                                            ExaminationTemplateTicketQuestion ticketQuestion = CreateQuestion(elem.Value[index].Id, ticket.Id, elem.Key.Id, order++);

                                            ticket.ExaminationTemplateTicketQuestions.Add(ticketQuestion);

                                            elem.Value.RemoveAt(index);
                                        }
                                        #endregion
                                    }
                                }

                                // если флаг в true, значит сформировать билет уже не получилось
                                if (!stopCreate)
                                {
                                    context.ExaminationTemplateTickets.Add(ticket);
                                    context.SaveChanges();
                                }
                            }
                            #endregion

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return ResultService.Error(ex, ResultServiceStatusCode.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }

            return ResultService.Success();
        }

        public ResultService SaveTemplate(TicketProcessLoadTemplateBindingModel model)
        {
            if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
            {
                throw new Exception("Нет доступа на чтение данных по записям {0}");
            }
            // переводим doc-файл в формат xml и обрабатываем его
            object missing = System.Reflection.Missing.Value;
            Application wordApp = new Application
            {
                Visible = false,
                ScreenUpdating = false
            };

            Object xmlFormat = WdSaveFormat.wdFormatXML;
            Object docFile = model.FileName;
            string fileXML = model.FileName + ".xml";
            Object xmlFile = fileXML;
            try
            {
                Document doc = wordApp.Documents.Open(ref docFile, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

                doc.SaveAs(ref xmlFile, ref xmlFormat, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

                doc.Close(ref missing, ref missing, ref missing);
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
            finally
            {
                wordApp.Quit(WdSaveOptions.wdPromptToSaveChanges, WdOriginalFormat.wdWordDocument, Type.Missing);
            }

            string text = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(fileXML))
                {
                    text = reader.ReadToEnd();
                }
                File.Delete(fileXML);
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }

            TicketTemplate ticketTemplate = new TicketTemplate
            {
                ExaminationTemplateId = model.ExaminationTemplateId,
                TemplateName = model.TemplateName,
                XML = text,
                TicketTemplateBodies = new List<TicketTemplateBody>()
            };

            var body = XMLParser.GetBody(text, ticketTemplate.Id);

            try
            {
                using (var context = new DepartmentDbContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.TicketTemplates.Add(ticketTemplate);
                            context.SaveChanges();

                            if (body != null)
                            {
                                context.TicketTemplateBodies.Add(body);
                                context.SaveChanges();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return ResultService.Error(ex, ResultServiceStatusCode.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }

            return ResultService.Success(ticketTemplate.Id);
        }

        public ResultService SynchronizeBlocksByTemplate(TicketProcessSynchronizeBlocksByTemplateBindingModel model)
        {
            try
            {
                using (var context = new DepartmentDbContext())
                {
                    var ticketTemplate = context.TicketTemplates.FirstOrDefault(x => x.ExaminationTemplateId == model.ExaminationTemplateId && !x.IsDeleted);
                    if (ticketTemplate == null)
                    {
                        return ResultService.Error("Error:", "TicketTemplate not found", ResultServiceStatusCode.NotFound);
                    }

                    var body = XMLParser.GetBody(ticketTemplate.XML, ticketTemplate.Id);

                    var questions = TicketTemplateAnalyser.AnalysisBody(body);

                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var question in questions)
                            {
                                var block = context.ExaminationTemplateBlocks.FirstOrDefault(x => x.ExaminationTemplateId == model.ExaminationTemplateId && x.QuestionTagInTemplate == question.Key && !x.IsDeleted);
                                if (block == null)
                                {
                                    block = context.ExaminationTemplateBlocks.FirstOrDefault(x => x.ExaminationTemplateId == model.ExaminationTemplateId && x.QuestionTagInTemplate == question.Key);
                                    if (block == null)
                                    {
                                        block = TicketModelFacotryFromBindingModel.CreateExaminationTemplateBlock(new ExaminationTemplateBlockSetBindingModel
                                        {
                                            ExaminationTemplateId = model.ExaminationTemplateId,
                                            BlockName = question.Key,
                                            CountQuestionInTicket = question.Value,
                                            QuestionTagInTemplate = question.Key,
                                            IsCombine = question.Key.ToLower().Contains("random"),
                                            CombineBlocks = question.Key.ToLower().Contains("random") ? TicketTemplateAnalyser.RandomQuestions[question.Key] : string.Empty
                                        });
                                        context.ExaminationTemplateBlocks.Add(block);
                                        context.SaveChanges();
                                    }
                                    else
                                    {
                                        block.CountQuestionInTicket = question.Value;
                                        block.IsCombine = question.Key.ToLower().Contains("random");
                                        block.CombineBlocks = question.Key.ToLower().Contains("random") ? TicketTemplateAnalyser.RandomQuestions[question.Key] : string.Empty;
                                        block.IsDeleted = false;
                                        block.DateDelete = null;
                                        context.SaveChanges();
                                    }
                                }
                                else
                                {
                                    block.CountQuestionInTicket = question.Value;
                                    block.IsCombine = question.Key.ToLower().Contains("random");
                                    block.CombineBlocks = question.Key.ToLower().Contains("random") ? TicketTemplateAnalyser.RandomQuestions[question.Key] : string.Empty;
                                    context.SaveChanges();
                                }
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return ResultService.Error(ex, ResultServiceStatusCode.Error);
                        }
                    }

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService UploadTickets(TicketProcessUploadTicketsBindingModel model)
        {
            try
            {
                using (var context = new DepartmentDbContext())
                {
                    var ticketTemplate = context.TicketTemplates.FirstOrDefault(x => x.Id == model.TicketTemplateId && !x.IsDeleted);
                    if(ticketTemplate == null)
                    {
                        return ResultService.Error("Error:", "TicketTemplate not found", ResultServiceStatusCode.NotFound);
                    }

                    var examinationTemplate = context.ExaminationTemplates
                        .Include(x => x.Discipline)
                        .Include(x => x.EducationDirection)
                        .FirstOrDefault(x => x.Id == ticketTemplate.ExaminationTemplateId && !x.IsDeleted);
                    if (examinationTemplate == null)
                    {
                        return ResultService.Error("Error:", "ExaminationTemplate not found", ResultServiceStatusCode.NotFound);
                    }

                    var blocks = context.ExaminationTemplateBlocks.Where(x => x.ExaminationTemplateId == ticketTemplate.ExaminationTemplateId && !x.IsDeleted).ToList();
                    var tickets = context.ExaminationTemplateTickets.Where(x => x.ExaminationTemplateId == ticketTemplate.ExaminationTemplateId && !x.IsDeleted).OrderBy(x => x.TicketNumber);

                    var body = TicketBodyGet.GetBody(context, ticketTemplate.Id);

                    TicketDocCreator docCreator = new TicketDocCreator();

                    using (StreamWriter writer = new StreamWriter(model.FileName + ".xml"))
                    {
                        var stratIndex = ticketTemplate.XML.IndexOf("<w:body>");
                        writer.Write(ticketTemplate.XML.Substring(0, stratIndex));
                        writer.Write("<w:body><wx:sect>");
                        foreach(var ticket in tickets)
                        {
                            var questions = context.ExaminationTemplateTicketQuestions
                                                .Where(x => x.ExaminationTemplateTicketId == ticket.Id && !x.IsDeleted)
                                                .Include(x => x.ExaminationTemplateBlockQuestion)
                                                .ToList();

                            writer.Write(docCreator.GetBody(body, examinationTemplate, ticket, questions, blocks));
                        }

                        writer.Write(docCreator.GetBodyFormat(body));
                        writer.Write("</wx:sect></w:body></w:wordDocument>");
                    }

                    Application wordApp = new Application();
                    try
                    {
                        object missing = System.Reflection.Missing.Value;
                        wordApp.Visible = false;
                        wordApp.ScreenUpdating = false;
                        
                        Object docFormat = WdSaveFormat.wdFormatDocument;
                        Object openFormat = WdOpenFormat.wdOpenFormatXML;
                        Object f = model.FileName;
                        Object fxml = model.FileName + ".xml";

                        wordApp.Documents.Open(ref fxml, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref openFormat, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                        
                        Document doc = wordApp.ActiveDocument;
                        doc.SaveAs(ref f, ref docFormat, ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

                        doc.Close(ref missing, ref missing, ref missing);

                        File.Delete(model.FileName + ".xml");
                    }
                    catch (Exception ex)
                    {
                        return ResultService.Error(ex, ResultServiceStatusCode.Error);
                    }
                    finally
                    {
                        wordApp.Quit(WdSaveOptions.wdPromptToSaveChanges, WdOriginalFormat.wdWordDocument, Type.Missing);
                    }
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }

            return ResultService.Success();
        }

        public ResultService<List<TicketProcessGetParagraphDatasViewModel>> GetParagraphDatas(TicketProcessGetParagraphDatasBindingModel model)
        {
            List<TicketProcessGetParagraphDatasViewModel> datas = new List<TicketProcessGetParagraphDatasViewModel>();
            try
            {
                using (var context = new DepartmentDbContext())
                {
                    var body = TicketBodyGet.GetBody(context, model.TicketTemplateId);
                    if(body != null)
                    {

                    }
                }
            }
            catch(Exception ex)
            {
                return ResultService<List<TicketProcessGetParagraphDatasViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }

            return ResultService<List<TicketProcessGetParagraphDatasViewModel>>.Success(datas);
        }

        private Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>> GetDublicateDictionary(Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>> original)
        {
            Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>> duplicate = new Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>>();

            foreach (var elem in original)
            {
                List<ExaminationTemplateBlockQuestion> questions = new List<ExaminationTemplateBlockQuestion>();
                foreach (var q in elem.Value)
                {
                    questions.Add(q);
                }
                duplicate.Add(elem.Key, questions);
            }

            return duplicate;
        }

        private List<ExaminationTemplateBlockQuestion> GetDuplicateList(List<ExaminationTemplateBlockQuestion> original)
        {
            List<ExaminationTemplateBlockQuestion> questions = new List<ExaminationTemplateBlockQuestion>();
            foreach (var q in original)
            {
                questions.Add(q);
            }

            return questions;
        }

        private ExaminationTemplateTicketQuestion CreateQuestion(Guid examinationTemplateBlockQuestionId, Guid ticketId, Guid examinationTemplateBlockId, int order)
        {
            return TicketModelFacotryFromBindingModel.CreateExaminationTemplateTicketQuestion(new ExaminationTemplateTicketQuestionSetBindingModel
            {
                ExaminationTemplateBlockQuestionId = examinationTemplateBlockQuestionId,
                ExaminationTemplateTicketId = ticketId,
                ExaminationTemplateBlockId = examinationTemplateBlockId,
                Order = order
            });
        }
    }
}