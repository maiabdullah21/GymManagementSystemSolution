using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class SessionRepo : GenerecRepo<Session>, ISessionRepo
    {
        private readonly GymDBContext _dbContext;

        public SessionRepo (GymDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Session> GetAllSesssionsWithTrainerAndCategories()
        {
            return _dbContext.Sessions.Include(X => X.SessionTrainer )
                                       .Include (X=>X.SessionCategory )
                                       .ToList();
        }

        public int GetCountOfBookedSlots( int SessionId)
        {
            return _dbContext.MemberSessions.Count(x => x.SessionId == SessionId);
        }

        public Session? GetSessionByIdWithTrainerAndCategories(int Id)
        {
            return _dbContext.Sessions.Include(X => X.SessionTrainer)
                                      .Include(X => X.SessionCategory)
                                      .FirstOrDefault(x => x.Id == Id);

        }
    }
}
