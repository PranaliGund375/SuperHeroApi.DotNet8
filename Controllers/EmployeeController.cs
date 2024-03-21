using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroApi.DotNet8.Data;
using SuperHeroApi.DotNet8.Entities;

namespace SuperHeroApi.DotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployee()
        {
            var emp = await _context.Employees.ToListAsync();
            return Ok(emp);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Employee>>> GetEmployee(int id)
        {
            var emp = _context.Employees.FindAsync(id);
            if (emp == null)
                return NotFound("Employee is not found");

            return Ok(emp);
        }
        
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee emp)
        {
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();
            return Ok(emp);
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee updatedEmp)
        {
            var employee = await _context.Employees.FindAsync(updatedEmp.Id);
            if (employee == null)
                return NotFound("Employee is not found");

            employee.FirstName = updatedEmp.FirstName;
            employee.LastName = updatedEmp.LastName;
            employee.Email = updatedEmp.Email;

            await _context.SaveChangesAsync();
            //return Ok(employee);
            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound("Employee is not found");

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok(await _context.Employees.ToListAsync());
        }


    }
}
