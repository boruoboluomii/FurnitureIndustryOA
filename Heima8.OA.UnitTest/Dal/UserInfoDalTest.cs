using System;
using System.Linq;
using Heima8.OA.EFDAL;
using Heima8.OA.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heima8.OA.UnitTest
{
    [TestClass]
    public class UserInfoDalTest
    {
        //到此一游
        [TestMethod]
        public void TestGetUsers()
        {
            //单元测试是没用的，浪费开发时间而已？
            //1、节省的改bug的时间。

            //2、对项目非常有自信。

            //3、单元测试也是一种设计（写单元测试的时候促进对方法进行再思考）

            //4、它也是一种 项目管理的手段。TDD：测试驱动开发。

            //测试  获取数据的方法。
            UserInfoDal dal =new UserInfoDal();

            //单元测y试必须自己处理数据，不能依赖第三方数据。如果依赖数据那么先自己创建数据，然后用完之后再清除数据。
            //创建测试的数据
            for (var i = 0; i < 10; i++)
            {
                dal.Add(new UserInfo()
                    {
                        UName = i+"ssss"
                    });
            }

            IQueryable<UserInfo> temp = dal.GetEntities(u => u.DelFlag == DeleteFlag.DelflagNormal && u.UName.Contains("ss"));

            //断言
            Assert.AreEqual(true,temp.Count()>=10);
            ///////////////////葛洋洋
            ////喜洋洋，慢洋洋！
        }
    }
}
