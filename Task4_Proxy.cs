using System;
using System.IO;
using System.Text.RegularExpressions;

public interface ITextReader
{
    char[][] Read(string filePath);
}

public class SmartTextReader : ITextReader
{
    public char[][] Read(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        char[][] result = new char[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            result[i] = lines[i].ToCharArray();
        }
        return result;
    }
}

public class SmartTextChecker : ITextReader
{
    private SmartTextReader _reader = new SmartTextReader();

    public char[][] Read(string filePath)
    {
        Console.WriteLine($"[LOG]: Opening file {filePath}...");
        try
        {
            char[][] result = _reader.Read(filePath);
            Console.WriteLine($"[LOG]: File {filePath} read successfully.");
            
            int totalChars = 0;
            foreach (var line in result) totalChars += line.Length;
            
            Console.WriteLine($"[LOG]: Total lines: {result.Length}, Total characters: {totalChars}");
            Console.WriteLine($"[LOG]: Closing file {filePath}...");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR]: Failed to read file. {ex.Message}");
            return null;
        }
    }
}

public class SmartTextReaderLocker : ITextReader
{
    private SmartTextReader _reader = new SmartTextReader();
    private Regex _restrictedRegex;

    public SmartTextReaderLocker(string restrictedPattern)
    {
        _restrictedRegex = new Regex(restrictedPattern);
    }

    public char[][] Read(string filePath)
    {
        if (_restrictedRegex.IsMatch(filePath))
        {
            Console.WriteLine("Access denied!");
            return null;
        }
        return _reader.Read(filePath);
    }
}