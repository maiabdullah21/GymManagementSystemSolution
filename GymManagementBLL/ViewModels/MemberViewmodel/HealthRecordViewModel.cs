using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.MemberViewmodel
{
    internal class HealthRecordViewModel
    {
        [Required(ErrorMessage = "Height is required")]
        [Range(0.1,300,ErrorMessage ="Height must be between 0.1 and 300 cm")]
        public decimal Height { get; set; }
        [Required(ErrorMessage = "Weight is required")]
        [Range(0.1, 500, ErrorMessage = "Height must be between 0.1 and 500 cm")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Bolld Type is required")]
        [StringLength(3, ErrorMessage = "Blood Type must be at most 3 charachters")]
        public string BloodType { get; set; } = null!;

        public string ? Note { get; set; }
    }
}
