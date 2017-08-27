namespace Heima8.OA.IDAL
{
    public partial interface IDbSession
    {
        #region 改成了模板自动生成
        //IUserInfoDal UserInfoDal { get;}
        //IOrderInfoDal OrderInfoDal { get; } 
        #endregion

        int SaveChanges();
    }
}