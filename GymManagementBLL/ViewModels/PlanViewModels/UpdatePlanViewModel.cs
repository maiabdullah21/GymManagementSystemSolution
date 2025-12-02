using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.PlanViewModels
{
    internal class UpdatePlanViewModel
    {
       

        [Required(ErrorMessage = "Description is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 50 characters")]
        public String Description { get; set; } = null!;

        [Required(ErrorMessage = "Duration Days is required")]
        [Range(1,365, ErrorMessage = "Duration Days Must Be Between 1 and 365")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.1,10000, ErrorMessage = "Duration Days Must Be Between 0.1 and 10000")]
        public decimal Price { get; set; }
    }
}
