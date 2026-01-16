using SharedMemory.Core;

public sealed class WriterApp
{
    private readonly SharedMemoryConfig _config;

    public WriterApp(SharedMemoryConfig config)
    {
        _config = config;
    }

    public void Run()
    {
        using var mmf = new Int32MmfAccessor(_config, readOnly: false);

        var value = 0;
        Console.WriteLine("[Writer] Start. Press Ctrl+C to stop.");
        Console.WriteLine($"[Writer] MapName: {_config.MapName}");

        while (true)
        {
            value++;
            mmf.WriteInt32(value);
            Console.WriteLine($"[Writer] Write: {value}");
            Thread.Sleep(1000);
        }
    }
}

internal class Program
{
    private static void Main()
    {
        var app = new WriterApp(SharedMemoryConfig.DefaultInt32());
        app.Run();
    }
}
