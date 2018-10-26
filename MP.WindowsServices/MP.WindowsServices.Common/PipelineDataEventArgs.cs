using System;
using System.Collections.Generic;

namespace MP.WindowsServices.Common
{
    public class PipelineDataEventArgs : EventArgs
    {
        private List<string> _batchFilesPaths;

        public List<string> BatchFilesPaths
        {
            get
            {
                if (_batchFilesPaths == null)
                {
                    _batchFilesPaths = new List<string>();
                }
                return _batchFilesPaths;
            }
            set
            {
                _batchFilesPaths = value ?? throw new ArgumentNullException("BatchFilesPathsList value");
            }
        }
    }
}
