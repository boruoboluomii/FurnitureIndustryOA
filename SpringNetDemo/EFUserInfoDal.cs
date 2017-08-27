using System;

namespace SpringNetDemo
{
    public class EFUserInfoDal:IUserInfoDal
    {

        public void Show()
        {
            Console.WriteLine("EFUserInfoDal  "+Name);
        }


        public string Name { get; set; }
    }
}