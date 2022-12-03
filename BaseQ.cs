public abstract class BaseQuestion
{
    public abstract object Run();

    public string[] ReadAllLines()
    {
        string ns = this.GetType().Namespace ?? string.Empty;
        if(string.IsNullOrEmpty(ns))
        {
            throw new InvalidOperationException("Q class should have namespace with the same name as its enclosing folder name");
        }
        string inputPath = Path.Combine(ns, "input.txt");
        return File.ReadAllLines(inputPath);
    }
}

public class QRunner
{
    public static void Run<T>() where T:BaseQuestion, new()
    {
        T qToRun = new();
        string className =typeof(T).Namespace + " " +typeof(T).Name;
        Console.WriteLine($"Running for {className}");
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        var result = qToRun.Run();
        watch.Stop();
        Console.WriteLine($"Result of {className} is: {result}");



        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
    }
}