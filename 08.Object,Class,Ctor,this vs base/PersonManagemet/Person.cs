namespace PersonManagemet;

public class Person
{
    public string firstName;
    public string lastName;
    public int age;
    public string mail;
    public string id;

    public Person(string firstName, string lastName, int age, string mail, string id)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        this.mail = mail;
        this.id = id;
        
    }
    public void GetFullName()
    {
        
        Console.WriteLine($"Ad : {firstName} Soyad :{lastName}");
    }

    public void ShowBasicInfo()
    {
        Console.WriteLine(
            "Ad: " +firstName +"\n"+
            "Soyad: " +lastName+"\n"+
            "Yash" + age +"\n"+
            "Mail: " +mail+"\n"+
            "ID: "+id
        );

    }
}