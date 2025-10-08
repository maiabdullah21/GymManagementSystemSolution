using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    internal class MembershipRepo : IMembershipRepo
    {
        private readonly GymDBContext _dbContext;

        public MembershipRepo(GymDBContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public int Add(Membership membership)
        {
            _dbContext.Memberships.Add(membership);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var membership = _dbContext.Memberships.Find(id);
            if (membership == null)
                return 0;

            _dbContext.Memberships.Remove(membership);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Membership> GetAll() => _dbContext.Memberships.ToList();

        public Membership? GetById(int id) => _dbContext.Memberships.Find(id);

        public int Update(Membership membership)
        {
            _dbContext.Memberships.Update(membership);
            return _dbContext.SaveChanges();
        }
    }
}
