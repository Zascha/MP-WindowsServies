using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MP.WindowsServices.Common;
using MP.WindowsServices.FileStorageObserver.Interfaces;

namespace MP.WindowsServices.FileStorageObserver
{
    public class GoogleDriveObserver : IFileStorageObserver
    {
        public event EventHandler<FileStoragePipelineEventArgs> FileAdded;

        public void MonitorAndProceedExistingFiles()
        {
            throw new NotImplementedException();
        }
    }
}
