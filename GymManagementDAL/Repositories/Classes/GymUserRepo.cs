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
    internal class GymUserRepo : IGymUserRepo
    {
        private readonly GymDBContext _dbContext;

        public GymUserRepo(GymDBContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public int Add(GymUser gymUser)
        {
            _dbContext.GymUsers.Add(gymUser);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var gymUser = _dbContext.GymUsers.Find(id);
            if (gymUser == null)
                return 0;

            _dbContext.GymUsers.Remove(gymUser);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<GymUser> GetAll() => _dbContext.GymUsers.ToList();

        public GymUser? GetById(int id) => _dbContext.GymUsers.Find(id);

        public int Update(GymUser gymUser)
        {
            _dbContext.GymUsers.Update(gymUser);
            return _dbContext.SaveChanges();
        }
    }
}
