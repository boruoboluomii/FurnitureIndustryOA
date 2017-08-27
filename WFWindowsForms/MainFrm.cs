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
using System.Activities.DurableInstancing;


namespace WFWindowsForms
{
    public partial class MainFrm : Form
    {
        WorkflowApplication WFApp { get; set; }

        public MainFrm()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Heima8OADb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private void btnStart_Click(object sender, EventArgs e)
        {
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(connectionString);

            WorkflowApplication wfApp =new WorkflowApplication(
                new StateMoneyActivity1xaml(),
                new Dictionary<string, object>()
                    {
                        {"InBookMarkName",this.txtBookMarkName.Text}
                    });

            wfApp.InstanceStore = store;

            wfApp.Idle += OnWfAppIdel;
            wfApp.OnUnhandledException += OnWfAppException;
            wfApp.Unloaded = a => { Console.WriteLine("工作流卸载了..."); };
            wfApp.Aborted = a => { Console.WriteLine("Aborted"); };
            
            //启用序列化必须使用此方法。
            wfApp.PersistableIdle = a => { return PersistableIdleAction.Unload; };
            wfApp.Run();//启动工作流

            this.txtAppId.Text = wfApp.Id.ToString();

            WFApp = wfApp;
            //wfApp.ResumeBookmark();
        }

        private UnhandledExceptionAction OnWfAppException(WorkflowApplicationUnhandledExceptionEventArgs arg)
        {
            //arg.UnhandledException
            Console.WriteLine("出现了未处理的异常："+arg.UnhandledException.ToString());
            return UnhandledExceptionAction.Terminate;
        }

        private void OnWfAppIdel(WorkflowApplicationIdleEventArgs obj)
        {
            Console.WriteLine("工作流停下来。 ");
        }

       

        private void btnStartStatWF_Click(object sender, EventArgs e)
        {
            WorkflowApplication wfApp =new WorkflowApplication(new StateActivity());
            wfApp.Run();
        }


        private void btnContinue_Click(object sender, EventArgs e)
        {
            //让工作流在书签的位置继续往下流动
            //WFApp.ResumeBookmark(txtBookMarkName.Text, int.Parse(txtMoney.Text));

            //从数据库中加载工作流实例的状态
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(connectionString);

            WorkflowApplication wfApp = new WorkflowApplication(new StateMoneyActivity1xaml());

            wfApp.InstanceStore = store;

            wfApp.Idle += OnWfAppIdel;
            wfApp.OnUnhandledException += OnWfAppException;
            wfApp.Unloaded = a => { Console.WriteLine("工作流卸载了..."); };
            wfApp.Aborted = a => { Console.WriteLine("Aborted"); };

            //启用序列化必须使用此方法。
            wfApp.PersistableIdle = a => { return PersistableIdleAction.Unload; };

            //加载数据库中的实例的状态。
            wfApp.Load(Guid.Parse(txtAppId.Text));
            wfApp.ResumeBookmark(txtBookMarkName.Text, int.Parse(txtMoney.Text));
        }

    }
}
