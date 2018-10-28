using MP.WindowsServices.Common.FileSystemHelpers;
using MP.WindowsServices.Common.Interfaces;
using MP.WindowsServices.FileStorageObserver;
using MP.WindowsServices.ImagesManager;
using MP.WindowsServices.ImagesManager.ImagesBatchCleaner;
using MP.WindowsServices.ImagesManager.ImagesBatchHandlers;
using MP.WindowsServices.ProcessingBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileSystemHelper = new LocalFileSystemHelper();
            var fileObserver = new LocalFileSystemObserver();
            var provideImagesBacthStep = new ImagesBatchProvider();
            var convertBatchToPdfStep = new PdfImagesBatchHandler(fileSystemHelper);
            var imagesBatchFilesCleanerStep = new ImagesBatchFilesCleaner(fileSystemHelper);

            var workflowBuilder = new FileStorageWorkflowBuilder(fileObserver);
            workflowBuilder.StartProcessing(new List<IWorkflowStepExecutor>
            {
                provideImagesBacthStep,
                convertBatchToPdfStep,
                imagesBatchFilesCleanerStep
            });

            Console.ReadKey();
        }
    }
}
