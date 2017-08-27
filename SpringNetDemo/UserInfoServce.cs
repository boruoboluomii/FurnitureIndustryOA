using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo
{
    public class UserInfoServce
    {
        public IUserInfoDal UserInfoDal { get; set; }

        public void Show()
        {
            UserInfoDal.Show();
            Console.WriteLine("UserInfoService ");
        }
    }
}
