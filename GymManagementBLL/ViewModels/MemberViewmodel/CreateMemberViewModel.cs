using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.MemberViewmodel
{
    internal class CreateMemberViewModel
    {
        [Required (ErrorMessage = "Name is required")]
        [StringLength(50,MinimumLength =2, ErrorMessage ="Name must be between 2 and 50 characters")]
        [RegularExpression (@"^[a-zA-Z\s]+$",ErrorMessage ="Name only contain letters and white spaces")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [DataType (DataType.EmailAddress)]
        [EmailAddress (ErrorMessage ="Invalid Email Format")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 100 characters")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone (ErrorMessage = "Invalid Phone Format")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(010 || 011 || 012 || 015)\d{8}$", ErrorMessage ="Phone number must be valid Egyptian phone number")]
         public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Date Of birth  is required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; } 

        [Required(ErrorMessage = "Building Number is required")]
        [Range (1,1000 , ErrorMessage = "Building  Number Must Be Between 1 and 1000")]
        public int BuildingNumber { get; set; }
        [Required(ErrorMessage = "Street is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "street must be between 2 and 30 characters")]
        public string street { get; set; } = null!;
        [Required(ErrorMessage = "City is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City must be between 2 and 30 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City only contain letters and white spaces")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Health Record is required")]
        public HealthRecordViewModel HealthRecord { get; set; } = null!;
    }
}
