using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/Employees")]
    public class EmployeesControler : Controller
    {
        private readonly AppDbContext _appDbContext;

        public EmployeesControler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await _appDbContext.Employees.ToListAsync());

        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.id = Guid.NewGuid();
            await _appDbContext.Employees.AddAsync(employeeRequest);
            await _appDbContext.SaveChangesAsync();
            return Ok(employeeRequest);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;

            await _appDbContext.SaveChangesAsync();
            return Ok(employee);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            _appDbContext.Employees.Remove(employee);
            await _appDbContext.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
