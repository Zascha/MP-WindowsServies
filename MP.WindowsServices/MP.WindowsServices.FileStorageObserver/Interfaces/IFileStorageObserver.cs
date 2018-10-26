using System;
using System.IO;

namespace MP.WindowsServices.FileStorageObserver.Interfaces
{
    public interface IFileStorageObserver
    {
        event EventHandler<FileSystemEventArgs> FileAdded;
    }
}
