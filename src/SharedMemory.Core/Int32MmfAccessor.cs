using System.IO.MemoryMappedFiles;

namespace SharedMemory.Core;

public sealed class Int32MmfAccessor : IDisposable
{
    private readonly MemoryMappedFile _mmf;
    private readonly MemoryMappedViewAccessor _accessor;

    public Int32MmfAccessor(SharedMemoryConfig config, bool readOnly)
    {
        if (readOnly)
        {
            _mmf = MemoryMappedFile.OpenExisting(config.MapName, MemoryMappedFileRights.Read);
            _accessor = _mmf.CreateViewAccessor(0, config.CapacityBytes, MemoryMappedFileAccess.Read);
        }
        else
        {
            _mmf = MemoryMappedFile.CreateOrOpen(config.MapName, config.CapacityBytes, MemoryMappedFileAccess.ReadWrite);
            _accessor = _mmf.CreateViewAccessor(0, config.CapacityBytes, MemoryMappedFileAccess.ReadWrite);
        }
    }

    public void WriteInt32(int value) => _accessor.Write(0, value);
    public int ReadInt32() => _accessor.ReadInt32(0);

    public void Dispose()
    {
        _accessor.Dispose();
        _mmf.Dispose();
    }
}
