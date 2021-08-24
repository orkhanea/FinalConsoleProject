using System;
namespace FinalConsoleProject
{
    public class Department
    {
        public string Name;
        public int WorkerLimit;
        public double SalaryLimit;
        public Employee[] employees;
        public Department()
        {
            employees = new Employee[0];
        }
        public double CalcSalaryAverage(Department department)
        {
            int workercount = 0;
            double employeesSalarySum = 0;
            if (department.employees.Length != 0)
            {
                foreach (Employee employee in department.employees)
                {
                    if (employee != null)
                    {
                        workercount++;
                        employeesSalarySum += employee.Salary;
                    }
                }
                return employeesSalarySum / workercount;
            }
            Console.WriteLine("Departament'de isci yoxdur.");
            return 0;
        }
        public override string ToString()
        {
            return $"{Name} {WorkerLimit} {SalaryLimit}";
        }
    }
}
