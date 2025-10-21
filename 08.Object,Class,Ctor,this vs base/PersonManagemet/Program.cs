using PersonManagemet;

#region usersData




Person person = new Person("Orxan", "Mirzeyev", 20, "mirzeyevorxan@gmail.com", "202206417");
Student student = new Student(
    "Orxan", "Mirzeyev", 20, "mirzeyevorxan@gmail.com", "202206417",
    "No243", "Engineering", 92.0, 4 );

Student student2 = new Student(
    "Elman", "Aliyev", 20, "elman@gmail.com", "202206419",
    "No244", "Engineering", 68.5, 3 );

Student student3 = new Student(
    "Arif", "Muradli", 20, "arif@gmail.com", "202206432",
    "No244", "Engineering", 88.5, 3 );

Teacher teacher = new Teacher(
    "Rufan", "Memmedli", 78, "rufanmemmedli@gmail.com", "202206422",
    "Fizika", "Fizika", 650.4, 15 );

Teacher teacher2 = new Teacher(
    "Ramin", "Kerimov", 42, "ramin@gmail.com", "202206421",
    "Kimya", "Kimya", 450.2, 8 );

//Shagirdleri asagi elave etmeyi unutmayin
Student[] students = { student, student2, student3 };

//Muellimleri asagi elave etmeyi unutmayin
Teacher[] teachers = { teacher, teacher2 };

Administrator dekan = new Administrator("Elman", "Eleskerli", 27, "elman@gmail.com", "202206420",
    "Kimya", "Kimya", 2);
#endregion

double TotalSalary(Teacher[] teachers)
{
    double totalSalary = 0;
    for (int i = 0; i < teachers.Length; i++)
    {
        totalSalary += teachers[i].CalculateSalary();
    }
    return totalSalary;
}
double TotalTeqaud(Student[] students)
{
    double totalGPA = 0;
    for (int i = 0; i < students.Length; i++)
    {
        students[i].CalculateScholarship();
        totalGPA += students[i].teqaud;
    }
    return totalGPA;
}

#region baseRegion

Console.WriteLine("Vezifenizi sechin: \n Shagird--1 Muellim--2 Admin--3");
int a=int.Parse(Console.ReadLine());

switch (a)
{
    case 1:
        Console.WriteLine("Teqaud--1  Telebe Melumatlari--2 Esas melumat--3");
        int b=int.Parse(Console.ReadLine());
        switch (b)
        {
            case 1:
                student.CalculateScholarship();
                break;
            case 2:
                student2.showStudentInfo();
                break;
            case 3 :
                student3.ShowBasicInfo();
                break;
        }
        break;
    case 2:
        Console.WriteLine("Yekun maash--1  Muellim melumatlari--2 Esas melumat--3");
        int c=int.Parse(Console.ReadLine());
        switch (c)
        {
            case 1:
                
                double salary = teacher.CalculateSalary();
                Console.WriteLine("Yekun maash:"+salary);
                break;
            case 2:
                teacher2.showTeacherInfo();
                break;
            case 3:
                teacher2.ShowBasicInfo();
                break;
        }
        break;
    case 3:
        Console.WriteLine("---Giris seviyenizi daxil edin---");
        int d=int.Parse(Console.ReadLine());
        switch (d)
        {
            case 1:
                Console.WriteLine("---Administrator melumatlari---");
                dekan.showAdminInfo();
                break;
            case 2:
                Console.WriteLine("Admin melumati--1 Istifadeci melumati alma--2");
                int f =int.Parse(Console.ReadLine());
                switch (f)
                {
                    case 1:
                        dekan.showAdminInfo();
                        break;
                    case 2:
                        teacher2.showTeacherInfo();
                        teacher.showTeacherInfo();
                        student.showStudentInfo();
                        student2.showStudentInfo();
                        student3.showStudentInfo();
                        break;
                    
                }
                break;
            case 3:
                Console.WriteLine("Admin melumati--1 Istifadeci melumati alma--2 Istifadeci gelir melumatlari alma--3");
                int t = int.Parse(Console.ReadLine());
                switch (t)
                {
                    case 1:
                        dekan.showAdminInfo();
                        break;
                    case 2:
                        teacher2.showTeacherInfo();
                        teacher.showTeacherInfo();
                        student.showStudentInfo();
                        student2.showStudentInfo();
                        student3.showStudentInfo();
                        break;
                    case 3:
                        Console.WriteLine("Umumi gelirler--1 Statistikalar--2");
                        int l =int.Parse(Console.ReadLine());
                        switch (l)
                        {
                            case 1:
                                teacher2.CalculateSalary();
                                teacher.CalculateSalary();
                                student.CalculateScholarship();
                                student2.CalculateScholarship();
                                student3.CalculateScholarship();
                                break;
                            case 2:
                                Console.WriteLine("Umumi maash xerci:");
                                double topluMaas=TotalSalary(teachers);
                                Console.WriteLine("Toplam maashlari:" + topluMaas);
                                Console.WriteLine("Umumi teqaud xerci");
                                double toplamTeqaud = TotalTeqaud(students);
                                Console.WriteLine("Umumi teqaud xerci : " + toplamTeqaud + " AZN");

                                break;
                            
                        }
                        
                        
                    break;
                }
                break;
        }
        break;

        
    
        
    
}
#endregion