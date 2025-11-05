using System.Text.Json;

namespace _17_DirectoryStreamReader;

public class FileManager
{
    public string FolderPath  { get; set; }
    public string TextFilePath { get; set; }
    public string JsonPath { get; set; }

    public FileManager()
    {
        //Mende emeliyyat sistemi linux oldugu ucun mecbur FileManager()-i ai dan destek alaraq yazdim.
        //Cunki Windows- daki kimi fayllari root etme selahiyyetim yoxdur
        
        string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Apa-102/17-DirectoryStreamReader");
        FolderPath = Path.Combine(basePath, "StudentDatas");
        TextFilePath = Path.Combine(FolderPath, "StudentDatas.txt");
        JsonPath = Path.Combine(FolderPath, "StudentDatas.json");
    }

    public void CreateFolder()
    {
        if (!IsDirectoryExist())
        {
            Directory.CreateDirectory(FolderPath);
            Console.WriteLine($"Folder created : {FolderPath}");
        }
        else
        {
            Console.WriteLine($"Folder already exist : {FolderPath}");
        }
    }
    public void DeleteFolder()
    {
        if (!IsDirectoryExist())
        {
            Directory.Delete(FolderPath, true);
            Console.WriteLine($"Folder deleted : {FolderPath}");
        }
        else
        {
            Console.WriteLine($"Folder does not exist : {FolderPath}");
        }
    }

    public bool IsDirectoryExist()
    {
        return Directory.Exists(FolderPath);
    }
    public void WriteStudent(Student student)
    {
        if (!IsDirectoryExist())
            CreateFolder();

        using (StreamWriter writer = new StreamWriter(TextFilePath, append: true))
        {
            writer.WriteLine(student.ToString());
        }

        Console.WriteLine($"Tələbə fayla yazıldı: {student.Name}");
    }
    public void WriteAllStudents(List<Student> students)
    {
        if (!IsDirectoryExist())
            CreateFolder();

        File.WriteAllText(TextFilePath, "");

        using (StreamWriter writer = new StreamWriter(TextFilePath, append: true))
        {
            foreach (var stu in students)
            {
                writer.WriteLine(stu.ToString());
            }
        }


        Console.WriteLine($"Totally {students.Count} students written to database :");
    }
    public void WriteStudentsToJson(List<Student> students)
    {
        string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(JsonPath, json);

        Console.WriteLine("Students are saved with json format");
    }
    public List<Student> ReadStudentsFromJson()
    {
        if (!File.Exists(JsonPath))
        {
            Console.WriteLine("Json file cannot find");
            return new List<Student>();
        }

        string json = File.ReadAllText(JsonPath);
        List<Student> students = JsonSerializer.Deserialize<List<Student>>(json);

        if (students == null)
            students = new List<Student>();

        Console.WriteLine($"{students.Count} have readed from json");
        return students;
    }

    public void SerializeToJson(List<Student> students)
    {
        string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(JsonPath, json);

        Console.WriteLine($"Students are kept with json format");
        Console.WriteLine($"Json file path : {JsonPath}");
    }
    
}