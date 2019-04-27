using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using System.Collections.Generic;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface ITicketProcess
    {
        ResultService<TicketTemplateViewModel> LoadTemplate(TicketProcessLoadTemplateBindingModel model);

        ResultService SaveTemplate(TicketProcessLoadTemplateBindingModel model);

        ResultService LoadQuestions(TicketProcessLoadQuestionsBindingModel model);

        ResultService MakeTickets(TicketProcessMakeTicketsBindingModel model);

        ResultService SynchronizeBlocksByTemplate(TicketProcessSynchronizeBlocksByTemplateBindingModel model);

        ResultService UploadTickets(TicketProcessUploadTicketsBindingModel model);

        ResultService<List<TicketProcessGetParagraphDatasViewModel>> GetParagraphDatas(TicketProcessGetParagraphDatasBindingModel model);
    }
}