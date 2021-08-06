using System.Collections.Generic;


namespace challenge.Models
{
    public class ReportingStructure
    {
        public Employee Employee { get; set; }
        public int NumberOfReports { get; set; }

        public ReportingStructure(Employee employee)
        {
            Employee = employee;
            NumberOfReports = GetNumberOfReports(employee.DirectReports);
        }


        /// <summary>
        /// Recursive Function to retrieve the total number of direct reporters of an Employee
        /// </summary>
        /// <param name="directReports"></param>
        /// <returns></returns>
        private int GetNumberOfReports(List<Employee> directReports)
        {
            int totalReports = 0;

            //Check first whether or not the employee has direct reports
            if (directReports != null)
            {
                //Set the total reports for this Employee equal to the count of Employees in the Employee.DirectReports property/collection
                totalReports += directReports.Count;

                //Recursively iterate through DirectReports collection to retrieve the # of direct reporters of this specific employee
                foreach (Employee employee in directReports)
                {
                    totalReports += GetNumberOfReports(employee.DirectReports);
                }
            }
            //Return # of total diret reports
            return totalReports;
        }
    }

    
}
