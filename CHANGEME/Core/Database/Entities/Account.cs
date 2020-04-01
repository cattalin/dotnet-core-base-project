using Infrastructure.Base;

namespace Core.Database.Entities
{
    public class Account : BaseEntityOfCompany
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public virtual User User { get; set; }
        public virtual Company Company{ get; set; }
    }
}
