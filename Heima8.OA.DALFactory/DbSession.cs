using Heima8.OA.EFDAL;
using Heima8.OA.IDAL;

namespace Heima8.OA.DALFactory
{
    public partial class DbSession :IDbSession
    {
        #region 简单工厂或者抽象工厂部分  有模板自动生成
        //public IUserInfoDal UserInfoDal
        //{
        //    get { return StaticDalFactory.GetUserInfoDal(); }
        //}
        //public IOrderInfoDal OrderInfoDal
        //{
        //    get { return StaticDalFactory.GetOrderInfoDal(); }
        //} 
        #endregion

        /// <summary>
        /// 拿到当前的EF的上下文，然后进行 把修改实体进行一个整体提交。
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
             return DbContextFactory.GetCurrentDbContext().SaveChanges();
        }
    }
}