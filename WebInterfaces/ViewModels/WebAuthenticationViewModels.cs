using System.Collections.Generic;

namespace WebInterfaces.ViewModels
{
    public class WebAuthenticationLoginViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public List<string> UserRoles { get; set; }
    }
}