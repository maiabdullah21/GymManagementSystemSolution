using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class Member : GymUser
    {
        //joindate == creates at date in the base entity
        public string? Photo {  get; set; }
        #region Relationships

        #region Member - HealthRecord
        public HealthRecord HealthRecord { get; set; } = null!;

        #endregion

        #region Member-MemberShip
        public ICollection<Membership> Memberships { get; set; } = null!;
        #endregion

        #region Member-session
        public ICollection<MemberSession> MembersSession { get; set; } = null!;
        #endregion


        #endregion
    }
}
