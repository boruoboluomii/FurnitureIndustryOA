using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Heima8.OA.IBLL
{
    public interface IBaseService<T> where T : class ,new()
    {
        #region 查询

         IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda);


         IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                 Expression<Func<T, bool>> whereLambda,
                                                 Expression<Func<T, S>> orderByLambda,
                                                 bool isAsc);

        #endregion


        //GoldenKey 来拜访过了，啊哈哈哈哈哈哈哈

        #region cud

        T Add(T entity);

        bool Update(T entity);


        bool Delete(T entity);

        bool Delete(int id);

        int DeleteList(List<int> ids);

        int DeleteListByLogical(List<int> ids);

        #endregion
    }
}