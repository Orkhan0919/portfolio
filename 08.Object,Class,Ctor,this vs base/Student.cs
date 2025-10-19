namespace PersonManagemet;

internal class Student : Person
{
    public string studentNumber;
    public string faculty;
    public double gpa;
    public int year;
    public double teqaud;

    public Student(string firstName, string lastName, int age, string mail, string id,
        string studentNumber, string faculty, double gpa, int year):base(firstName,lastName, age, mail,id)

    {
        this.studentNumber = studentNumber;
        this.faculty = faculty;
        this.gpa = gpa;
        this.year = year;
        this.teqaud = 0;
    }

    public void showStudentInfo()
    {
        Console.WriteLine(
            "First Name: " +firstName+"\n"+
            "Last Name: " + lastName +"\n"+
            "Age: " +age+"\n"+
            "Mail: " +mail +"\n"+
            "ID: " +id+"\n"+
            "Student Number: " +studentNumber+"\n"+
            "Faculty: " +faculty+"\n"+
            "GPA: " +gpa+"\n"+
            "Year: "+year+"\n"
        );
    }

    public void CalculateScholarship()
    {
        
        if (gpa >= 90)
        {
            this.teqaud = 500;
            Console.WriteLine("---Teqaud mebleginiz 500 AZN---");
        }
        else if (gpa >= 80)
        {
            this.teqaud = 350;
            Console.WriteLine("---Teqaud mebleginiz 350 AZN---");
        }
        else if (gpa >= 70)
        {
            this.teqaud = 200;
            Console.WriteLine("---Teqaud mebleginiz 200 AZN---");
        }
        else if (gpa >= 0)  // 0-69
        {
            this.teqaud = 0;
            Console.WriteLine("---Teqaud mebleginiz 0 AZN---");
        }
        else
        {
            Console.WriteLine("---Duzgun eded daxil edin !---");
        }
    }
}

