using AutoMapper;
using GymManagementBLL.services.interfaces;
using GymManagementBLL.ViewModels.SessionViewModel;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace GymManagementBLL.services.classes
{
    internal class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork , IMapper mapper  ) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ISessionRepo SessionRepo { get; }

        public bool CreateSession(CreateSessionViewModel createsession)
        {
            try
            {

                if (!IsTrainerExists(createsession.TrainerId)) return false;
                if (!IsCategoryExists(createsession.CategoryId)) return false;
                if (IsValidDate(createsession.StartDate, createsession.EndDate)) return false;

                var mappedsession = _mapper.Map<CreateSessionViewModel,Session>(createsession);
                _unitOfWork.SessionRepo.Add(mappedsession);
                return _unitOfWork.SaveChanges() > 0;
            }catch {  return false; }
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Sessions = _unitOfWork.SessionRepo.GetAllSesssionsWithTrainerAndCategories();
            if (Sessions == null || !Sessions.Any()) return [];



            #region Manual mapping
            //return Sessions.Select(session => new SessionViewModel()
            //{
            //    Id = session.Id,
            //    Capacity = session.Capacity,
            //    Description = session.Description,
            //    EndDate = session.EndDate,
            //    StartDate = session.StartDate,
            //    TrainerName = session.SessionTrainer.Name,
            //    CategoryName = session.SessionCategory.CategoryName,
            //    AvailableSlots = session.Capacity - _unitOfWork.SessionRepo.GetCountOfBookedSlots(session.Id),

            //});
            #endregion

            #region Auto Mapping
            var MappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(Sessions);
            return MappedSessions;
            #endregion
        }

        public SessionViewModel? GetSessionById(int id)
        {
            var Session = _unitOfWork.SessionRepo.GetSessionByIdWithTrainerAndCategories(id);
            if (Session == null) return null;
            //return new SessionViewModel()
            //{
            //    Id = Session.Id,
            //    Capacity = Session.Capacity,
            //    Description = Session.Description,
            //    EndDate = Session.EndDate,
            //    StartDate = Session.StartDate,
            //    TrainerName = Session.SessionTrainer.Name,
            //    CategoryName = Session.SessionCategory.CategoryName,
            //    AvailableSlots = Session.Capacity - _unitOfWork.SessionRepo.GetCountOfBookedSlots(Session.Id),
            //};

            var MappedSession = _mapper.Map<Session , SessionViewModel>(Session);
            return MappedSession;
        }

        public UpdateSessionViewModel? SessionToUpdate(int SessionId)
        {
            var session = _unitOfWork.SessionRepo.GetbyId(SessionId);
            if (!IsSessionAvailableToUpdate(session!)) return null;
            return _mapper.Map<UpdateSessionViewModel>(session);

        }

        public bool UpdateSession(UpdateSessionViewModel updatesession, int SessionId)
        {
           try
            {
                var session = _unitOfWork.SessionRepo.GetbyId(SessionId);
                if (!IsSessionAvailableToUpdate(session!)) return false;
                if (!IsTrainerExists(updatesession.TrainerId)) return false;
                if (IsValidDate(updatesession.StartDate, updatesession.EndDate)) return false;
                _mapper.Map(updatesession, session);
                session.UpdatedAt = DateTime.Now;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }

        public bool DRemoveSession(int SessionId)
        {
            try
            {
                var session = _unitOfWork.SessionRepo.GetbyId(SessionId);
                if (!IsSessionAvailableToRemove(session!)) return false;

                _unitOfWork.SessionRepo.Delete(session!);

                return _unitOfWork.SaveChanges() > 0;


            }
            catch
            {
                return false;
            }
        }

        #region Helpers
        private bool IsTrainerExists(int id)
        {
            return _unitOfWork.GetGenerecRepo<Trainer>().GetbyId(id) is not null;
        }

        private bool IsCategoryExists(int id)
        {
            return _unitOfWork.GetGenerecRepo<Category>().GetbyId(id) is not null;
        }

        private bool IsValidDate (DateTime start , DateTime end)
        {
            return start < end && start > DateTime.Now;
        }

       private bool IsSessionAvailableToUpdate (Session session)
        {
            if (session == null) return false;
            if (session.EndDate < DateTime.Now) return false;
            if (session.StartDate <= DateTime.Now) return false;
            var HasActiveBooking = _unitOfWork.SessionRepo.GetCountOfBookedSlots(session.Id) > 0;
            if (!HasActiveBooking ) return false;
            return true;  
        }
        private bool IsSessionAvailableToRemove(Session session)
        {
            if (session == null) return false;
            if (session.StartDate > DateTime.Now) return false;
            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;
            var HasActiveBooking = _unitOfWork.SessionRepo.GetCountOfBookedSlots(session.Id) > 0;
            if (!HasActiveBooking) return false;
            return true;
        }

        #endregion
    }
}
