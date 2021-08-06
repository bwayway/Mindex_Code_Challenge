using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Data;
using System;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        //Task 2 - Add Compensation to persistence layer
        /// <summary>
        /// Adds Compensation to the database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public Compensation Add(Compensation compensation)
        {
            //Task 2 - give the CompensationId a random GUID as a primary key
            compensation.CompensationId = Guid.NewGuid().ToString();
            _employeeContext.Compensation.Add(compensation);
            return compensation;
        }

        //Task 2 - Retrieve the employee via employeeId
        /// <summary>
        /// Retrieve compensation by employer Id and sort by date
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Compensation GetByEmployeeId(string id)
        {
            //Went back and forth on this. Should only the current compensation be returned? Or should the history of compensations be returned?
            return _employeeContext.Compensation
                .SingleOrDefault(e => e.EmployeeId == id);
        }

        //Task 2 - Save the compensation to the db
        /// <summary>
        /// Saves the Database to a snapshot
        /// </summary>
        /// <returns></returns>
        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

    }
}
