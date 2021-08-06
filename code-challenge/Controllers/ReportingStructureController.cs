using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;



namespace challenge.Controllers
{
    [Route("api/reporting-structure")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }


        //Task 1 - HTTP Method to retrieve direct reports
        /// <summary>
        /// Retrieves and returns the full employee structure of an employee, as well as the number of direct reports for the employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetDirectReports(String id)
        {
            _logger.LogDebug($"Recieved total number of direct report request for employee {id}");

            //Extract employee from database by the id to an Employee object

            var employee = _employeeService.GetReportStructureById(id);

            //Check to make sure the employee exists in the db
            if (employee == null)
                return NotFound();

            var directReport = new ReportingStructure(employee);

            return Ok(directReport);
        }
    }
}
