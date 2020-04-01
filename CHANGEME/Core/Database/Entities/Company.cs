using Infrastructure.Base;
using System.Collections.Generic;

namespace Core.Database.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<User> Users { get; set; }
        public virtual List<Account> Accounts { get; set; }
        public virtual List<Department> Departments { get; set; }
    }
}
