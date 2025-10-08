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
    internal class HealthRecordRepo : IHealthRecordRepo
    {
        private readonly GymDBContext _dbContext;

        public HealthRecordRepo(GymDBContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public int Add(HealthRecord healthRecord)
        {
            _dbContext.HealthRecords.Add(healthRecord);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var healthRecord = _dbContext.HealthRecords.Find(id);
            if (healthRecord == null)
                return 0;

            _dbContext.HealthRecords.Remove(healthRecord);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<HealthRecord> GetAll() => _dbContext.HealthRecords.ToList();

        public HealthRecord? GetById(int id) => _dbContext.HealthRecords.Find(id);

        public int Update(HealthRecord healthRecord)
        {
            _dbContext.HealthRecords.Update(healthRecord);
            return _dbContext.SaveChanges();
        }
    }
}
