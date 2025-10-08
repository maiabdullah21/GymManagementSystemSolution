using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IGymUserRepo
    {
        IEnumerable<GymUser> GetAll();
        GymUser? GetById(int id);
        int Add(GymUser gymUser);
        int Update(GymUser gymUser);
        int Delete(int id);
    }
}
