using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDAL.Models
{
    public class UserRole
    {
        public long Id { get; set; }

        public long RoleId { get; set; }

        public long UserId { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
