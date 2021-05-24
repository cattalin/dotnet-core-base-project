using Infrastructure.Base;

namespace Core.Database.Entities
{
    public class User : BaseEntityOfCompany
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual Account Account { get; set; }
        public virtual Company Company { get; set; }
    }
}
