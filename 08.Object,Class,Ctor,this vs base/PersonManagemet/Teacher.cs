namespace PersonManagemet;

public class Teacher : Person
{
    public string department;
    public string mainSubject;
    public double baseSalary;
    public int experience;

    public Teacher(string firstName, string lastName, int age, string mail, string id, string department , string mainSubject,
        double baseSalary,int experience):base(firstName,lastName,age,mail,id)
    {
        this.department = department;
        this.mainSubject = mainSubject;
        this.baseSalary = baseSalary;
        this.experience = experience;
    }

    public void showTeacherInfo()
    {
        Console.WriteLine(
            "First Name: " + firstName + "\n" +
            "Last Name: " + lastName + "\n" +
            "Age: " + age + "\n" +
            "Mail: " + mail + "\n" +
            "ID: " + id + "\n" +
            "Kafedra: " + department + "\n" +
            "Esas fenn: " + mainSubject + "\n" +
            "Salary: " + baseSalary
        );

    }

    public double CalculateSalary()
    {   
        double salary = baseSalary + experience*50;
        return salary;
    }
    


   
    
}