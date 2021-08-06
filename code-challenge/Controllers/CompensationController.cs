using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace challenge.Controllers
{

    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        // GET api/compensation/5
        //Task 2 - Returns a collection of compensation adjustments of the employee via ID
        [HttpGet("{id}")] 
        public IActionResult GetCompensationByEmployeeId(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var compensation = _compensationService.GetByEmployeeId(id);

            if (compensation == null)
                return NotFound();


            return Ok(compensation);
        }

        // POST api/compensation
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Created a new compensation for employee {compensation.EmployeeId}");

            _compensationService.Create(compensation);

            return CreatedAtRoute(new { id = compensation.CompensationId }, compensation);
        }
    }
}
