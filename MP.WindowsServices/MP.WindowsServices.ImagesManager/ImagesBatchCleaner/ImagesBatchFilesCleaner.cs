using MP.WindowsServices.Common;
using MP.WindowsServices.Common.FileSystemHelpers;
using MP.WindowsServices.Common.Interfaces;
using System;
using System.Linq;

namespace MP.WindowsServices.ImagesManager.ImagesBatchCleaner
{
    public class ImagesBatchFilesCleaner : IWorkflowStepExecutor
    {
        private readonly IFileSystemHelper _fileSystemHelper;

        public ImagesBatchFilesCleaner(IFileSystemHelper fileSystemHelper)
        {
            _fileSystemHelper = fileSystemHelper ?? throw new ArgumentNullException(nameof(fileSystemHelper));
        }

        public event EventHandler<FileStoragePipelineEventArgs> StepExecuted;

        private void OnStepExecuted(object sender, FileStoragePipelineEventArgs e)
        {
            StepExecuted?.Invoke(this, e);
        }

        public void HandlePreviousStepResult(object sender, FileStoragePipelineEventArgs args)
        {
            if (args.BatchFilePaths == null)
                throw new ArgumentNullException(nameof(args.BatchFilePaths));

            if (!args.BatchFilePaths.Any())
                throw new ArgumentException(nameof(args.BatchFilePaths), "The pathed batch contains no files.");

            foreach(var file in args.BatchFilePaths)
            {
                _fileSystemHelper.DeleteFile(file);
            }
        }
    }
}
