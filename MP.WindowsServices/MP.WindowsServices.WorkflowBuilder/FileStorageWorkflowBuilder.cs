using System;
using System.Collections.Generic;
using MP.WindowsServices.Common.Interfaces;
using MP.WindowsServices.FileStorageObserver.Interfaces;
using MP.WindowsServices.ProcessingBuilder.Extentions;

namespace MP.WindowsServices.ProcessingBuilder
{
    public class FileStorageWorkflowBuilder : IFileStorageWorkflowBuilder
    {
        private readonly IFileStorageObserver _fileStorageObserver;

        public FileStorageWorkflowBuilder(IFileStorageObserver fileStorageObserver)
        {
            _fileStorageObserver = fileStorageObserver ?? throw new ArgumentNullException(nameof(fileStorageObserver));
        }

        public void StartProcessing(IEnumerable<IWorkflowStepExecutor> stepExecutors)
        {
            var linkedList = stepExecutors.ToLinkedList();

            _fileStorageObserver.FileAdded += linkedList.First.Value.HandlePreviousStepResult;

            var node = linkedList.First;

            while (node.Next != null)
            {
                node.Value.StepExecuted += node.Next.Value.HandlePreviousStepResult;
                node = node.Next;
            }

            _fileStorageObserver.ObserverAndProceedExistingFiles();
        }
    }
}
