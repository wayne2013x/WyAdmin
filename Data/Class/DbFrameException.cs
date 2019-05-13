using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.Class
{
    /// <summary>
    /// DbFrame 异常对象
    /// </summary>
    public class DbFrameException : Exception
    {

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 WebException 类的新实例。
        /// </summary>
        /// <param name="Messager">解释异常原因的错误消息。</param>
        public DbFrameException(string Messager)
            : base(Messager)
        {

        }

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 WebException 类的新实例。
        /// </summary>
        /// <param name="Messager">解释异常原因的错误消息。</param>
        /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用</param>
        public DbFrameException(string Messager, Exception InnerException)
            : base(Messager, InnerException)
        {

        }

    }
}
