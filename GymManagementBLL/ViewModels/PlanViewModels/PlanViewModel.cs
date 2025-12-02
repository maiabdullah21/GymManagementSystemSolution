using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.PlanViewModels
{
    internal class PlanViewModel
    {
        public int Id { get; set; }
          public String Name { get; set; } = null!;
        public String Description { get; set; } = null!;

        public int DurationDays { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }
    }
}
