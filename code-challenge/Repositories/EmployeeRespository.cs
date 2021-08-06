using System;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            //Task - 1
            //DirectReports property was not being included when retrieving the Employees from _employeeContext. This is not matching the output for the README Not sure if this is intentional for memory purposes? Added the "Include" statment to retrieve them
            return _employeeContext.Employees
                .Include(d=>d.DirectReports) //Include Linq statement allows you to include related entities from the database. In this case, it uses the EmployeeId's in the DirectReports to retrieve their Employee information (Lazy vs Eager Loading)
                .SingleOrDefault(e => e.EmployeeId == id); //Maybe rather than returning the entire employee object as the direct reports, only return employee id? Not 100% how that would work
        }

        //Task 1 -  method to return full report structure 
        public Employee GetReportStructureById(string id)
        {

            //Similar to GetById, We have to eagerly load the DirectReports from the collection, but in this case, we have to also eagerly load the direct reports within the direct reports to retrieve the full report structure. Reportception!
            return _employeeContext.Employees
                .Include(d => d.DirectReports)
                .ToList() //Loads the directreports to list, which forces them to be eagerly loaded.
                .SingleOrDefault(e => e.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
