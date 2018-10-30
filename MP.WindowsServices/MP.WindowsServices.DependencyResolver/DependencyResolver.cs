using Autofac;
using MP.WindowsServices.Common.FileSystemHelpers;
using MP.WindowsServices.Common.FileSystemHelpers.Interfaces;
using MP.WindowsServices.FileStorageObserver;
using MP.WindowsServices.FileStorageObserver.Interfaces;
using MP.WindowsServices.ImagesManager;
using MP.WindowsServices.ImagesManager.ImagesBatchCleaner;
using MP.WindowsServices.ImagesManager.ImagesBatchHandlers;
using MP.WindowsServices.ProcessingBuilder;

namespace MP.WindowsServices.DependencyResolver
{
    public static class DependencyResolver
    {
        private static IContainer _container;

        public static IContainer Container
        {
            get
            {
                if(_container == null)
                {
                    _container = Resolve();
                }

                return _container;
            }
        }

        private static IContainer Resolve()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LocalFileSystemHelper>().As<IFileSystemHelper>();
            builder.RegisterType<LocalFileSystemObserver>().As<IFileStorageObserver>();
            builder.RegisterType<FileStorageWorkflowBuilder>().As<IFileStorageWorkflowBuilder>();

            builder.RegisterType<ImagesBatchProvider>();
            builder.RegisterType<PdfImagesBatchHandler>();
            builder.RegisterType<ImagesBatchFilesCleaner>();

            return builder.Build();
        }
    }
}
