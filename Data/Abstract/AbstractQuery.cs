

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.Abstract
{
    using DataFramework.Class;
    using DataFramework.CodeAnalysis;
    using DataFramework.Interface;
    using System.Data;
    using System.Linq.Expressions;

    public abstract class AbstractQueryBase
    {
        protected SQL SqlCode { get; set; }

        protected AbstractAdo Ado { get; set; }

        protected Analysis analysis { get; set; }
    }

    public abstract class AbstractSelect : AbstractQueryBase, IQuery
    {
        public abstract TReturn Frist<TReturn>();
        public abstract List<TReturn> ToList<TReturn>();
        public abstract DataTable ToTable();
        public abstract List<DbParam> GetDbParam();
        public abstract SQL ToSql();
        public abstract int Count();
        //
        public abstract IQuery Top(int Top);
        public abstract IQuery Distinct();
        public abstract IQuery TakePage(int PageNumber, int PageSize);

    }

    public abstract class AbstractQuery<T> : AbstractQueryBase, IQuery<T>
    {
        public abstract IJoin<T, TJoin> Join<TJoin>(Expression<Func<T, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        public abstract IQuery Select<TReturn>(Expression<Func<T, TReturn>> Select);

        public abstract IQuery<T> Where(Expression<Func<T, bool>> Where);

        public abstract IQuery<T> WhereIF(bool IsWhere, Expression<Func<T, bool>> Where);

        public abstract IQuery<T> OrderBy<TReturn>(Expression<Func<T, TReturn>> OrderBy);

        public abstract IQuery<T> OrderByDesc<TReturn>(Expression<Func<T, TReturn>> OrderBy);

        public abstract IQuery<T> GroupBy<TReturn>(Expression<Func<T, TReturn>> GroupBy);

        public abstract IQuery<T> AddSqlString(Action<SQL> _Action);

        public abstract TReturn Frist<TReturn>();
        public abstract List<TReturn> ToList<TReturn>();
        public abstract DataTable ToTable();
        public abstract List<DbParam> GetDbParam();
        public abstract SQL ToSql();
        public abstract int Count();
        //
        public abstract IQuery Top(int Top);
        public abstract IQuery Distinct();
        public abstract IQuery TakePage(int PageNumber, int PageSize);
        //
        public abstract T Frist();
        public abstract List<T> ToList();

        IQuery<T> IQuery<T>.Top(int Top)
        {
            this.Top(Top);
            return this;
        }

        IQuery<T> IQuery<T>.Distinct()
        {
            this.Distinct();
            return this;
        }

        IQuery<T> IQuery<T>.TakePage(int PageNumber, int PageSize)
        {
            this.TakePage(PageNumber, PageSize);
            return this;
        }
        //
        public abstract TReturn Max<TReturn>(Expression<Func<T, TReturn>> Max);
        public abstract TReturn Min<TReturn>(Expression<Func<T, TReturn>> Min);
        public abstract TReturn Sum<TReturn>(Expression<Func<T, TReturn>> Sum);

    }



}




