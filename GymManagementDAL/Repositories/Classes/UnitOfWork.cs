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
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly GymDBContext _dBContext;
        //private readonly ISessionRepo _sessionRepo;

        public UnitOfWork( GymDBContext dBContext , ISessionRepo sessionRepo ) {
            _dBContext = dBContext;
            SessionRepo = sessionRepo;
        }
        private readonly Dictionary<Type , object> _repositories = new Dictionary<Type , object>();

        public ISessionRepo SessionRepo { get; }

        ISessionRepo IUnitOfWork.SessionRepo => throw new NotImplementedException();

        public IGenerecRepo<TEntity> GetGenerecRepo<TEntity>() where TEntity : BaseEntity, new()
        {
            var TentityType = typeof(TEntity);
            if ( _repositories.ContainsKey(TentityType ))
                return (IGenerecRepo<TEntity>) _repositories[TentityType];
             
            var NewRepo = new GenerecRepo<TEntity>(_dBContext);
            _repositories[TentityType] = NewRepo;
            return NewRepo;

        }

        public int SaveChanges()
        {
            return _dBContext.SaveChanges();
        }

    }
}
