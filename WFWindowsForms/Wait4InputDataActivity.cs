using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WFWindowsForms
{

    public sealed class Wait4InputDataActivity : NativeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public OutArgument<int> UserInputMoney { get; set; }

        public InArgument<string> BookMarkName { get; set; }

        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }


        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(NativeActivityContext context)
        {
            string str = context.GetValue(BookMarkName);

            //创建书签
            context.CreateBookmark(str,new BookmarkCallback(InputDataCallBack));
        }

        private void InputDataCallBack(NativeActivityContext context, Bookmark bookmark, object value)
        {
            //当书签继续往下执行的时候，首先来执行此方法。
            context.SetValue(UserInputMoney,(int)value);

        }
    }
}
