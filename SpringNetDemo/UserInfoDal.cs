using System;

namespace SpringNetDemo
{
    public class UserInfoDal :IUserInfoDal
    {
        public UserInfoDal(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void Show()
        {
            Console.WriteLine("越教越兽 by 老牛" + Name);
        }
    }
}