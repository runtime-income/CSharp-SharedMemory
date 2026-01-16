namespace SharedMemory.Core;

public sealed class SharedMemoryConfig
{
    public string MapName { get; }
    public int CapacityBytes { get; }

    public SharedMemoryConfig(string mapName, int capacityBytes)
    {
        if (string.IsNullOrWhiteSpace(mapName))
            throw new ArgumentException("mapName is required.", nameof(mapName));
        if (capacityBytes <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacityBytes));

        MapName = mapName;
        CapacityBytes = capacityBytes;
    }

    public static SharedMemoryConfig DefaultInt32()
    {
        // 테스트가 안 되면 "Global\\"을 제거한 로컬 이름으로 시작하세요.
        return new SharedMemoryConfig("RuntimeIncome_SharedMem_V1", 4);
    }
}
