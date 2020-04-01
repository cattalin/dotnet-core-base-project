using System;

namespace Infrastructure.Base
{
    public class BaseDatabaseOperationIdentifiers
    {
        public int DepartmentId { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }

        public int ResourceId { get; set; }

        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
