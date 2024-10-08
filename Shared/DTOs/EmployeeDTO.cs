using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string IPAddress { get; set; } = string.Empty;
        public string IPCountryCode { get; set; } = string.Empty;
        public string? PositionName { get; set; } = string.Empty;
    }
}
