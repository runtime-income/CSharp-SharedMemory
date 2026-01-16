using SharedMemory.Core;

public sealed class ReaderApp
{
    private readonly SharedMemoryConfig _config;

    public ReaderApp(SharedMemoryConfig config)
    {
        _config = config;
    }

    public void Run()
    {
        Console.WriteLine("[Reader] Start. Press Ctrl+C to stop.");
        Console.WriteLine($"[Reader] MapName: {_config.MapName}");

        while (true)
        {
            try
            {
                using var mmf = new Int32MmfAccessor(_config, readOnly: true);

                while (true)
                {
                    var value = mmf.ReadInt32();
                    Console.WriteLine($"[Reader] Read: {value}");
                    Thread.Sleep(300);
                }
            }
            catch
            {
                Console.WriteLine("[Reader] Waiting for shared memory...");
                Thread.Sleep(500);
            }
        }
    }
}

internal class Program
{
    private static void Main()
    {
        var app = new ReaderApp(SharedMemoryConfig.DefaultInt32());
        app.Run();
    }
}
