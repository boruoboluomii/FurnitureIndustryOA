using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace WorkflowDemo1
{

    class Program
    {
        static void Main(string[] args)
        {
            //Activity workflow1 = new Workflow1();
            //WorkflowInvoker.Invoke(workflow1);

            Activity leaveActivity =new LeaveActivity();
            WorkflowInvoker.Invoke(leaveActivity);


            Console.ReadKey();
        }
    }
}
