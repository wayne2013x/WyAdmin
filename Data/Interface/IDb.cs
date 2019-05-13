using System;
using System.Collections.Generic;

namespace DataFramework.Interface
{
    using System.Data;
    //
    using System.Linq.Expressions;

    public interface IDb : IAdo
    {
        /*Insert*/
        object Insert<T>(T Entity);
        object Insert<T>(Expression<Func<T>> Entity);
        /*Update*/
        bool Update<T>(Expression<Func<T, bool>> Where, T Entity);
        bool Update<T>(Expression<Func<T, bool>> Where, Expression<Func<T>> Entity);
        bool UpdateById<T>(T Entity);
        /*Delete*/
        bool Delete<T>(Expression<Func<T, bool>> Where);
        bool DeleteById<T>(object Id);
        /*Select*/
        IQuery<T> Query<T>();
        IQuery<T> Query<T>(Expression<Func<T, bool>> Where);
        T Find<T>(Expression<Func<T, bool>> Where);
        T FindById<T>(object Id);
        List<T> FindList<T>(Expression<Func<T, bool>> Where);

    }
}
