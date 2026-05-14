using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // --- Завдання 1 ---
        Console.WriteLine("=== Завдання 1: Адаптер ===");
        ILogger consoleLogger = new Logger();
        consoleLogger.Log("Звичайне повідомлення");
        string logPath = "test_log.txt";
        FileWriter writer = new FileWriter(logPath);
        ILogger fileLogger = new FileLoggerAdapter(writer);
        fileLogger.Error("Це повідомлення буде записане в файл.");
        Console.WriteLine($"[Перевірте файл {logPath}]\n");

        // --- Завдання 2 ---
        Console.WriteLine("=== Завдання 2: Декоратор ===");
        Hero myHero = new Warrior();
        myHero = new WeaponDecorator(myHero, "Sword of Truth");
        myHero = new ClothingDecorator(myHero, "Dragonscale Armor");
        myHero = new ArtifactDecorator(myHero, "Ring of Health");
        myHero.ShowInventory();
        Console.WriteLine();

        // --- Завдання 3 ---
        Console.WriteLine("=== Завдання 3: Міст ===");
        IRenderer raster = new RasterRenderer();
        IRenderer vector = new VectorRenderer();
        Shape triangle = new Triangle(raster);
        Shape circle = new Circle(vector);
        triangle.Draw();
        circle.Draw();
        Console.WriteLine();

        // --- Завдання 4 ---
        Console.WriteLine("=== Завдання 4: Проксі ===");
        string bookPath = "book.txt";
        File.WriteAllText(bookPath, "First line of book\n  Second indented line\nShort");
        ITextReader checker = new SmartTextChecker();
        checker.Read(bookPath);
        ITextReader locker = new SmartTextReaderLocker(@".*restricted.*");
        locker.Read("restricted_file.txt");
        Console.WriteLine();

        // --- Завдання 5 ---
        Console.WriteLine("=== Завдання 5: Компонувальник ===");
        LightElementNode table = new LightElementNode("table", "block", "paired");
        LightElementNode tr = new LightElementNode("tr", "block", "paired");
        LightElementNode td = new LightElementNode("td", "block", "paired");
        td.Add(new LightTextNode("Cell Data"));
        tr.Add(td);
        table.Add(tr);
        Console.WriteLine(table.OuterHTML());
        Console.WriteLine();

        // --- Завдання 6 ---
        Console.WriteLine("=== Завдання 6: Легковаговик ===");
        long memoryBefore = GC.GetTotalMemory(true);
        string[] bookLines = File.ReadAllLines(bookPath);
        FlyweightFactory factory = new FlyweightFactory();
        LightElementNodeFlyweight root = new LightElementNodeFlyweight(factory.GetTagMetadata("div"));

        for (int i = 0; i < bookLines.Length; i++)
        {
            string line = bookLines[i];
            TagMetadata meta;
            if (i == 0) meta = factory.GetTagMetadata("h1");
            else if (line.Length < 20) meta = factory.GetTagMetadata("h2");
            else if (line.StartsWith(" ")) meta = factory.GetTagMetadata("blockquote");
            else meta = factory.GetTagMetadata("p");

            LightElementNodeFlyweight element = new LightElementNodeFlyweight(meta);
            element.Add(new LightTextNode(line.Trim()));
            root.Add(element);
        }

        Console.WriteLine(root.OuterHTML());
        long memoryAfter = GC.GetTotalMemory(true);
        Console.WriteLine($"\nПам'ять, зайнята деревом (приблизно): {memoryAfter - memoryBefore} байт");
    }
}