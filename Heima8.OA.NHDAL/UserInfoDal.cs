using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Heima8.OA.IDAL;
using Heima8.OA.Model;

namespace Heima8.OA.NHDAL
{
    public class UserInfoDal :IUserInfoDal
    {
        
        public UserInfo Add(UserInfo entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(UserInfo entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(UserInfo entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserInfo> GetEntities(Expression<Func<UserInfo, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserInfo> GetPageEntities<S>(int pageSize, int pageIndex, out int total, Expression<Func<UserInfo, bool>> whereLambda, Expression<Func<UserInfo, S>> orderByLambda, bool isAsc)
        {
            throw new NotImplementedException();
        }


        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }


        public int DeleteListByLogical(List<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
