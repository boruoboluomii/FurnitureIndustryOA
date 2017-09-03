using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heima8.OA.DALFactory;
using Heima8.OA.EFDAL;
using Heima8.OA.IBLL;
using Heima8.OA.IDAL;
using Heima8.OA.Model;
using Heima8.OA.NHDAL;

namespace Heima8.OA.BLL
{
    //模块内高内聚。模块间低耦合。

    //变化点：1、跨数据库。有mysql，slqserver2、数据库访问驱动层驱动变化。

    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService //crud
    {




        //1233
        #region old2
        //让一个菜鸟到另一个一般开发人员。
        //依赖接口编程。
        //IUserInfoDal UserInfoDal = new UserInfoDal();

        //稍微高级点开发人员也不能这么写。
        //private IUserInfoDal UserInfoDal = StaticDalFactory.GetUserInfoDal();

        //DbSession dbSession =new DbSession();
        //IDbSession dbSession = new DbSession();
        //private IDbSession dbSession = DbSessionFactory.GetCurrentDbSession(); 
        #endregion

        #region old

        //更高级：IoC、DI 依赖注入的方式。  Spring.Net

        //public UserInfo Add(UserInfo userInfo)//UnitWork单元工作模式。
        //{
        //    //10000:每个用户
        //    dbSession.UserInfoDal.Add(userInfo);
        //    if (dbSession.SaveChanges() > 0)
        //    {

        //    }
        //    dbSession.UserInfoDal.Add(new UserInfo());
        //    dbSession.OrderInfoDal.Delete(new OrderInfo());
        //    dbSession.OrderInfoDal.Update(new OrderInfo());

        //    dbSession.SaveChanges();//数据提交的权利从数据库访问层提到了业务逻辑层。

        //    //return UserInfoDal.Add(userInfo);
        //} 
        #endregion

        //public override void SetCurrentDal()
        //{
        //    CurrentDal = this.DbSession.UserInfoDal;
        //}
        #region 多条件查询
        public IQueryable<UserInfo> LoagPageData(Model.Param.UserQueryParam userQueryParam)
        {
            short normalFlag = (short)Heima8.OA.Model.Enum.DelFlagEnum.Normal;
            var temp = DbSession.UserInfoDal.GetEntities(u => u.DelFlag == normalFlag);

            //过滤
            if (!string.IsNullOrEmpty(userQueryParam.SchName))
            {
                temp = temp.Where(u => u.UName.Contains(userQueryParam.SchName)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(userQueryParam.SchRemark))
            {
                temp = temp.Where(u => u.Remark.Contains(userQueryParam.SchRemark)).AsQueryable();
            }

            userQueryParam.Total = temp.Count();

            //分页
            return temp.OrderBy(u => u.ID)
                .Skip(userQueryParam.PageSize * (userQueryParam.PageIndex - 1))
                .Take(userQueryParam.PageSize).AsQueryable();


        }
        #endregion


        public bool SetRole(int userId, List<int> roleIds)
        {
            var user = DbSession.UserInfoDal.GetEntities(u =>u.DelFlag == DeleteFlag.DelflagNormal && u.ID == userId).FirstOrDefault();
            user.RoleInfo.Clear();//全剁掉。

            var allRoles = DbSession.RoleInfoDal.GetEntities(r => r.DelFlag == DeleteFlag.DelflagNormal && roleIds.Contains(r.ID));
            foreach (var roleInfo in allRoles)
            {
                user.RoleInfo.Add(roleInfo);//加最新的角色。
            }
            DbSession.SaveChanges();
            return true;
        }

        

    }
}
