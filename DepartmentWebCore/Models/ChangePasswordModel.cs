namespace DepartmentWebCore.Models
{
    public class ChangePasswordModel
    {
        public string Password { get; set; }

        public string NewPassword { get; set; }

        public string Confirmation { get; set; }
    }
}