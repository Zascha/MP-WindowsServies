using System;
using System.Collections.Generic;

namespace MP.WindowsServices.ImagesManager
{
    public class NewBatchEventArgs
    {
        public List<string> BatchFilesPaths { get; }

        public NewBatchEventArgs(List<string> batchFilesPaths)
        {
            BatchFilesPaths = batchFilesPaths ?? throw new ArgumentNullException(nameof(batchFilesPaths));
        }
    }
}
