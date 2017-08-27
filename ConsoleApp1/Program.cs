using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heima8.OA.EFDAL;
using Heima8.OA.Model;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInfoDal dal = new UserInfoDal();
            var userInfo = dal.GetEntities(u => true).FirstOrDefault();

            //var userInfo =
            //    UserInfoService.GetEntities(u => u.UName == name && u.Pwd == pwd && u.DelFlag == delNormal)
            //                   .FirstOrDefault();
            Console.WriteLine("ok");
            Console.ReadLine();
        }
    }
}
