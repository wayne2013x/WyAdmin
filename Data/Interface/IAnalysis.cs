using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.Interface
{
    using DataFramework.Class;
    using System.Linq.Expressions;

    public interface IAnalysis
    {
        /// <summary>
        /// Insert 解析并执行
        /// </summary>
        object Insert<T>(MemberInitExpression _MemberInitExpression);

        /// <summary>
        /// Update 解析并执行
        /// </summary>
        bool Update<T>(MemberInitExpression _MemberInitExpression, Expression<Func<T, bool>> Where);

        /// <summary>
        /// 删除
        /// </summary>
        bool Delete<T>(Expression<Func<T, bool>> Where);
        
        /// <summary>
        /// 获取 Count 
        /// </summary>
        int Count(SQL _Sql);

        /// <summary>
        /// Max
        /// </summary>
        T Max<T>(LambdaExpression _LambdaExpression, SQL _Sql);

        /// <summary>
        /// Min
        /// </summary>
        T Min<T>(LambdaExpression _LambdaExpression, SQL _Sql);

        /// <summary>
        /// Sum
        /// </summary>
        T Sum<T>(LambdaExpression _LambdaExpression, SQL _Sql);






        /*****************Create*********************/


        /// <summary>
        /// 创建 Query 对象
        /// </summary>
        void CreateQuery<T>(SQL _Sql);

        /// <summary>
        /// 创建 GroupBy 对象
        /// </summary>
        void CreateGroupBy(LambdaExpression _LambdaExpression, SQL _Sql);

        /// <summary>
        /// 创建 OrderBy 对象
        /// </summary>
        void CreateOrderBy(LambdaExpression _LambdaExpression, SQL _Sql);

        /// <summary>
        /// 创建 OrderBy  desc 对象
        /// </summary>
        void CreateOrderByDESC(LambdaExpression _LambdaExpression, SQL _Sql);

        /// <summary>
        /// 创建 Select 对象
        /// </summary>
        void CreateSelect(LambdaExpression _LambdaExpression, SQL _Sql);

        /// <summary>
        /// 创建 Where 对象
        /// </summary>
        void CreateWhere(LambdaExpression _LambdaExpression, SQL _Sql);

        /// <summary>
        /// 创建 Join 对象
        /// </summary>
        void CreateJoin(LambdaExpression _LambdaExpression, SQL _Sql, EJoinType _EJoinType);

        /// <summary>
        /// 创建 TOP 对象
        /// </summary>
        void CreateTop(int Top, SQL _Sql);

        /// <summary>
        /// 创建 Distinct 对象
        /// </summary>
        void CreateDistinct(SQL _Sql);

        /// <summary>
        /// 创建 分页 对象
        /// </summary>
        void CreateTakePage(SQL _Sql, int PageNumber, int PageSize);
        


    }
}
