using System;
using System.Collections.Generic;

namespace DataFramework.Interface
{
    //
    using System.Data;
    using System.Linq.Expressions;
    using DataFramework.Class;

    public interface IQuery
    {
        TReturn Frist<TReturn>();
        List<TReturn> ToList<TReturn>();
        DataTable ToTable();
        List<DbParam> GetDbParam();
        SQL ToSql();
        int Count();
        //
        IQuery Top(int Top);
        IQuery Distinct();
        IQuery TakePage(int PageNumber, int PageSize);
    }

    public interface IQuery<T> : IQuery
    {
        IJoin<T, TJoin> Join<TJoin>(Expression<Func<T, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T, TReturn>> Select);

        IQuery<T> Where(Expression<Func<T, bool>> Where);

        IQuery<T> WhereIF(bool IsWhere, Expression<Func<T, bool>> Where);

        IQuery<T> OrderBy<TReturn>(Expression<Func<T, TReturn>> OrderBy);

        IQuery<T> OrderByDesc<TReturn>(Expression<Func<T, TReturn>> OrderBy);

        IQuery<T> GroupBy<TReturn>(Expression<Func<T, TReturn>> GroupBy);

        IQuery<T> AddSqlString(Action<SQL> _Action);

        T Frist();
        List<T> ToList();
        //
        new IQuery<T> Top(int Top);
        new IQuery<T> Distinct();
        new IQuery<T> TakePage(int PageNumber, int PageSize);
        //
        TReturn Max<TReturn>(Expression<Func<T, TReturn>> Max);
        TReturn Min<TReturn>(Expression<Func<T, TReturn>> Min);
        TReturn Sum<TReturn>(Expression<Func<T, TReturn>> Sum);
    }





}
