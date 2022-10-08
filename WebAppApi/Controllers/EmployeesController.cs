using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppApi.Repositories;


namespace WebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _employeeRepository.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data from Database");
            }

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployee(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data from Database");
            }

        }
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                var CreatedEmployee = await _employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee), new { id = CreatedEmployee.Id }, CreatedEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error in Retrieving Data from Database");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id,Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("Id MissMatch");
                }
                var employeeUpdate = await _employeeRepository.GetEmployee(id);
                if (employeeUpdate == null)
                {
                    return NotFound(employee.Id);
                }
                return await _employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data from Database");
            }
            
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employeeDelete = await _employeeRepository.GetEmployee(id);
                if (employeeDelete == null)
                {
                    return NotFound($"Employee Id={id} not Found");
                }
                return await _employeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data from Database");
            }

        }


    }
}





