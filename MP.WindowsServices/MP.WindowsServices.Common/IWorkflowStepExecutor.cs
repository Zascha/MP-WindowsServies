using System;
using System.Threading.Tasks;

namespace MP.WindowsServices.Common.Interfaces
{
    public interface IWorkflowStepExecutor
    {
        event EventHandler<PipelineDataEventArgs> StepExecuted;

        //Task ExecuteStepLogic(object sender, PipelineDataEventArgs args);
    }
}
