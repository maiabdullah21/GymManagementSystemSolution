using GymManagementBLL.services.interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.classes
{
    internal class TrainerService : ItrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
           try
            {
                var Repo = _unitOfWork.GetGenerecRepo<Trainer>();
                if (IsEmailExists(createTrainer.Email) || IsPhoneExists(createTrainer.Phone)) return false;

                var Trainer = new Trainer()
                {
                    Email = createTrainer.Email,
                    Phone = createTrainer.Phone,
                    DateOfBirth = createTrainer.DateOfBirth,
                    Specialties = createTrainer.Specialties,
                    Gender = createTrainer.Gender,
                    Address = new Address()
                    {
                        BuildingNumber = createTrainer.BuildingNumber,
                        Street = createTrainer.Street,
                        City = createTrainer.City,
                    }
                };
                Repo.Add(Trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch {return false; }
        }

        public IEnumerable<TrainerViewModel> GetAllTrainer()
        {
            var trainers = _unitOfWork.GetGenerecRepo<Trainer>().GetAll();
            if (trainers == null || !trainers.Any()) return [];

            return trainers.Select(x => new TrainerViewModel(){
                Email = x.Email,
                Phone = x.Phone,
                Id = x.Id,
                Specialties = x.Specialties.ToString(),
            });
        }

        public TrainerViewModel? GetTrainerDetailes(int TrainerId)
        {
            var trainer =_unitOfWork.GetGenerecRepo<Trainer>().GetbyId(TrainerId);
            if (trainer == null) return null;
            return new TrainerViewModel()
            {
                Email = trainer.Email,
                Phone = trainer.Phone,
                Name = trainer.Name,
                Specialties = trainer.Specialties.ToString(),

            };
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId)
        {
            var trainer = _unitOfWork.GetGenerecRepo<Trainer>().GetbyId(TrainerId);
            if (trainer == null) return null;
            return new TrainerToUpdateViewModel()
            {
                Email = trainer.Email,
                Phone = trainer.Phone,
                BuildingNumber = trainer.Address.BuildingNumber,
                Street = trainer.Address.Street,
                City = trainer.Address.City,
                Specialties = trainer.Specialties,
            };
        }

        public bool RemoveTrainer(int TrainerId)
        {
           var ex = _unitOfWork.GetGenerecRepo<Trainer>();
            var RemoveTrainer = ex.GetbyId(TrainerId);
            ex.Delete(RemoveTrainer);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool UpdateTrainerDetailes(UpdateTrainerViewModel updateTrainer, int TrainerId)
        {
            var ex = _unitOfWork.GetGenerecRepo<Trainer>();
            var updatetraine = ex.GetbyId(TrainerId);
            if (updatetraine == null || IsEmailExists( updateTrainer.Email) || IsPhoneExists(updateTrainer.Phone) ) return false;
            updatetraine.Email = updateTrainer.Email;
            updatetraine.Phone = updateTrainer.Phone;
            updatetraine.Address.BuildingNumber = updateTrainer.BuildingNumber;
            updatetraine.Address.Street = updateTrainer.Street;
            updatetraine.Address.City = updateTrainer.City;
            updatetraine.UpdatedAt = DateTime.Now;

            ex.Update( updatetraine );
            return _unitOfWork.SaveChanges() > 0;
        }
    
    #region Helper Methods
       private bool IsEmailExists(string Email)
        {
            return _unitOfWork.GetGenerecRepo<Trainer>().GetAll(X => X.Email == Email).Any();

        }

        private bool IsPhoneExists(string Phone)
        {
            return _unitOfWork.GetGenerecRepo<Trainer>().GetAll(X => X.Phone == Phone).Any();

        }
        private bool HasActiveSession(int TrainerId)
        {
            return _unitOfWork.GetGenerecRepo<Trainer>().
                GetAll(X => X.Id == TrainerId && X.TrainerSessions != null).Any();
        }
        #endregion
    }
}
