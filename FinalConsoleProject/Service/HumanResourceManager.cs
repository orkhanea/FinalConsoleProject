using System;
using System.Collections.Generic;

namespace FinalConsoleProject
{
    public class HumanResourceManager : IHumanResourceManager
    {
        private List<Department> _departments;
        public List<Department> departments => _departments;
        public HumanResourceManager()
        {
            _departments = new List<Department>();

        }

        public void AddDepartment(Department deparment)
        {

            _departments.Add(deparment);

        }

        public void AddEmployee(Employee employee)
        {
            Department department = _departments.Find(d => d.Name.ToUpper() == employee.DepartmentName.ToUpper());
            if (department != null)
            {
                int workercount = 0;
                foreach (Employee employee1 in department.employees)
                {
                    if (employee1 != null)
                    {
                        workercount++;
                    }
                }
                if (department.WorkerLimit > workercount)
                {
                    if ((department.SalaryLimit - employee.Salary) >= 0)
                    {
                        Array.Resize(ref department.employees, department.employees.Length + 1);
                        department.employees[department.employees.Length - 1] = employee;
                        employee.No = department.Name.Substring(0, 2).ToUpper() + Employee.Counter;
                        Employee.Counter++;
                        department.SalaryLimit -= employee.Salary;
                        Console.WriteLine($"{employee.Name.ToUpper()} adinda isci muveffeqiyyetle {employee.DepartmentName.ToUpper()} adli departamente elave olundu.");
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Press Enter To Continue");
                        Console.ReadLine();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Departamentin maas limiti doldugundan isci elave etmek mumkun olmadi.");
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Press Enter To Continue");
                        Console.ReadLine();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Departamentin isci limiti doldugundan isci elave etmek mumkun olmadi.");
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Press Enter To Continue");
                    Console.ReadLine();
                    return;
                }
            }
        }   

        public void EditDepartments(string departmentname, string newdepartmentname)
        {
            foreach (Department department in departments)
            {
                if (department.Name==departmentname)
                {
                    department.Name = newdepartmentname;
                    Console.WriteLine($"Departamentin adi {newdepartmentname.ToUpper()} olaraq deyisdirildi.");
                    

                    foreach (Employee employee in department.employees)
                    {
                        if (employee!=null)
                        {
                            employee.DepartmentName = newdepartmentname;
                        }
                    }

                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Press Enter To Continue");
                    Console.ReadLine();
                    break;
                }
            }

        }

        public void EditEmployee(string no, string newposition, double newsalary)
        {
            foreach (Department department in departments)
            {
            
                foreach (Employee employee in department.employees)
                {
                    if (employee!=null && no.ToUpper() == employee.No.ToUpper())
                    {
                        department.SalaryLimit += employee.Salary;
                    }
                    
                }

                foreach (Employee employee1 in department.employees)
                {
                    if (employee1 != null && no.ToUpper() == employee1.No.ToUpper() && (department.SalaryLimit - newsalary) >= 0)
                    {
                        employee1.Position = newposition;
                        employee1.Salary = newsalary;
                        department.SalaryLimit -= newsalary;
                        Console.WriteLine($"{no.ToUpper()} nomreli iscinin melumatlari deyisdirildi.");
                        Console.ReadLine();
                        break;
                    }
                    else if ((department.SalaryLimit - newsalary) < 0)
                    {
                        Console.WriteLine("Yeni iscinin maasi departamentin maas limitini asir.");
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Press Enter To Continue");
                        Console.ReadLine();
                        break;
                    }
                   
                }
            }
            
        }

        public List<Department> GetDepartments()
        {
            return _departments;
        }

        public void RemoveEmployee(string no, string departmentname)
        {
            Department department = _departments.Find(d => d.Name.ToUpper() == departmentname.ToUpper());
            if (department!=null)
            {
                for (int i = 0; i < department.employees.Length; i++)
                {
                    if (department.employees[i]!=null&& no.ToLower() == department.employees[i].No.ToLower())
                    {
                        department.SalaryLimit += department.employees[i].Salary;
                        Array.Clear(department.employees, i, 1);
                        Console.WriteLine($"Employee silindi.");
                        return;
                    }
                }
            }
        }
    }
}
