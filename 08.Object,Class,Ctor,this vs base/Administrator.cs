namespace PersonManagemet;

public class Administrator : Person
{
    public string position;
    public string department;
    public int accessLevel;

    public Administrator(string firstName, string lastName, int age, string mail, string id,string
        position, string department, int accessLevel):base(firstName,lastName,age,mail,id)
    {
        this.department=department;
        this.position = position;
        this.accessLevel = accessLevel;
    }
    public void showAdminInfo()
    {
        Console.WriteLine(
            "First Name: " + firstName + "\n" +
            "Last Name: " + lastName + "\n" +
            "Age: " + age + "\n" +
            "Mail: " + mail + "\n" +
            "ID: " + id + "\n" +
            "Kafedra: " + department + "\n" +
            "Vezife : " + position + "\n" +
            "Girish Seviyyesi: " + accessLevel
        );
    }
}