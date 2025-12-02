using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IGenerecRepo<TEntity> where TEntity : BaseEntity , new()
    {
        TEntity? GetbyId(int id);
        
        IEnumerable<TEntity>GetAll(Func<TEntity,bool>? condtion= null);
         void Add (TEntity entity);
        void Update (TEntity entity);
        void Delete (TEntity entity);
    }
}
