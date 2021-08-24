using System;

namespace FinalConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            HumanResourceManager humanResourceManager = new HumanResourceManager();

            do
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Seciminizi edin.");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("1.1 - Departameantlerin siyahisini gostermek");
                Console.WriteLine("1.2 - Departamenet yaratmaq");
                Console.WriteLine("1.3 - Departmanetde deyisiklik etmek");
                Console.WriteLine("2.1 - Iscilerin siyahisini gostermek");
                Console.WriteLine("2.2 - Departamentdeki iscilerin siyahisini gostermrek");
                Console.WriteLine("2.3 - Isci elave etmek");
                Console.WriteLine("2.4 - Isci uzerinde deyisiklik etmek");
                Console.WriteLine("2.5 - Departamentden isci silinmesi");
                Console.WriteLine(Environment.NewLine);

                string choose = Console.ReadLine();

                switch (choose)
                {
                    case "1.1":
                        ShowDepartments(humanResourceManager);
                        break;
                    case "1.2":
                        AddDepartment(humanResourceManager);
                        break;
                    case "1.3":
                        EditDepartment(humanResourceManager);
                        break;
                    case "2.1":
                        ShowEmployeeList(humanResourceManager);
                        break;
                    case "2.2":
                        GetEmployeeByDepartament(humanResourceManager);
                        break;
                    case "2.3":
                        AddEmployee(humanResourceManager);
                        break;
                    case "2.4":
                        EditEmployee(humanResourceManager);
                        break;
                    case "2.5":
                        RemoveEmployee(humanResourceManager);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Seciminiz yalnisdir.");
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Press Enter to Continue");
                        Console.ReadLine();
                        break;
                }

            } while (true);
        }

        static void AddDepartment(HumanResourceManager humanResourceManager)
        {
            Console.WriteLine("Departamentin adini daxil edin.");
            string departmentName = Console.ReadLine();
            while (departmentName.Length < 2)
            {
                Console.Clear();
                Console.WriteLine("Departamentin adi minimum 2 herfden ibaret ola biler.");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Departamentin adini yeniden daxil edin.");
                departmentName = Console.ReadLine();
            }
            Console.WriteLine("Limit teyin edin.");
            string wLimit = Console.ReadLine();
            int workerLimit;
            while (!int.TryParse(wLimit, out workerLimit) || workerLimit < 1)
            {
                Console.Clear();
                Console.WriteLine("Limit 1'den kicik ola bilmez ve yalniz reqemlerden ibaret olmalidir.");
                wLimit = Console.ReadLine();
                int.TryParse(wLimit, out workerLimit);
            }
            Console.WriteLine("Salary Limit teyin edin");
            string sLimit = Console.ReadLine();
            double salaryLimit;
            while (!double.TryParse(sLimit, out salaryLimit) || salaryLimit < 250)
            {
                Console.Clear();
                Console.WriteLine("Salary limit'i 250'den kicik ola bilmez ve yalniz reqemlerden ibaret olmalidir.");
                sLimit = Console.ReadLine();
                double.TryParse(sLimit, out salaryLimit);
            }
            Department department = new Department
            {
                Name = departmentName,
                WorkerLimit = workerLimit,
                SalaryLimit = salaryLimit,
            };
            humanResourceManager.AddDepartment(department);
        }

        static void ShowDepartments(HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.departments.Count != 0)
            {
                foreach (Department department in humanResourceManager.departments)
                {
                    int workercount = 0;
                    foreach (Employee employee in department.employees)
                    {
                        if (employee == null)
                        {
                            workercount++;
                        }
                       
                    }

                    Console.WriteLine("==========Department==========");
                    Console.WriteLine($"Department Name: {department.Name} \nIsci sayi: {department.employees.Length - workercount} \nIscilerin maas ortalamalari: {department.CalcSalaryAverage(department)}");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Sistemde Departament yoxdur.");
                Console.ReadLine();
            }
        }

        static void AddEmployee(HumanResourceManager humanResourceManager)
        {
            Console.WriteLine("Ad ve soyadi daxil edin.");
            string fullName = Console.ReadLine();
            Console.WriteLine("Position daxil edin.");
            string position = Console.ReadLine();
            while (position.Length < 2)
            {
                Console.Clear();
                Console.WriteLine("Position uzunlugu minimum 2 ola biler.");
                position = Console.ReadLine();
            }
            Console.WriteLine("Department adini daxil edin.");
            string departmentName = Console.ReadLine();
            while (departmentName.Length < 2)
            {
                Console.Clear();
                Console.WriteLine("Department adininin uzunlugu minimum 2 ola biler.");
                departmentName = Console.ReadLine();
            }
            Console.WriteLine("Salary daxil edin.");
            string empSalary = Console.ReadLine();
            double salary;
            while (!double.TryParse(empSalary, out salary) || salary < 250)
            {
                Console.Clear();
                Console.WriteLine("Salary minimum 250 ola biler ve yalniz reqemlerden ibaret olmalidir.");
                empSalary = Console.ReadLine();
                double.TryParse(empSalary, out salary);
            }
            Employee employee = new Employee
            {
                Name = fullName,
                Position = position,
                Salary = salary,
                DepartmentName = departmentName
            };
            humanResourceManager.AddEmployee(employee);
        }

        static void ShowEmployeeList(HumanResourceManager humanResourceManager)
        {
            foreach (Department department in humanResourceManager.departments)
            {
                foreach (Employee employee in department.employees)
                {
                    if (employee != null)
                    {
                        Console.WriteLine("---------------");
                        Console.WriteLine($"Isci nomresi: {employee.No} \nAd ve soyadi: {employee.Name} \nDepartament adi: {employee.DepartmentName} \nMaasi: {employee.Salary} \nVezifesi: {employee.Position}");
                    }
                }
            }
        }

        static void EditDepartment(HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.departments.Count != 0)
            {
                Console.WriteLine("Deyismek istediyiniz Departamentin adini daxil edin.");
                Console.WriteLine(Environment.NewLine);
                string departname = Console.ReadLine();
                Department department = humanResourceManager.departments.Find(d => d.Name.ToUpper() == departname.ToUpper());
                if (department == null)
                {
                    Console.Clear();
                    Console.WriteLine($"Daxil etdiyiniz {departname.ToUpper()} adda Department movcud deyildir");
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Press Enter To Continue");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    int workerlimitcount = 0;
                    foreach (Employee employee in department.employees)
                    {
                        if (employee == null)
                        {
                            workerlimitcount++;
                        }
                    }
                    Console.WriteLine($"Departament adi: {department.Name} \nBalansda qalan maas limiti: {department.SalaryLimit} \nIse qebul oluna bilecek isci sayi: {department.WorkerLimit - (department.employees.Length - workerlimitcount)}");
                }
                Console.WriteLine("Departamentin yeni adini daxil edin");
                string newdepartname = Console.ReadLine();
                while (newdepartname.Length < 2)
                {
                    Console.WriteLine("Department adinin uzunlugu minimum 2 ola biler.");
                    newdepartname = Console.ReadLine();
                }
                humanResourceManager.EditDepartments(departname, newdepartname);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Sistemde melumatlari deyisdirile bilecek departament yoxdur.");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Press Enter To Continue");
                Console.ReadLine();
            }
        }

        static void GetEmployeeByDepartament(HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.departments.Count > 0)
            {
                Console.WriteLine("Iscilerinin siyahisini gormey istediyiniz departamentin adini daxil edin.");
                string getdepartmentname = Console.ReadLine();
                bool check = true;
                foreach (Department department in humanResourceManager.departments)
                {
                    if (department.Name.ToUpper() == getdepartmentname.ToUpper())
                    {
                        foreach (Employee employee in department.employees)
                        {
                            if (employee != null)
                            {
                                Console.WriteLine("---------------");
                                Console.WriteLine($"Isci no:{employee.No} \nAd ve soyadi: {employee.Name} \nVezifesi: {employee.Position} \nMaasi: {employee.Salary}");
                                check = false;
                            }
                        }
                    }
                    if (check)
                    {
                        Console.Clear();
                        Console.WriteLine($"Sistemde {getdepartmentname.ToUpper()} adinda department movcud deyil.");
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Press Enter To Continue");
                        Console.ReadLine();
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Sistemde hec bir departament movcud deyil.");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Press Enter To Continue");
                Console.ReadLine();
            }
        }

        static void EditEmployee(HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.departments.Count != 0)
            {
                bool check = true;
                Console.WriteLine("Deyisiklik etmek istediyiniz iscinin nomresini daxil edin.");
                string no = Console.ReadLine();
                foreach (Department department in humanResourceManager.departments)
                {
                    foreach (Employee employee in department.employees)
                    {
                        if (employee != null)
                        {
                            if (no.ToUpper() == employee.No.ToUpper())
                            {
                                check = false;
                                Console.Clear();
                                Console.WriteLine("==========Iscinin melumatlari==========");
                                Console.WriteLine($"Isci adi: {employee.Name} \nMaasi: {employee.Salary} \nVezifesi: {employee.Position}");
                                Console.WriteLine(Environment.NewLine);
                                Console.WriteLine("Deyisiklik etmeye baslamaq ucun Enter'i basin");
                                Console.ReadLine();
                                Console.WriteLine("Yeni position daxil edin.");
                                string newposition = Console.ReadLine();
                                while (newposition.Length < 2)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Position minimum 2 herfden ibaret ola biler.");
                                    newposition = Console.ReadLine();
                                }
                                Console.WriteLine("Yeni salary daxil edin");
                                string newsalary1 = Console.ReadLine();
                                double newsalary;
                                while (!double.TryParse(newsalary1, out newsalary) || newsalary < 250)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Salary 250'den kicik ola bilmez ve yalniz reqemden ibaret olmalidir.");
                                    newsalary1 = Console.ReadLine();
                                    double.TryParse(newsalary1, out newsalary);
                                }
                                humanResourceManager.EditEmployee(no, newposition, newsalary);
                            }
                        }
                    }
                }
                if (check)
                {
                    Console.Clear();
                    Console.WriteLine($"Sistemde daxil etdiyiniz {no.ToUpper()} nomreli isci tapilmadi.");
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Press Enter To Continue");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Sistemde melumatlari deyisdirile bilecek isci yoxdur.");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Press Enter To Continue");
                Console.ReadLine();
                Console.Clear();
            }

        }

        static void RemoveEmployee(HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.departments.Count != 0)
            {
                Console.WriteLine("Departamentden silmey istediyiniz iscinin nomresini daxil edin");
                string no = Console.ReadLine();
                Console.WriteLine("Silmey istediyiniz iscinin daxil oldugu departamentin adini yazin.");
                string departmentname = Console.ReadLine();
                Department department = humanResourceManager.departments.Find(d => d.Name.ToUpper() == departmentname.ToUpper());
                if (department==null)
                {
                    Console.Clear();
                    Console.WriteLine($"Sitemde daxil etdiyiniz {departmentname.ToUpper()} adinda departament movcud deyil.");
                    Console.WriteLine("Press Enter To Continue");
                    Console.ReadLine();
                    Console.Clear();
                    return;
                }
                else
                {
                    foreach (Employee employee in department.employees)
                    {
                        if (employee.No.ToUpper()==no.ToUpper())
                        {
                            humanResourceManager.RemoveEmployee(no, departmentname);
                            return;
                        }
                    }
                    Console.WriteLine($"{department.Name.ToUpper()} departamentinde {no.ToUpper()} nomreli isci yoxdur.");
                    return;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Sistemde silinecek her hansisa bir isci movcud deyil.");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Press Enter To Continue");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
