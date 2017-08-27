using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WFWinFrmDemoo
{
    //1、让工作流停下来。
    //2、把用户通过WinFrom输入的信息传入到工作流里面去。
    public sealed class InputMoneyActivity : NativeActivity
    {
        // 定义一个字符串类型的活动输入参数
        

        public OutArgument<int> OutMoney { get; set; }
        public InArgument<string> InBookMarkName { get; set; }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }
        

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(NativeActivityContext context)
        {
            // 获取 Text 输入参数的运行时值
            //string text = context.GetValue(this.Text);

            //int moeny = int.Parse(Console.ReadLine());

            //1、让工作流停下来。

            //2、有了数据之后，还得让工作流继续往下流动。

            //由总工作流传来的数据。
            string bookMarkName = context.GetValue(InBookMarkName);

            Console.WriteLine("创建书签");
            //创建书签。
            context.CreateBookmark(bookMarkName,new BookmarkCallback(CallBackFun));


            //context.SetValue(OutMoney, moeny);

        }

        //也就是  继续  书签的时候执行的代码。
        private void CallBackFun(NativeActivityContext context, Bookmark bookmark, object value)
        {
            context.SetValue(OutMoney,(int)value);
            Console.WriteLine("书签继续执行....");
        }
    }
}
