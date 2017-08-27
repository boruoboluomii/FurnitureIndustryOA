using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Context;
using Spring.Context.Support;

namespace SpringNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //IUserInfoDal userInfoDal =new UserInfoDal();
            //userInfoDal.Show();

            //下面走一个容器来 创建UserInfoDal实例

            //第一步  初始化容器
            IApplicationContext ctx = ContextRegistry.GetContext();

            //IUserInfoDal dal = ctx.GetObject("UserInfoDal") as IUserInfoDal;
            //dal.Show();

            IUserInfoDal dal1 = ctx.GetObject("UserInfoDal1") as IUserInfoDal;
            dal1.Show();

            //UserInfoServce userInfoServce = ctx.GetObject("UserInfoServce") as UserInfoServce;

            //userInfoServce.Show();


            Console.ReadKey();

        }
    }
}
