using GymManagementBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.interfaces
{
    internal interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanDetailes(int PlanId);
        UpdatePlanViewModel? GetPlanToUpdate (int PlanId);
        bool UpdatePlane (int  PlanId, UpdatePlanViewModel  updatePlan);
        bool ToggelStatus (int  PlanId);
    }
}
