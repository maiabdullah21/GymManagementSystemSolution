using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class Plan : BaseEntity
    {
        public String Name { get; set; } = null!;
        public String Description { get; set; } = null!;

        public int DurationDays { get; set; }

        public Decimal Price { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Membership> PlanMembers { get; set; } = null!;

    }
}
