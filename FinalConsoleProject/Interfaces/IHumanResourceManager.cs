using System;
using System.Collections.Generic;
namespace FinalConsoleProject
{
    public interface IHumanResourceManager
    {
        public List<Department> departments { get; }
        public List<Department> GetDepartments();
        public void AddDepartment(Department deparment);
        public void EditDepartments(string departmentname, string newdepartmentname);
        public void AddEmployee(Employee employee);
        public void RemoveEmployee(string no, string departmentname);
        public void EditEmployee(string no, string newposition, double newsalary);
    }
}
