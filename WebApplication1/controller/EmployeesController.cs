using Microsoft.AspNetCore.Mvc;
using pruebaTecnica.dto;
using pruebaTecnica.models;
using pruebaTecnica.services;

namespace pruebaTecnica.controllers
{
    [Route("employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdEmployee = await _employeeService.CreateEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.EmployeeId }, createdEmployee);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            var isUpdated = await _employeeService.UpdateEmployeeAsync(id, employee);

            if (isUpdated == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteEmployee(int id) 
        {
            var isDeleted = await _employeeService.DeleteEmployeeAsync(id);

            if (!isDeleted)
            {
                return false;
            }

            return isDeleted;
        }

        [HttpPost("getAllAfterContract")]
        public async Task<IActionResult> getAllAfterContract([FromBody] requestDateDto data)
        {
            var result = await _employeeService.GetEmployeesHiredAfterAsync(data.request);
            return Ok(result);
        }

    }
}
