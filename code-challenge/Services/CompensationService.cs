using System;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class CompensationService: ICompensationService
    {
        private readonly ILogger<CompensationService> _logger;
        private readonly ICompensationRepository _compensationRepository;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _logger = logger;
            _compensationRepository = compensationRepository;
        }


        /// <summary>
        /// Retrieves compensation data from the repository based on the Employee id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Compensation GetByEmployeeId(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetByEmployeeId(id);
            }

            return null;
        }

        public Compensation Create(Compensation compensation)
        {
            if (compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }
    }
}
