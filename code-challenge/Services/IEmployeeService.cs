using challenge.Models;
using System;

namespace challenge.Services
{
    public interface IEmployeeService
    {
        Employee GetById(String id);
        Employee Create(Employee employee);
        Employee Replace(Employee originalEmployee, Employee newEmployee);
        //Created for Task 1
        Employee GetReportStructureById(string Id);
    }
}
