﻿using DepartmentModel;
using System.Collections.Generic;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceInterfaces.Interfaces
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