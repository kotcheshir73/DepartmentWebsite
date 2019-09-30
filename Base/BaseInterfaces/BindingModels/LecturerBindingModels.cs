using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace BaseInterfaces.BindingModels
{
    public class LecturerGetBindingModel : PageSettingGetBinidingModel
    {
        public string Abbreviation { get; set; }
    }

    public class LecturerSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid LecturerPostId { get; set; }

        public string FirstName { get; set; }

        [Required(ErrorMessage = "required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "required")]
        public string Patronymic { get; set; }

        public string Abbreviation { get; set; }

        [Required(ErrorMessage = "required")]
        public DateTime DateBirth { get; set; }

        [Required(ErrorMessage = "required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "required")]
        public string MobileNumber { get; set; }

        public string HomeNumber { get; set; }

        [Required(ErrorMessage = "required")]
        public string Post { get; set; }

        [Required(ErrorMessage = "required")]
        public string Rank { get; set; }

        [Required(ErrorMessage = "required")]
        public string Rank2 { get; set; }

        public string Description { get; set; }

        public byte[] Photo { get; set; }

        public bool OnlyForPrivate { get; set; }
    }
}