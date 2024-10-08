using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int? PositionID { get; set; }
        public Position? Position { get; set; }
        public string IPAddress { get; set; } = string.Empty;
        public string IPCountryCode { get; set; } = string.Empty;
    }
}
