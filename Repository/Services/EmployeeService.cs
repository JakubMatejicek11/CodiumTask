using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Shared.DTOs;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{

    public class EmployeeService
    {
        private readonly DataContext _dataContext;
        private readonly GeoLocationService _geoLocationService;

        public EmployeeService(DataContext context, GeoLocationService geoLocationService)
        {
            _dataContext = context;
            _geoLocationService = geoLocationService;
        }

        public async Task<List<EmployeeDTO>> GetEmployeesAsync()
        {
            List<Employee> employees = await _dataContext.Employees.Include(x => x.Position).ToListAsync();
            return employees.Select(x => new EmployeeDTO
            {
                EmployeeID = x.EmployeeID,
                Name = x.Name,
                Surname = x.Surname,
                BirthDate = x.BirthDate,
                IPAddress = x.IPAddress,
                IPCountryCode = x.IPCountryCode,
                PositionName = x.Position == null ? null : x.Position.PositionName
            }).ToList();
        }

        public async Task<EmployeeAddEditDTO?> GetEmployeeAsync(int id)
        {
            Employee? employee = await _dataContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return null;
            }
            return new EmployeeAddEditDTO()
            {
                EmployeeID = employee.EmployeeID,
                Name = employee.Name,
                Surname = employee.Surname,
                BirthDate = employee.BirthDate,
                IPAddress = employee.IPAddress,
                PositionID = employee.PositionID
            };
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            Employee? employee = await _dataContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }
            _dataContext.Remove(employee);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<string> CheckIPAsync(string ipAddress)
        {
            string countryCode = await _geoLocationService.GetCountryCodeByIP(ipAddress);
            if (countryCode == "Unknown")
            {
                return "Unknown";
            }
            return countryCode;
        }

        public async Task<bool> UpdateEmployeeAsync(int id, EmployeeAddEditDTO updatedEmployee)
        {
            Employee? employee = await _dataContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }
            if (updatedEmployee.IPAddress != employee.IPAddress)
            {
                employee.IPCountryCode = await CheckIPAsync(updatedEmployee.IPAddress);
            }
            employee.Name = updatedEmployee.Name;
            employee.Surname = updatedEmployee.Surname;
            employee.BirthDate = updatedEmployee.BirthDate;
            employee.IPAddress = updatedEmployee.IPAddress;
            employee.PositionID = updatedEmployee.PositionID;
            _dataContext.Update(employee);
            await _dataContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> AddEmployeeAsync(EmployeeAddEditDTO addedEmployee)
        {
            string countryCode = await _geoLocationService.GetCountryCodeByIP(addedEmployee.IPAddress);
            Employee newEmployee = new Employee()
            {
                Name = addedEmployee.Name,
                Surname = addedEmployee.Surname,
                BirthDate = addedEmployee.BirthDate,
                IPAddress = addedEmployee.IPAddress,
                IPCountryCode = countryCode,
                PositionID = addedEmployee.PositionID
            };
            _dataContext.Add(newEmployee);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmployeeExistsAsync(string name, string surname, DateTime birthDate)
        {
            return await _dataContext.Employees.AnyAsync(e => e.Name == name
            && e.Surname == surname
            && e.BirthDate == birthDate);
        }

        public async Task UploadEmployeesAsync(List<EmployeeUploadDTO> employees)
        {
            HashSet<string> uniqueEmployees = new HashSet<string>();
            foreach (EmployeeUploadDTO employeeDTO in employees)
            {
                string employeeKey = $"{employeeDTO.Name}-{employeeDTO.Surname}-{employeeDTO.BirthDate.ToString("yyyy-MM-dd")}";
                if (uniqueEmployees.Contains(employeeKey))
                {
                    continue;
                }
                uniqueEmployees.Add(employeeKey);
                if (await EmployeeExistsAsync(employeeDTO.Name, employeeDTO.Surname, employeeDTO.BirthDate))
                {
                    continue;
                }
                Position? position = _dataContext.Positions.FirstOrDefault(p => p.PositionName == employeeDTO.Position);
                int? positionId = position?.PositionID;
                string ipCountryCode = await _geoLocationService.GetCountryCodeByIP(employeeDTO.IpAddress);
                var employee = new Employee
                {
                    Name = employeeDTO.Name,
                    Surname = employeeDTO.Surname,
                    BirthDate = employeeDTO.BirthDate,
                    IPAddress = employeeDTO.IpAddress,
                    IPCountryCode = ipCountryCode
                };
                _dataContext.Employees.Add(employee);
            }
            await _dataContext.SaveChangesAsync();
        }
    }
}
