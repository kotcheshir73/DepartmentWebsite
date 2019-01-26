using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public TicketProcess()
        {
        }

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

                if(questions.Count > 0)
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

            try
            {
                using (var context = new DepartmentDbContext())
                {
                    var blocks = context.ExaminationTemplateBlocks.Where(x => x.ExaminationTemplateId == model.ExaminationTemplateId && !x.IsDeleted).ToList();
                    if (blocks.Count > 0)
                    {
                        TicketTemplateAnalyser.AnalysisBody(body, blocks);
                    }
                }
            }
            catch (Exception ex)
            {
                return ResultService<TicketTemplateViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }

            return ResultService<TicketTemplateViewModel>.Success(TicketModelFactoryToViewModel.CreateTicketTemplate(ticketTemplate));
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

                            var blocks = context.ExaminationTemplateBlocks.Where(x => x.ExaminationTemplateId == model.ExaminationTemplateId && !x.IsDeleted).ToList();

                            var questions = TicketTemplateAnalyser.AnalysisBody(body, blocks);

                            if (blocks.Count == 0)
                            {
                                foreach (var question in questions)
                                {
                                    var block = TicketModelFacotryFromBindingModel.CreateExaminationTemplateBlock(new ExaminationTemplateBlockSetBindingModel
                                    {
                                        ExaminationTemplateId = model.ExaminationTemplateId.Value,
                                        BlockName = question.Key,
                                        CountQuestionInTicket = question.Value,
                                        QuestionTagInTemplate = question.Key
                                    });
                                    context.ExaminationTemplateBlocks.Add(block);
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
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }

            return ResultService.Success(ticketTemplate.Id);
        }
    }
}