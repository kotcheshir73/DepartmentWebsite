using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace BaseInterfaces.BindingModels
{
    public class DisciplineBlockGetBindingModel : PageSettingGetBinidingModel { }

    public class DisciplineBlockSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public string Title { get; set; }

        public string DisciplineBlockBlueAsteriskName { get; set; }

        public bool DisciplineBlockUseForGrouping { get; set; }

        [Required(ErrorMessage = "required")]
        public int DisciplineBlockOrder { get; set; }

        public string DisciplineBlockBlueAsteriskCode { get; set; }
    }
}