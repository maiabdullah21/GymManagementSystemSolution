using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IMembershipRepo
    {
        IEnumerable<Membership> GetAll();
        Membership? GetById(int id);
        int Add(Membership membership);
        int Update(Membership membership);
        int Delete(int id);
    }
}
