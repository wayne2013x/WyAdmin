
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.SqlServerContext.Achieve
{
    using System.Data;
    using System.Linq.Expressions;
    //
    using DataFramework.Class;
    using DataFramework.Interface;
    using DataFramework.CodeAnalysis;
    using DataFramework.Abstract;

    public class SelectAchieve : AbstractSelect
    {

        public SelectAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.Ado = _Ado;
            this.SqlCode = _Sql;
            this.analysis = _Analysis;
        }

        public override TReturn Frist<TReturn>()
        {
            this.Top(1);

            this.ToSql();

            var _T = this.Ado.QueryFirstOrDefault<TReturn>(this.SqlCode.Code.ToString(), this.SqlCode.GetDynamicParameters());
            if (_T == null)
                return Parser.CreateInstance<TReturn>();
            return _T;
        }
        public override List<TReturn> ToList<TReturn>()
        {
            this.ToSql();

            var _List = this.Ado.QueryBySql<TReturn>(this.SqlCode.Code.ToString(), this.SqlCode.GetDynamicParameters()).ToList();
            if (_List == null)
                return new List<TReturn>();
            return _List;
        }
        public override DataTable ToTable()
        {
            this.ToSql();

            return this.Ado.QueryDataTable(this.SqlCode.Code.ToString(), this.SqlCode.GetDynamicParameters());
        }
        public override List<DbParam> GetDbParam()
        {
            return this.SqlCode.Parameter;
        }
        public override SQL ToSql()
        {
            Analysis.ToSql(this.SqlCode, this.Ado.CommitState);

            return this.SqlCode;
        }
        public override int Count()
        {
            return this.analysis.Count(this.SqlCode);
        }
        //
        public override IQuery Top(int Top)
        {
            this.analysis.CreateTop(Top, this.SqlCode);
            return this;
        }
        public override IQuery Distinct()
        {
            this.analysis.CreateDistinct(this.SqlCode);
            return this;
        }
        public override IQuery TakePage(int PageNumber, int PageSize)
        {
            this.analysis.CreateTakePage(this.SqlCode, PageNumber, PageSize);
            return this;
        }

    }

    public class QueryAchieve<T> : AbstractQuery<T>
    {
        public QueryAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.Ado = _Ado;
            this.SqlCode = _Sql;
            this.analysis = _Analysis;
            this.SqlCode.IsAlias = false;
            this.analysis.CreateQuery<T>(this.SqlCode);
        }

        public override IJoin<T, TJoin> Join<TJoin>(Expression<Func<T, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            //给表设置别名
            var _Param = ON.Parameters[0];
            var _Alias = DbSettings.KeywordHandle(_Param.Name);
            this.SqlCode.Code_FromTab.Append(" AS " + _Alias);
            this.SqlCode.Alias[_Alias] = _Param.Type.Name;
            this.SqlCode.IsAlias = true;
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }

        public override IQuery Select<TReturn>(Expression<Func<T, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IQuery<T> Where(Expression<Func<T, bool>> Where)
        {
            if (Where == null) return this;
            //设置 不需要别名
            this.SqlCode.IsAlias = false;
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IQuery<T> WhereIF(bool IsWhere, Expression<Func<T, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IQuery<T> OrderBy<TReturn>(Expression<Func<T, TReturn>> OrderBy)
        {
            //设置 不需要别名
            this.SqlCode.IsAlias = false;
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IQuery<T> OrderByDesc<TReturn>(Expression<Func<T, TReturn>> OrderBy)
        {
            //设置 不需要别名
            this.SqlCode.IsAlias = false;
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IQuery<T> GroupBy<TReturn>(Expression<Func<T, TReturn>> GroupBy)
        {
            //设置 不需要别名
            this.SqlCode.IsAlias = false;
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IQuery<T> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }

        public override TReturn Frist<TReturn>()
        {
            this.Top(1);

            this.ToSql();

            var _T = this.Ado.QueryFirstOrDefault<TReturn>(this.SqlCode.Code.ToString(), this.SqlCode.GetDynamicParameters());
            if (_T == null)
                return Parser.CreateInstance<TReturn>();
            return _T;
        }
        public override List<TReturn> ToList<TReturn>()
        {
            this.ToSql();

            var _List = this.Ado.QueryBySql<TReturn>(this.SqlCode.Code.ToString(), this.SqlCode.GetDynamicParameters()).ToList();
            if (_List == null)
                return new List<TReturn>();
            return _List;
        }
        public override DataTable ToTable()
        {
            this.ToSql();

            return this.Ado.QueryDataTable(this.SqlCode.Code.ToString(), this.SqlCode.GetDynamicParameters());
        }
        public override List<DbParam> GetDbParam()
        {
            return this.SqlCode.Parameter;
        }
        public override SQL ToSql()
        {
            Analysis.ToSql(this.SqlCode, this.Ado.CommitState);

            return this.SqlCode;
        }
        public override int Count()
        {
            return this.analysis.Count(this.SqlCode);
        }

        //
        public override IQuery Top(int Top)
        {
            this.analysis.CreateTop(Top, this.SqlCode);
            return this;
        }
        public override IQuery Distinct()
        {
            this.analysis.CreateDistinct(this.SqlCode);
            return this;
        }
        public override IQuery TakePage(int PageNumber, int PageSize)
        {
            this.analysis.CreateTakePage(this.SqlCode, PageNumber, PageSize);
            return this;
        }
        //
        public override T Frist()
        {
            this.Top(1);

            this.ToSql();

            var _T = this.Ado.QueryFirstOrDefault<T>(this.SqlCode.Code.ToString(), this.SqlCode.GetDynamicParameters());
            if (_T == null)
                return Parser.CreateInstance<T>();
            return _T;
        }
        public override List<T> ToList()
        {
            this.ToSql();

            var _List = this.Ado.QueryBySql<T>(this.SqlCode.Code.ToString(), this.SqlCode.GetDynamicParameters()).ToList();
            if (_List == null)
                return new List<T>();
            return _List;
        }
        //
        public override TReturn Max<TReturn>(Expression<Func<T, TReturn>> Max)
        {
            return this.analysis.Max<TReturn>(Max, this.SqlCode);
        }

        public override TReturn Min<TReturn>(Expression<Func<T, TReturn>> Min)
        {
            return this.analysis.Min<TReturn>(Min, this.SqlCode);
        }

        public override TReturn Sum<TReturn>(Expression<Func<T, TReturn>> Sum)
        {
            return this.analysis.Sum<TReturn>(Sum, this.SqlCode);
        }
        //



    }





}


