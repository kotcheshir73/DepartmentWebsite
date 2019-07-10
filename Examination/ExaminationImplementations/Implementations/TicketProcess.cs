using Enums;
using ExaminationImplementations.Helpers;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using Models.Examination;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TicketServiceImplementations.Helpers;
using Tools;

namespace ExaminationImplementations.Implementations
{
    public class TicketProcess : ITicketProcess
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        private readonly string _entity = "Составление Экзаменов";

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
                    using (var context = DepartmentUserManager.GetContext)
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var question in questions)
                                {
                                    context.ExaminationTemplateBlockQuestions.Add(
                                        ExaminationModelFacotryFromBindingModel.CreateExaminationTemplateBlockQuestion(new ExaminationTemplateBlockQuestionSetBindingModel
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
            DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

            TicketTemplateViewModel ticketTemplate = ExaminationModelFactoryToViewModel.CreateTicketTemplateViewModel(WordParser.ParseDocument(model));

            return ResultService<TicketTemplateViewModel>.Success(ticketTemplate);
        }

        public ResultService MakeTickets(TicketProcessMakeTicketsBindingModel model)
        {
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

            Random random = new Random();
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    #region Получаем вопросы
                    // здесь будут храниться вопросы с пометками, использованы они или нет
                    Dictionary<Guid, Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>>> questions =
                        new Dictionary<Guid, Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>>>();

                    var blocks = context.ExaminationTemplateBlocks
                        .Include(x => x.ExaminationTemplateBlockQuestions)
                        .Where(x => x.ExaminationTemplateId == model.ExaminationTemplateId && !x.IsDeleted)
                        .OrderBy(x => x.BlockName)
                        .ToList();
                    foreach (var block in blocks.Where(x => x.CountQuestionInTicket > 0))
                    {
                        if (block.IsCombine)
                        {
                            var dict = new Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>>();

                            var tags = block.CombineBlocks.Split(',');
                            foreach (var tag in tags)
                            {
                                var tagBlock = blocks.FirstOrDefault(x => x.QuestionTagInTemplate == tag);
                                if (tagBlock == null)
                                {
                                    return ResultService.Error("Error", $"Не найден блок вопросов с тегом {tag}", ResultServiceStatusCode.NotFound);
                                }
                                dict.Add(tagBlock,
                                    new List<ExaminationTemplateBlockQuestion>(tagBlock.ExaminationTemplateBlockQuestions.Select(x => x)));
                            }

                            questions.Add(block.Id, dict);
                        }
                        else
                        {
                            if (block.CountQuestionInTicket > block.ExaminationTemplateBlockQuestions.Count)
                            {
                                throw new Exception($"Вопросов в блоке {block.BlockName} меньше ({block.ExaminationTemplateBlockQuestions.Count}), чем требуется в билете ({block.CountQuestionInTicket})");
                            }

                            questions.Add(block.Id, new Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>>
                                {
                                    {
                                        block, new List<ExaminationTemplateBlockQuestion>(block.ExaminationTemplateBlockQuestions.Select(x => x))
                                    }
                                });
                        }
                    }

                    // сбрасываем все признаки участия в билетах у вопросов в 0
                    foreach (var q in questions)
                    {
                        foreach (var block in q.Value)
                        {
                            block.Key.IsUse = false;
                            foreach (var elem in block.Value)
                            {
                                elem.IsUse = false;
                            }
                        }
                    }

                    var selectedBlock = model.SelectedBlock.HasValue ? blocks.FirstOrDefault(x => x.Id == model.SelectedBlock) : null;
                    #endregion

                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            #region
                            bool stopCreate = false;
                            for (int i = 0; !stopCreate; ++i)
                            {
                                if (model.HowCreateTickets == HowCreateTickets.ПоКоличествуБилетов && i >= model.CountTickets)
                                {
                                    break;
                                }

                                if (model.HowCreateTickets == HowCreateTickets.ПоВыбранномуБлоку && i >= selectedBlock.ExaminationTemplateBlockQuestions.Count)
                                {
                                    break;
                                }

                                ExaminationTemplateTicket ticket = ExaminationModelFacotryFromBindingModel.CreateExaminationTemplateTicket(new ExaminationTemplateTicketSetBindingModel
                                {
                                    ExaminationTemplateId = model.ExaminationTemplateId,
                                    TicketNumber = i + 1
                                });

                                ticket.ExaminationTemplateTicketQuestions = new List<ExaminationTemplateTicketQuestion>();
                                // порядковый номер вопроса в билете
                                int order = 0;

                                foreach (var block in blocks.Where(x => x.CountQuestionInTicket > 0))
                                {
                                    if (block.IsCombine)
                                    {
                                        var keys = questions[block.Id].Keys.ToList();
                                        // если для рандомного блока не хватает блоков под вопросы
                                        if ((model.HowUseBlock[block.BlockName] == HowUseExaminationBlock.СбросПередБилетомПриНехваткиБлоков ||
                                                    model.HowUseBlock[block.BlockName] == HowUseExaminationBlock.СбросПередБилетомПриНехваткиВопросов) &&
                                                block.CountQuestionInTicket > questions[block.Id].Where(x => x.Value.Where(y => !y.IsUse).Count() > 0).Count())
                                        {
                                            if (model.HowCreateTickets == HowCreateTickets.ПокаВозможноСоздавать)
                                            {
                                                stopCreate = true;
                                                break;
                                            }
                                            else
                                            {
                                                ResetBlockQuestions(questions[block.Id]);
                                            }
                                        }

                                        // вытаскиваем вопросы
                                        for (int j = 0; j < block.CountQuestionInTicket; ++j)
                                        {
                                            // если для блока указано обновлять список, как только закончатся вопросы
                                            if (model.HowUseBlock[block.BlockName] == HowUseExaminationBlock.СбросПриОкончанииСписка &&
                                                        questions[block.Id].SelectMany(x => x.Value).Where(x => !x.IsUse).Count() == 0)
                                            {
                                                ResetBlockQuestions(questions[block.Id]);
                                            }
                                            // выбираем случайный блок, в котором еще есть вопросы и который еще не использовался в этом билете
                                            var indexBlock = random.Next(0, questions[block.Id].Count);
                                            while (keys[indexBlock].IsUse || questions[block.Id][keys[indexBlock]].Where(x => !x.IsUse).Count() <= 0)
                                            {
                                                indexBlock++;
                                                if (indexBlock == keys.Count)
                                                {
                                                    indexBlock = 0;
                                                }
                                            }

                                            GetQuestionInTicket(model.HowGetQuestionFromBlock[block.BlockName], questions[block.Id][keys[indexBlock]], ticket, keys[indexBlock], block, ref order);

                                            keys[indexBlock].IsUse = true;
                                        }
                                    }
                                    else
                                    {
                                        // если для блока указано обновлять список
                                        // если требуемое количество вопросов в билете превышает доступное, то обновляем список
                                        if (model.HowUseBlock[block.BlockName] == HowUseExaminationBlock.СбросПередБилетом &&
                                                block.CountQuestionInTicket > questions[block.Id][block].Where(x => !x.IsUse).Count())
                                        {
                                            if (model.HowCreateTickets == HowCreateTickets.ПокаВозможноСоздавать)
                                            {
                                                stopCreate = true;
                                                break;
                                            }
                                            else
                                            {
                                                ResetBlockQuestions(questions[block.Id]);
                                            }
                                        }

                                        for (int j = 0; j < block.CountQuestionInTicket && !stopCreate; ++j)
                                        {
                                            // если для блока указано обновлять список, как только закончатся вопросы
                                            // и все вопросы в блоке уже выбраны
                                            if (model.HowUseBlock[block.BlockName] == HowUseExaminationBlock.СбросПриОкончанииСписка &&
                                                questions[block.Id][block].Where(x => x.IsUse).Count() == 0)
                                            {
                                                ResetBlockQuestions(questions[block.Id]);
                                            }

                                            GetQuestionInTicket(model.HowGetQuestionFromBlock[block.BlockName], questions[block.Id][block], ticket, block, block, ref order);
                                        }
                                    }
                                }

                                // если флаг в true, значит сформировать билет уже не получилось
                                if (!stopCreate)
                                {
                                    context.ExaminationTemplateTickets.Add(ticket);
                                    context.SaveChanges();
                                }

                                ResetBlockCombine(questions);
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

        public ResultService SynchronizeBlocksByTemplate(TicketProcessSynchronizeBlocksByTemplateBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var ticketTemplate = GetTicketTemplate(model.ExaminationTemplateId);
                    if (ticketTemplate == null)
                    {
                        return ResultService.Error("Error:", "TicketTemplate not found", ResultServiceStatusCode.NotFound);
                    }

                    var questions = TicketTemplateAnalyser.AnalysisBody(ticketTemplate.TicketTemplateBody);

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
                                        block = ExaminationModelFacotryFromBindingModel.CreateExaminationTemplateBlock(new ExaminationTemplateBlockSetBindingModel
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
                using (var context = DepartmentUserManager.GetContext)
                {
                    var examinationTemplate = context.ExaminationTemplates
                        .Include(x => x.Discipline)
                        .Include(x => x.EducationDirection)
                        .Include(x => x.ExaminationTemplateBlocks)
                        .FirstOrDefault(x => x.Id == model.ExaminationTemplateId && !x.IsDeleted);
                    if (examinationTemplate == null)
                    {
                        return ResultService.Error("Error:", "ExaminationTemplate not found", ResultServiceStatusCode.NotFound);
                    }

                    var ticketTemplate = GetTicketTemplate(model.ExaminationTemplateId);
                    if (ticketTemplate == null)
                    {
                        return ResultService.Error("Error:", "TicketTemplate not found", ResultServiceStatusCode.NotFound);
                    }

                    var tickets = context.ExaminationTemplateTickets
                        .Include(x => x.ExaminationTemplateTicketQuestions)
                        .Include("ExaminationTemplateTicketQuestions.ExaminationTemplateBlockQuestion")
                        .Where(x => x.ExaminationTemplateId == model.ExaminationTemplateId && !x.IsDeleted);
                    if (tickets == null)
                    {
                        return ResultService.Error("Error:", "tickets not found", ResultServiceStatusCode.NotFound);
                    }

                    WordCreator.CreateDoc(model.FileName, ticketTemplate, tickets.ToList(), examinationTemplate);
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
                using (var context = DepartmentUserManager.GetContext)
                {
                    //var body = TicketBodyGet.GetBody(model.TicketTemplateId);
                    //if(body != null)
                    //{

                    //}
                }
            }
            catch (Exception ex)
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
            return ExaminationModelFacotryFromBindingModel.CreateExaminationTemplateTicketQuestion(new ExaminationTemplateTicketQuestionSetBindingModel
            {
                ExaminationTemplateBlockQuestionId = examinationTemplateBlockQuestionId,
                ExaminationTemplateTicketId = ticketId,
                ExaminationTemplateBlockId = examinationTemplateBlockId,
                Order = order
            });
        }

        private void ResetBlockCombine(Dictionary<Guid, Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>>> blocks)
        {
            foreach (var block in blocks)
            {
                foreach (var elem in block.Value)
                {
                    elem.Key.IsUse = false;
                }
            }
        }

        private void ResetBlockQuestions(Dictionary<ExaminationTemplateBlock, List<ExaminationTemplateBlockQuestion>> blocks)
        {
            foreach (var block in blocks)
            {
                foreach (var q in block.Value)
                {
                    q.IsUse = false;
                }
            }
        }

        private void GetQuestionInTicket(HowGetQuestionFromExaminationBlock howGetQuestion, List<ExaminationTemplateBlockQuestion> blockQ, ExaminationTemplateTicket ticket,
            ExaminationTemplateBlock block, ExaminationTemplateBlock baseBlock, ref int order)
        {
            int index = -1;
            Random rnd = new Random();

            // ищем вопрос, котрого еще нет в билете
            while (index < 0 || ticket.ExaminationTemplateTicketQuestions.Exists(x => x.ExaminationTemplateBlockQuestionId == blockQ[index].Id))
            {
                if (blockQ.Where(x => !x.IsUse).Count() == 0)
                {
                    throw new Exception($"В блоке {block.BlockName} кончились вопросы");
                }
                if (howGetQuestion == HowGetQuestionFromExaminationBlock.РандомныйВопрос)
                {
                    // берем рандомный вопрос, котрого еще не было в этом билете
                    index = rnd.Next(0, blockQ.Count);
                }
                else if (howGetQuestion == HowGetQuestionFromExaminationBlock.ПоСписку)
                {
                    // берем вопрос по списку
                    index = 0;
                }
                // генерируем номер и ищем ближайший впорос, который еще не использовали
                while (blockQ[index].IsUse)
                {
                    index++;
                    if (index == blockQ.Count)
                    {
                        index = 0;
                    }
                }
            }

            ExaminationTemplateTicketQuestion ticketQuestion = CreateQuestion(blockQ[index].Id, ticket.Id, baseBlock.Id, order++);

            ticket.ExaminationTemplateTicketQuestions.Add(ticketQuestion);

            blockQ[index].IsUse = true;
        }

        private TicketTemplate GetTicketTemplate(Guid examinationTemplateId)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                return context.ExaminationTemplates
                    .Include(x => x.TicketTemplate)
                    .Include(x => x.TicketTemplate.TicketTemplateFontTables)
                    .Include(x => x.TicketTemplate.TicketTemplateNumberings)
                    .Include(x => x.TicketTemplate.TicketTemplateDocumentSettings)
                    .Include(x => x.TicketTemplate.TicketTemplateStyleDefinitions)
                    .Include(x => x.TicketTemplate.TicketTemplateWebSettings)
                    .Include("TicketTemplate.TicketTemplateBody")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateBodyProperties")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateParagraphs")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateTables")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateTables.TicketTemplateTableProperties")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateTables.TicketTemplateTableGridColumns")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateTables.TicketTemplateTableRows")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableRowProperties")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateTableCellProperties")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphProperties")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns")
                    .Include("TicketTemplate.TicketTemplateBody.TicketTemplateTables.TicketTemplateTableRows.TicketTemplateTableCells.TicketTemplateParagraphs.TicketTemplateParagraphRuns.TicketTemplateParagraphRunProperties")
                    .FirstOrDefault(x => x.Id == examinationTemplateId && !x.IsDeleted)
                    .TicketTemplate;
            }
        }
    }
}