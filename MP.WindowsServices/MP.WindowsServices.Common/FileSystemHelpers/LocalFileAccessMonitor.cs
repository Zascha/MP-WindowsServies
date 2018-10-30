using MP.WindowsServices.Common.FileSystemHelpers.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MP.WindowsServices.Common.FileSystemHelpers
{
    internal class LocalFileAccessMonitor : IFileAccessMonitor
    {
        public Task EnsureFileIsReadyForAccess(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            return Task.Run(() => {
                var isReadyForAccess = false;
                while (!isReadyForAccess)
                {
                    try
                    {
                        var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                        fileStream.Close();

                        isReadyForAccess = true;
                    }
                    catch (IOException)
                    {

                    }
                }
            });
        }
    }
}
