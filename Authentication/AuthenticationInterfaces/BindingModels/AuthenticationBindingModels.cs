using System;

namespace AuthenticationInterfaces.BindingModels
{
    public class ChangePasswordBindingModels
    {
        public Guid Id { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
