using DepartmentModel;
using DepartmentModel.Enums;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketModels.Models;
using TicketServiceImplementations.Helpers;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;

namespace TicketServiceImplementations.Implementations
{
    public class TicketProcess : ITicketProcess
    {
        public ResultService LoadTemplate(TicketProcessLoadTemplateBindingModel model)
        {
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
            catch(Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
            finally
            {
                wordApp.Quit(WdSaveOptions.wdPromptToSaveChanges, WdOriginalFormat.wdWordDocument, Type.Missing);
            }

            string text = string.Empty;
            using (StreamReader reader = new StreamReader(fileXML))
            {
                text = reader.ReadToEnd();
            }

            TicketTemplate ticketTemplate = new TicketTemplate
            {
                TemplateName = model.TemplateName,
                XML = text,
            };
            ticketTemplate.TicketTemplateBody = XMLParser.GetBody(text, ticketTemplate.Id);

            return ResultService.Success();
        }
    }
}
