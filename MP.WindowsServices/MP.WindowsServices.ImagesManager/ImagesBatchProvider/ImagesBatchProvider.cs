using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MP.WindowsServices.Common.Interfaces;
using MP.WindowsServices.Common;
using System.Timers;

namespace MP.WindowsServices.ImagesManager
{
    public class ImagesBatchProvider : IWorkflowStepExecutor
    {
        private readonly Timer _observingTimer;
        private readonly Regex _documentIndexNumberRegex;

        private List<string> _documentParts;

        public event EventHandler<FileStoragePipelineEventArgs> StepExecuted;

        public ImagesBatchProvider(int nextFileAddingLimit = 0)
        {
            _documentIndexNumberRegex = new Regex("[0-9]+", RegexOptions.Compiled);
            _documentParts = new List<string>();

            if(nextFileAddingLimit > 0)
            {
                _observingTimer = new Timer();
                _observingTimer.Interval = nextFileAddingLimit;
                _observingTimer.Elapsed += OnTimerElapsed;
                _observingTimer.Enabled = true;
            }
        }

        private void OnStepExecuted(object sender, FileStoragePipelineEventArgs e)
        {
            StepExecuted?.Invoke(this, e);
        }

        public void HandlePreviousStepResult(object sender, FileStoragePipelineEventArgs args)
        {
            if (IsFileFromNewPatch(args.FilePath))
            {
                ProvideNewBatch();
            }

            _documentParts.Add(args.FilePath);
        }

        private void OnTimerElapsed(object source, ElapsedEventArgs e)
        {
            ProvideNewBatch();
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

            return addedDocumentIndexNumber - lastDocumentIndexNumber != 1;
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

        private void ProvideNewBatch()
        {
            OnStepExecuted(this, new FileStoragePipelineEventArgs { BatchFilePaths = _documentParts });
            _documentParts.Clear();
        }

        #endregion
    }
}
