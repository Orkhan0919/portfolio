using AccessModifiersTest.School;

namespace AccessModifiersTest.School
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student();
            student.CreateStudent(student);
            student.GetStudentData();

        }
    }
}