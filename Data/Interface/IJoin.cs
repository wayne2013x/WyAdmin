
using System;

namespace DataFramework.Interface
{
    //
    using System.Linq.Expressions;
    using DataFramework.Class;




    public interface IJoin<T1, T2>
    {
        IJoin<T1, T2, TJoin> Join<TJoin>(Expression<Func<T1, T2, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, TReturn>> Select);

        IJoin<T1, T2> Where(Expression<Func<T1, T2, bool>> Where);

        IJoin<T1, T2> WhereIF(bool IsWhere, Expression<Func<T1, T2, bool>> Where);

        IJoin<T1, T2> OrderBy<TReturn>(Expression<Func<T1, T2, TReturn>> OrderBy);

        IJoin<T1, T2> OrderByDesc<TReturn>(Expression<Func<T1, T2, TReturn>> OrderBy);

        IJoin<T1, T2> GroupBy<TReturn>(Expression<Func<T1, T2, TReturn>> GroupBy);

        IJoin<T1, T2> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3>
    {
        IJoin<T1, T2, T3, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, TReturn>> Select);

        IJoin<T1, T2, T3> Where(Expression<Func<T1, T2, T3, bool>> Where);

        IJoin<T1, T2, T3> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, bool>> Where);

        IJoin<T1, T2, T3> OrderBy<TReturn>(Expression<Func<T1, T2, T3, TReturn>> OrderBy);

        IJoin<T1, T2, T3> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, TReturn>> OrderBy);

        IJoin<T1, T2, T3> GroupBy<TReturn>(Expression<Func<T1, T2, T3, TReturn>> GroupBy);

        IJoin<T1, T2, T3> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3, T4>
    {
        IJoin<T1, T2, T3, T4, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, TReturn>> Select);

        IJoin<T1, T2, T3, T4> Where(Expression<Func<T1, T2, T3, T4, bool>> Where);

        IJoin<T1, T2, T3, T4> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, bool>> Where);

        IJoin<T1, T2, T3, T4> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, TReturn>> GroupBy);

        IJoin<T1, T2, T3, T4> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3, T4, T5>
    {
        IJoin<T1, T2, T3, T4, T5, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, TReturn>> Select);

        IJoin<T1, T2, T3, T4, T5> Where(Expression<Func<T1, T2, T3, T4, T5, bool>> Where);

        IJoin<T1, T2, T3, T4, T5> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, bool>> Where);

        IJoin<T1, T2, T3, T4, T5> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, TReturn>> GroupBy);

        IJoin<T1, T2, T3, T4, T5> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3, T4, T5, T6>
    {
        IJoin<T1, T2, T3, T4, T5, T6, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, TReturn>> Select);

        IJoin<T1, T2, T3, T4, T5, T6> Where(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, TReturn>> GroupBy);

        IJoin<T1, T2, T3, T4, T5, T6> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3, T4, T5, T6, T7>
    {
        IJoin<T1, T2, T3, T4, T5, T6, T7, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TReturn>> Select);

        IJoin<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TReturn>> GroupBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>> Select);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>> GroupBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>> Select);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>> GroupBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>> Select);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>> GroupBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>> Select);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>> GroupBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    {
        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>> Select);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>> GroupBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    {
        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>> Select);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>> GroupBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    {
        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>> Select);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>> GroupBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    {
        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TJoin> Join<TJoin>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TJoin, bool>> ON, EJoinType _EJoinType = EJoinType.LEFT_JOIN);

        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>> Select);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>> GroupBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> AddSqlString(Action<SQL> _Action);

    }


    public interface IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
    {


        IQuery Select<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>> Select);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, bool>> Where);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> OrderBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> OrderByDesc<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>> OrderBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> GroupBy<TReturn>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>> GroupBy);

        IJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> AddSqlString(Action<SQL> _Action);

    }


}
