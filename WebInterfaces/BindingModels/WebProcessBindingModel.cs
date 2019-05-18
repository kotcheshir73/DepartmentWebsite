using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tools.BindingModels;

namespace WebInterfaces.BindingModels
{
    public class WebProcessGetBindingModel : PageSettingGetBinidingModel
    {
                
    }

    public class WebProcessSetBindingModel : PageSettingSetBinidingModel
    {
        
    }

    public class WebProcessFolderLoadSetBindingModel
    {
        public string DisciplineName { get; set; }

        public string Semestr { get; set; }

        public string TimeNorm { get; set; }
    }

    public class WebProcessDisciplineForDownloadGetBindingModel
    {
        public string DisciplineName { get; set; }
    }
}
