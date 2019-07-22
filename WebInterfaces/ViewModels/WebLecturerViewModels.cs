using System;
using System.Collections.Generic;
using System.Text;
using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class WebLecturerPageViewModel : PageSettingListViewModel<WebLecturerViewModel> { }

    public class WebLecturerViewModel : PageSettingElementViewModel
    {
        public Guid LecturerPostId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Email { get; set; }

        public string Post { get; set; }

        public string LecturerPost { get; set; }

        public string Rank { get; set; }

        public string Rank2 { get; set; }

        public string Description { get; set; }

        public byte[] Photo { get; set; }

        public List<Tuple<Guid, string>> Disiplines { get; set; }

        public string FullName
        {
            get
            {
                StringBuilder fullname = new StringBuilder(LastName);
                if (FirstName.Length > 0)
                {
                    fullname.Append(string.Format(" {0}.", FirstName[0]));
                }
                if (Patronymic.Length > 0)
                {
                    fullname.Append(string.Format(" {0}.", Patronymic[0]));
                }
                return fullname.ToString();
            }
        }
    }
}