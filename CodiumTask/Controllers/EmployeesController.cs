using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Services;
using Shared.DTOs;
using Shared.Entities;

namespace CodiumTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;
        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDTO>>> GetAllEmployees()
        {
            List<EmployeeDTO> employees = await _employeeService.GetEmployeesAsync();
            if (employees.Count == 0)
            {
                return NotFound("There are currently no employees");
            }
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeAddEditDTO>> GetOneEmployee(int id)
        {
            EmployeeAddEditDTO? employee = await _employeeService.GetEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound("Employee not found!");
            }
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            bool success = await _employeeService.DeleteEmployeeAsync(id);
            if (success)
            {
                return Ok();
            }
            return NotFound("Employee not found!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, EmployeeAddEditDTO updatedEmployee)
        {
            bool success = await _employeeService.UpdateEmployeeAsync(id, updatedEmployee);
            if (success)
            {
                return Ok();
            }
            return NotFound("Employee not found!");
        }

        [HttpGet("checkIP")]
        public async Task<IActionResult> CheckIP(string ipAddress)
        {
            string success = await _employeeService.CheckIPAsync(ipAddress);
            if (success != "Unknown")
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(EmployeeAddEditDTO addedEmployeeAddEditDTO)
        {
            bool success = await _employeeService.AddEmployeeAsync(addedEmployeeAddEditDTO);
            if (success)
            {
                return Ok();
            }
            return BadRequest("There was an error while trying to add Employee!");
        }

        [HttpGet("employeeExists")]
        public async Task<IActionResult> EmployeeExists(string name, string surname, DateTime birthDate)
        {
            bool exists = await _employeeService.EmployeeExistsAsync(name, surname, birthDate);
            return Ok(exists);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadEmployees([FromBody] EmployeeUploadParentDTO uploadDTOParent)
        {
            if (uploadDTOParent == null || !uploadDTOParent.Employees.Any())
            {
                return BadRequest("No employees found in the uploaded file!");
            }
            await _employeeService.UploadEmployeesAsync(uploadDTOParent.Employees);
            return Ok("Employees uploaded successfully.");
        }
    }
}
