using Azure.Core;
using GymManagementBLL.services.interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.classes
{
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService( IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GetGenerecRepo<Plan>().GetAll();
            if (Plans == null || !Plans.Any()) return [];

            return Plans.Select(p => new PlanViewModel()
            {
                Description = p.Description,
                DurationDays = p.DurationDays,
                Id = p.Id,
                Name = p.Name,
                IsActive = p.IsActive,
                Price = p.Price,
            });
        }

        public PlanViewModel? GetPlanDetailes(int PlanId)
        {
            var plan = _unitOfWork.GetGenerecRepo<Plan>().GetbyId(PlanId);
            if (plan == null) return null;
            return new PlanViewModel()
            {
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Id = plan.Id,
                Name = plan.Name,
                IsActive = plan.IsActive,
                Price = plan.Price,
            };

        }

        public UpdatePlanViewModel? GetPlanToUpdate(int PlanId)
        {
            var plan = _unitOfWork.GetGenerecRepo<Plan>().GetbyId(PlanId);
            if (plan == null || plan.IsActive == false || HasActiveMemperShip(PlanId)) return null;
            return new UpdatePlanViewModel()
            {
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                //PLanName = plan.Name,
                Price = plan.Price,
                
            };

        }

        public bool ToggelStatus(int PlanId)
        {
            var plan = _unitOfWork.GetGenerecRepo<Plan>().GetbyId(PlanId);
            if (plan == null || HasActiveMemperShip(PlanId)) return false;
            plan.IsActive = plan.IsActive == true ? false : true;

            try
            {
                _unitOfWork.GetGenerecRepo<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }catch {  return false; }
        }

        public bool UpdatePlane(int PlanId, UpdatePlanViewModel updatePlan)
        {
           try
            {
                var plan = _unitOfWork.GetGenerecRepo<Plan>().GetbyId(PlanId);
                if (plan == null || HasActiveMemperShip(PlanId)) return false;
                (plan.Description, plan.DurationDays, plan.Price, plan.UpdatedAt) = (updatePlan.Description, updatePlan.DurationDays, updatePlan.Price, DateTime.Now);

                _unitOfWork.GetGenerecRepo<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch {  return false; }

        }
        #region Helper Methods
        private bool HasActiveMemperShip(int PlanId)
        {
            return _unitOfWork.GetGenerecRepo<Membership>().
                GetAll(X => X.PlanId ==PlanId && X.status == "Active" ).Any();
        }
        #endregion
    }
}
