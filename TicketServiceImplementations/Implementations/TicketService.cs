using Microsoft.Office.Interop.Word;
using System;
using System.IO;
using TicketModels.Models;
using TicketServiceImplementations.Helpers;

namespace TicketServiceImplementations.Implementations
{
    public class TicketService
    {
        public void LoadTemplate(string fileName, string templateName)
        {
            // переводим doc-файл в формат xml и обрабатываем его
            object missing = System.Reflection.Missing.Value;
            Application wordApp = new Application
            {
                Visible = false,
                ScreenUpdating = false
            };
            
            Object xmlFormat = WdSaveFormat.wdFormatXML;
            Object docFile = fileName;
            string fileXML = fileName + ".xml";
            Object xmlFile = fileXML;

            Document doc = wordApp.Documents.Open(ref docFile, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            
            doc.SaveAs(ref xmlFile, ref xmlFormat, ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

            doc.Close(ref missing, ref missing, ref missing);

            wordApp.Quit(WdSaveOptions.wdPromptToSaveChanges, WdOriginalFormat.wdWordDocument, Type.Missing);

            string text = string.Empty;
            using (StreamReader reader = new StreamReader(fileXML))
            {
                text = reader.ReadToEnd();
            }

            TicketTemplate ticketTemplate = new TicketTemplate
            {
                TemplateName = templateName,
                XML = text,
            };
            ticketTemplate.TicketTemplateBody = XMLParser.GetBody(text, ticketTemplate.Id);
        }
    }
}