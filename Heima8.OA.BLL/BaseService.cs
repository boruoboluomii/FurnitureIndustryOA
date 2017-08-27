using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Heima8.OA.DALFactory;
using Heima8.OA.IDAL;
using Heima8.OA.Model;

namespace Heima8.OA.BLL
{
    /// <summary>
    /// 父类要逼迫自己给父类的一个属性赋值。
    /// 赋值的操作必须在父类的方法调用之前先执行。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> where T : class ,new()
    {
        public IBaseDal<T> CurrentDal { get; set; }


        public IDbSession DbSession
        {
            get
            {
                return DbSessionFactory.GetCurrentDbSession();
            }
        }

        public BaseService()//基类的构造函数
        {
            SetCurrentDal();//抽象方法。
            //CurrentDal = currentDal;
        }

        public abstract void SetCurrentDal();//抽象方法：要求子类必须实现。

        #region 查询

        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.GetEntities(whereLambda);
        }



        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                Expression<Func<T, bool>> whereLambda,
                                                Expression<Func<T, S>> orderByLambda,
                                                bool isAsc)
        {
            return CurrentDal.GetPageEntities(pageSize, pageIndex, out total, whereLambda, orderByLambda, isAsc);
        }
        #endregion

        #region cud

        public int DeleteListByLogical(List<int> ids)
        {
            CurrentDal.DeleteListByLogical(ids);
            return DbSession.SaveChanges();
        }


        //批量删除
        public int DeleteList(List<int> ids)
        {
            foreach (var id in ids)
            {
                CurrentDal.Delete(id);
            }

            return DbSession.SaveChanges();

        }

        public bool Delete(int id)
        {
            CurrentDal.Delete(id);
            return DbSession.SaveChanges() > 0;
        }


        public T Add(T entity)
        {
            //return DbSession.OrderInfoDal.Add();
            CurrentDal.Add(entity);
            DbSession.SaveChanges();
            return entity;
        }

        public bool Update(T entity)
        {
            CurrentDal.Update(entity);
            return DbSession.SaveChanges() > 0;
        }


        public bool Delete(T entity)
        {
            CurrentDal.Delete(entity);
            return DbSession.SaveChanges() > 0;
        }

        #endregion

    }
}