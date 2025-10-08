
using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GymManagementDAL.Data.Repositories.Implementations
{
    internal class TrainerRepo : ITrainerRepo
    {
        private readonly GymDBContext _dbContext;

        public TrainerRepo(GymDBContext _dbContext)
        {

            this._dbContext = _dbContext;

        }

        public int Add(Trainer trainer)
        {
            _dbContext.Trainers.Add(trainer);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var trainer = _dbContext.Trainers.Find(id);
            if (trainer == null)
                return 0;

            _dbContext.Trainers.Remove(trainer);
            return _dbContext.SaveChanges();
        }

        public int Delete(Trainer trainer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Trainer> GetAll() => _dbContext.Trainers.ToList();

        public Trainer? GetById(int id) => _dbContext.Trainers.Find(id);

        public int Update(Trainer trainer)
        {
            _dbContext.Trainers.Update(trainer);
            return _dbContext.SaveChanges();
        }
    }
}
