using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class EmployeeAddEditDTO
    {
        public int? EmployeeID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression("^[A-Za-zÀ-ž]+$", ErrorMessage = "Name can only contain letters and diacritics.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Surname is required")]
        [RegularExpression("^[A-Za-zÀ-ž]+$", ErrorMessage = "Surname can only contain letters and diacritics.")]
        public string Surname { get; set; } = string.Empty;
        [Required(ErrorMessage = "Birth Date is required")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "IP Address is required")]
        [RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "Invalid IP Address format")]
        public string IPAddress { get; set; } = string.Empty;
        [Required(ErrorMessage = "Position is required")]
        public int? PositionID { get; set; }

    }
}
