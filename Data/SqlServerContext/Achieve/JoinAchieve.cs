
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.SqlServerContext.Achieve
{
    using System.Linq.Expressions;
    using DataFramework.Abstract;
    using DataFramework.Class;
    using DataFramework.Interface;
    using DataFramework.CodeAnalysis;





    public class JoinAchieve<T1, T2> : AbstractJoin<T1, T2>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, TJoin> Join<TJoin>(Expression<Func<T1, T2, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2> Where(Expression<Func<T1, T2, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2> WhereIF(bool IsWhere, Expression<Func<T1, T2, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2> OrderBy<TReturn>(Expression<Func<T1, T2, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2> OrderByDesc<TReturn>(Expression<Func<T1, T2, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2> GroupBy<TReturn>(Expression<Func<T1, T2, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3> : AbstractJoin<T1, T2, T3>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, T3, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, T3, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3> Where(Expression<Func<T1, T2, T3, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3> OrderBy<TReturn>(Expression<Func<T1, T2, T3, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3> GroupBy<TReturn>(Expression<Func<T1, T2, T3, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3, T4> : AbstractJoin<T1, T2, T3, T4>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, T3, T4, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, T3, T4, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3, T4> Where(Expression<Func<T1, T2, T3, T4, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3, T4> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3, T4, T5> : AbstractJoin<T1, T2, T3, T4, T5>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, T3, T4, T5, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, T3, T4, T5, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3, T4, T5> Where(Expression<Func<T1, T2, T3, T4, T5, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3, T4, T5, T6> : AbstractJoin<T1, T2, T3, T4, T5, T6>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, T3, T4, T5, T6, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3, T4, T5, T6> Where(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3, T4, T5, T6, T7> : AbstractJoin<T1, T2, T3, T4, T5, T6, T7>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, T3, T4, T5, T6, T7, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8> : AbstractJoin<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9> : AbstractJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : AbstractJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : AbstractJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : AbstractJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : AbstractJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : AbstractJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : AbstractJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN)
        {
            if (ON == null) throw new DbFrameException(" Join 连接对象不能为 NULL ！");
            this.analysis.CreateJoin(ON, this.SqlCode, _EJoinType);
            return new JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TJoin>(this.SqlCode, this.Ado, this.analysis);
        }
        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }




    public class JoinAchieve<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : AbstractJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
    {
        public JoinAchieve(SQL _Sql, AbstractAdo _Ado, Analysis _Analysis)
        {
            this.SqlCode = _Sql;
            this.Ado = _Ado;
            this.analysis = _Analysis;
        }


        public override IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>> Select)
        {
            //解析 列
            this.analysis.CreateSelect(Select, this.SqlCode);
            return new SelectAchieve(this.SqlCode, this.Ado, this.analysis);
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, bool>> Where)
        {
            this.SqlCode.Code_Where.Append(" AND ");
            this.analysis.CreateWhere(Where, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, bool>> Where)
        {
            if (IsWhere)
                return this.Where(Where);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderBy(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>> OrderBy)
        {
            this.analysis.CreateOrderByDESC(OrderBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>> GroupBy)
        {
            this.analysis.CreateGroupBy(GroupBy, this.SqlCode);
            return this;
        }

        public override IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> AddSqlString(Action<SQL> _Action)
        {
            _Action?.Invoke(this.SqlCode);
            return this;
        }
    }



}
