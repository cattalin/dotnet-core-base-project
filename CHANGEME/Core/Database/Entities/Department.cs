using Infrastructure.Base;

namespace Core.Database.Entities
{
    public class Department : BaseEntityOfCompany
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Company Company { get; set; }
    }
}
