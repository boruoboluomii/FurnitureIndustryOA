using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Heima8.OA.Workflow
{

    public sealed class Wait4InputDataActivity<T> : NativeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InArgument<string> InBookmarkName { get; set; }
        public OutArgument<T> OutPutData { get; set; }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }


        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(NativeActivityContext context)
        {
            // 获取 Text 输入参数的运行时值
            string text = context.GetValue(this.InBookmarkName);

            context.CreateBookmark(text, new BookmarkCallback(MethodCallBack));
        }


        //回调函数。
        private void MethodCallBack(NativeActivityContext context, Bookmark bookmark, object value)
        {
            context.SetValue(OutPutData,(T)value);
        }
    }
}
