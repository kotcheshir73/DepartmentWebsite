using System;

namespace WebInterfaces.BindingModels
{
    public class WebAuthenticationLoginBindingModel
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }

    public class WebAuthenticationChangePassword
    {
        public Guid Id { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}