using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
     public interface ISessionRepo : IGenerecRepo<Session>
    {
        IEnumerable<Session> GetAllSesssionsWithTrainerAndCategories();
        Session? GetSessionByIdWithTrainerAndCategories(int Id);
        int GetCountOfBookedSlots(int SessionId);
    }
}
