using System;
using System.IO;

public interface ILogger
{
    void Log(string message);
    void Error(string message);
    void Warn(string message);
    void SetConsoleColor(ConsoleColor color);
}

public class Logger : ILogger
{
    public void SetConsoleColor(ConsoleColor color) => Console.ForegroundColor = color;

    public void Log(string message)
    {
        SetConsoleColor(ConsoleColor.Green);
        Console.WriteLine($"[LOG]: {message}");
        Console.ResetColor();
    }

    public void Error(string message)
    {
        SetConsoleColor(ConsoleColor.Red);
        Console.WriteLine($"[ERROR]: {message}");
        Console.ResetColor();
    }

    public void Warn(string message)
    {
        SetConsoleColor(ConsoleColor.DarkYellow);
        Console.WriteLine($"[WARN]: {message}");
        Console.ResetColor();
    }
}

public class FileWriter
{
    private string _filePath;
    public FileWriter(string filePath) { _filePath = filePath; }

    public void Write(string text) => File.AppendAllText(_filePath, text);
    public void WriteLine(string text) => File.AppendAllText(_filePath, text + Environment.NewLine);
}

public class FileLoggerAdapter : ILogger
{
    private FileWriter _fileWriter;

    public FileLoggerAdapter(FileWriter fileWriter)
    {
        _fileWriter = fileWriter;
    }

    public void SetConsoleColor(ConsoleColor color) { /* Файли не мають кольору */ }

    public void Log(string message) => _fileWriter.WriteLine($"[LOG]: {message}");
    public void Error(string message) => _fileWriter.WriteLine($"[ERROR]: {message}");
    public void Warn(string message) => _fileWriter.WriteLine($"[WARN]: {message}");
}