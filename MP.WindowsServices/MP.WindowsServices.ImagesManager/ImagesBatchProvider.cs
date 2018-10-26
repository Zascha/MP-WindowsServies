using MP.WindowsServices.FileStorageObserver.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using MP.WindowsServices.ImagesManager.Interfaces;

namespace MP.WindowsServices.ImagesManager
{
    public class ImagesBatchProvider : IImagesBatchProvider
    {
        private Regex _documentIndexNumberRegex = new Regex("[0-9]+", RegexOptions.Compiled);
        private List<string> _documentParts;
        
        private readonly IFileStorageObserver _fileStorageObserver;
        private readonly bool _enableTimeoutObserving;

        public event EventHandler<NewBatchEventArgs> NewBatchIsFormed;

        public ImagesBatchProvider(IFileStorageObserver fileStorageObserver, bool enableTimeoutObserving = false)
        {
            _fileStorageObserver = fileStorageObserver ?? throw new ArgumentNullException(nameof(fileStorageObserver));
            _fileStorageObserver.FileAdded += ExecuteNewFileAdded;

            _documentParts = new List<string>();
            _enableTimeoutObserving = enableTimeoutObserving;
        }

        private void OnNewBatchIsFormed(object sender, NewBatchEventArgs e)
        {
            NewBatchIsFormed?.Invoke(this, e);
        }

        private void ExecuteNewFileAdded(object sender, FileSystemEventArgs e)
        {
            if (IsFileFromNewPatch(e.Name))
            {
                OnNewBatchIsFormed(this, new NewBatchEventArgs(_documentParts));
                _documentParts.Clear();
            }

            _documentParts.Add(e.Name);
        }

        #region Private methods

        private bool IsFileFromNewPatch(string fileName)
        {
            if (!_documentParts.Any())
            {
                return false;
            }

            var lastDocumentIndexNumber = GetDocumentIndexNumber(_documentParts.Last());
            var addedDocumentIndexNumber = GetDocumentIndexNumber(fileName);

            return addedDocumentIndexNumber - lastDocumentIndexNumber == 1;
        }

        private int GetDocumentIndexNumber(string fileName)
        {
            var indexNumber = _documentIndexNumberRegex.Match(fileName).Value;

            if (string.IsNullOrEmpty(indexNumber))
            {
                return -1;
            }

            return int.Parse(indexNumber);
        }

        #endregion
    }
}
