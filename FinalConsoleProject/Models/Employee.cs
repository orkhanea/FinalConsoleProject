using System;
namespace FinalConsoleProject
{
    public class Employee
    {
        public static int Counter = 1000;
        public string Name;
        public string Position;
        public string DepartmentName;
        public double Salary;
        public string No;
        public Employee()
        {
        }
        public override string ToString()
        {
            return $"{No} {Name} {Position} {DepartmentName} {Salary}";
        }
    }
}
