
namespace AccessModifiersTest
{
    internal class Student
    {
        private string _name;
        private string _surname;
        public int _age;
        public int _gpa;

        #region Getters_Setters
        
        public string Firstname
        {
          
            get
            {
                return _name;
            }
            set
            {
                
                    while (string.IsNullOrWhiteSpace(value))
                    {
                   
                        Console.WriteLine("There is nothing ");
                        value= Console.ReadLine();
                    }
                    

                    _name = value;
                

            }


        }
        public string LastName
        {
            get
            {
                return _surname;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("There is nothing ");
                    return;
                }
                _surname = value;
            }
        }
        #endregion


        public void GetStudentData()
        {
            Console.WriteLine($"Name: {_name}\nSurname: {_surname}\nAge: {_age}\nAverage: {_gpa}");
        }

        #region CreateStudent
        
        public void CreateStudent(Student student)
        {
            Console.WriteLine("Adinizi daxil edin :");
            string name =Console.ReadLine();
            student.Firstname = name;
            Console.WriteLine("Soyadinizi daxil edin :");
            string lastName = Console.ReadLine();
            student.LastName = lastName;
            Console.WriteLine("Yashinizi daxil edin :");
            int age = int.Parse(Console.ReadLine());
            student._age = age;
            Console.WriteLine("Ortalamanizi daxil edin :");
            int gpa= int.Parse(Console.ReadLine());
            student._gpa= gpa;
        }
        #endregion
    }
}


