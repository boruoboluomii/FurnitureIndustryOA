using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFWinFrmDemoo
{
    public partial class Form1 : Form
    {
        private WorkflowApplication workflowApplication;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartWF_Click(object sender, EventArgs e)
        {
            //WorkflowInvoker.Invoke(new DemoActivity(), new Dictionary<string, object>()
            //    {
            //         {"BookMarkName",this.txtBookMarkName.Text}
            //    });

            workflowApplication = new WorkflowApplication(new DemoActivity(), new Dictionary<string, object>()
                {
                     {"BookMarkName",this.txtBookMarkName.Text}
                });

            workflowApplication.Idle += AfterWorkflowIdel;

            workflowApplication.Run();

        }

        private void AfterWorkflowIdel(WorkflowApplicationIdleEventArgs obj)
        {
            Console.WriteLine("工作流停下来了。");
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            workflowApplication.ResumeBookmark(txtBookMarkName.Text,int.Parse(txtMoney.Text));
        }
    }
}
