using WebApplication1.Utilities.Enums;

namespace WebApplication1.Utilities.Extentions;

public static class FileValidator
{
    public static bool ValidatorType(this IFormFile file, string type)
    {
        if (file.ContentType.Contains(type)) return true;
        return false;

    }
    public static bool ValidatorSize(this IFormFile file, int size, Sizes sizes)
    {
        int byt = 1; 

        switch (sizes)
        {
            case Sizes.Byte:
                byt = 1;
                break;
            case Sizes.KB:
                byt = 1024;
                break;
            case Sizes.MB:
                byt = 1024 * 1024;
                break;
        }

        return file.Length <= size * byt;
    }
    
    public static async Task<string> CreateFileAsync(this IFormFile file, params string[] roots)
    {
        string fileName = Guid.NewGuid() + file.FileName;

        string path = Path.Combine(roots); 

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string fullPath = Path.Combine(path, fileName);

        using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return fileName;
    }
}