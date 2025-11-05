using _17_DirectoryStreamReader;

List<Student> students = new List<Student>
{
    new Student(1, "Ali", "Məmmədov", 20, 85.5),
    new Student(2, "Leyla", "Həsənova", 19, 92.0),
    new Student(3, "Vüqar", "Əliyev", 21, 78.5),
    new Student(4, "Nigar", "Əhmədova", 20, 88.0),
    new Student(5, "Rəşad", "Quliyev", 22, 95.5)
};
Student student1 = new Student(1,"Ali","Məmmədov", 20, 85.5);
Student student2 = new Student(2, "Leyla", "Həsənova", 19, 92.0);
Student student3 = new Student(3, "Vüqar", "Əliyev", 21, 78.5);
Student student4 = new Student(4, "Nigar", "Əhmədova", 20, 88.0);
Student student5 = new Student(5, "Rəşad", "Quliyev", 22, 95.5);
foreach (var student in students)
{
    student.DisplayInfo();
}
////////////////////////////////////////////////////////
Console.WriteLine("Write student method is working");
FileManager fm = new FileManager();
fm.WriteStudent(student1);
fm.WriteStudent(student2);
fm.WriteStudent(student3);
fm.WriteStudent(student4);
fm.WriteStudent(student5);

Console.WriteLine("\n------------------------------\n");

FileManager fileManager = new FileManager();

if (fileManager.IsDirectoryExist())
{
    Console.WriteLine("Folder already exists,deleting...");
    fileManager.DeleteFolder();
}
///////////////////////////////////////////////////
fileManager.CreateFolder();
///////////////////////////////////////////////////////
if (fileManager.IsDirectoryExist())
    Console.WriteLine("Folder created!");
else
{
    Console.WriteLine("Folder can't be created");
}
fileManager.CreateFolder();

if (fileManager.IsDirectoryExist())
    Console.WriteLine("Folder createq!");
else
    Console.WriteLine("Folder can't be created");

//////////////////////////////////////////////////////
fileManager.WriteAllStudents(students);
Console.WriteLine("\n🧾 TEXT FAYL MƏZMUNU:");
if (File.Exists(fileManager.TextFilePath))
    Console.WriteLine(File.ReadAllText(fileManager.TextFilePath));
else
    Console.WriteLine("Text faylı tapılmadı.");

fileManager.SerializeToJson(students);
///////////////////////////////////////////
Console.WriteLine("\nJSON file format");
if (File.Exists(fileManager.JsonPath))
    Console.WriteLine(File.ReadAllText(fileManager.JsonPath));
else
    Console.WriteLine("JSON file can't be found.");
/////////////////////////////////////////////
fileManager.ReadStudentsFromJson();
////////////////////////////////////////////////
Console.WriteLine("Reading file with CSV content");
FileManager fm2 = new FileManager();
string path = File.ReadAllText(fm2.TextFilePath);
Console.WriteLine(path);
///////////////////////////////////////////////////
Console.WriteLine("Reading file with JSON content");
string path2=File.ReadAllText(fileManager.JsonPath);
Console.WriteLine(path2);

Console.WriteLine($"Total student count: {students.Count}");

double total = 0;
foreach (var st in students)
    total += st.Grade;

double avg = total / students.Count;
Console.WriteLine($"📊 Average: {avg:F3}");

double max = students[0].Grade;
double min = students[0].Grade;

foreach (var stu in students)
{
    if (stu.Grade > max)
    {
        max = stu.Grade;
    }

    if (stu.Grade < min)
    {
        min = stu.Grade;
    }
}

Console.WriteLine($"Highest: {max}");
Console.WriteLine($" Lowest: {min}");

int highScorers = 0;
foreach (var st in students)
{
    if (st.Grade >= 90)
        highScorers++;
}

Console.WriteLine($"90 + : {highScorers}");


FileInfo fileInfo = new FileInfo(fm.JsonPath);
Console.WriteLine($"File size: {fileInfo.Length} byte");