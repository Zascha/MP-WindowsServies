using MP.WindowsServices.Common.Interfaces;
using System.Collections.Generic;

namespace MP.WindowsServices.ProcessingBuilder
{
    public interface IFileStorageWorkflowBuilder
    {
        void StartProcessing(IEnumerable<IWorkflowStepExecutor> stepExecutors);
    }
}
