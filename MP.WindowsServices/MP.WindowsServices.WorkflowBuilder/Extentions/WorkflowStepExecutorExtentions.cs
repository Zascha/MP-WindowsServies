using MP.WindowsServices.Common.Interfaces;
using System.Collections.Generic;

namespace MP.WindowsServices.ProcessingBuilder.Extentions
{
    public static class WorkflowStepExecutorExtentions
    {
        public static LinkedList<IWorkflowStepExecutor> ToLinkedList(this IEnumerable<IWorkflowStepExecutor> stepExecutors)
        {
            var linkedList = new LinkedList<IWorkflowStepExecutor>();

            foreach(var step in stepExecutors)
            {
                linkedList.AddLast(step);
            }

            return linkedList;
        }
    }
}
