using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EjercicioPromart.Entitys;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EjercicioPromart.Context;

namespace EjercicioPromart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Models.EmployeeDAO employeeDAO;
        private readonly ILogger _logger;
        public EmployeesController(ApplicationDbContext context, ILoggerFactory logger)
        {
            _context = context;
            employeeDAO = new  Models.EmployeeDAO(context);
            _logger = logger.CreateLogger("MyCategory");
        }
        //GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            _logger.LogInformation(MyLogEvents.GetItem, "Getting items ");
            return  Ok(employeeDAO.GetEmployee());
        }
        //GET: api/Employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _logger.LogInformation(MyLogEvents.GetItem, "Getting item {Id}", id);
            if (employee == null)
            {
                _logger.LogWarning(MyLogEvents.GetItemNotFound, "Get({Id}) NOT FOUND", id);
                return NotFound();
            }
            return employee;
        }
        //PUT: api/Employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.id)
            {
                _logger.LogWarning(MyLogEvents.BadRequest, "Get({Id}) BAD REQUEST", id);
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                _logger.LogInformation(MyLogEvents.UpdateItem, "Updating item {Id}", id);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (EmployeeExists(id))
                {
                    _logger.LogWarning(MyLogEvents.UpdateItemNotFound, "Get({Id}) Item NOT FOUND", id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();
            _logger.LogInformation(MyLogEvents.InsertItem, "Creating item {employee}", employee);
           
            return CreatedAtAction("GetEmployee", new { id = employee.id }, employee);
        }

        //DELETE: api/Employee/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                _logger.LogWarning(MyLogEvents.DeleteItemNotFound, "Delete({Id}) Item NOT FOUND", id);
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            _logger.LogWarning(MyLogEvents.DeleteItem, "Delete({Id}) Item DELETED", id);
            return employee;
        }
        [HttpGet("SalaryRange{value1}&{value2}")]
        public async Task<ActionResult<Employee>> SearchEmployee(int value1, int value2)
        {
            var employee =  Ok(employeeDAO.GetSalaryRange(value1, value2));

            _logger.LogInformation(MyLogEvents.GetItem, "Getting item Range Salary {value1} and {value2}, \n {employee}", value1, value2, employee);

            if (employee == null)
            {
                _logger.LogWarning(MyLogEvents.GetItemNotFound, "NOT FOUND Items");
                return NotFound();
            }
            return employee;
        }
        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.id == id);
        }
    }
}
