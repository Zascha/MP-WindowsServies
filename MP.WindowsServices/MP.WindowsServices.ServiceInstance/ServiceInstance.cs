using Autofac;
using MP.WindowsServices.Common.Interfaces;
using MP.WindowsServices.DependencyResolver;
using MP.WindowsServices.FileStorageObserver.Interfaces;
using MP.WindowsServices.ImagesManager;
using MP.WindowsServices.ImagesManager.ImagesBatchCleaner;
using MP.WindowsServices.ImagesManager.ImagesBatchHandlers;
using MP.WindowsServices.ProcessingBuilder;
using System.Collections.Generic;

namespace MP.WindowsServices.ServiceInstance
{
    internal class ServiceInstance
    {
        private readonly ILifetimeScope _scope;

        public ServiceInstance()
        {
            _scope = DependencyResolver.DependencyResolver.Container.BeginLifetimeScope();
        }

        public void Start()
        {
            RunService();
        }

        public void Stop()
        {
            StopService();
            _scope.Dispose();
        }

        #region Private methods

        private void RunService()
        {
            var provideImagesBacthStep = _scope.Resolve<ImagesBatchProvider>();
            var convertBatchToPdfStep = _scope.Resolve<PdfImagesBatchHandler>();
            var cleanerStep = _scope.Resolve<ImagesBatchFilesCleaner>();
            var workflowBuilder = _scope.Resolve<IFileStorageWorkflowBuilder>();

            workflowBuilder.StartProcessing(new List<IWorkflowStepExecutor>
            {
                provideImagesBacthStep,
                convertBatchToPdfStep,
                cleanerStep
            });
        }

        private void StopService()
        {
            _scope.Resolve<IFileStorageObserver>().StopObserving();
        }

        #endregion
    }
}
