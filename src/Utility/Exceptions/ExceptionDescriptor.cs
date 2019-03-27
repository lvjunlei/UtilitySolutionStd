#region ExceptionDescriptor 文件信息
/***********************************************************
**文 件 名：ExceptionDescriptor 
**命名空间：Utility.Exceptions 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-27 07:06:35 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Utility.Exceptions
{
    /// <summary>
    /// 包含一个 <see cref="Exception"/> 对象的关键特征，可使用此对象的实例判断两个不同的异常实例是否极有可能表示同一个异常。
    /// </summary>
    [DebuggerDisplay("{" + nameof(TypeName) + ",nq}: {FrameSignature[0],nq}")]
    public class ExceptionDescriptor : IEquatable<ExceptionDescriptor>
    {
        /// <summary>
        /// 获取此异常的类型名称。
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// 获取此异常堆栈中的所有帧的方法签名，指的是在一个类型中不会冲突的最小部分，所以不含返回值和可访问性。
        /// 比如 private void Foo(Bar b); 方法，在这里会写成 Foo(Bar b)。
        /// </summary>
        public IReadOnlyList<string> FrameSignature { get; }

        /// <summary>
        /// 从一个异常中提取出关键的异常特征，并创建 <see cref="ExceptionDescriptor"/> 的新实例。
        /// </summary>
        /// <param name="exception">要提取特征的异常。</param>
        public ExceptionDescriptor(Exception exception)
        {
            var type = exception.GetType().FullName;
            var stackFrames = new StackTrace(exception).GetFrames() ?? new StackFrame[0];
            var frames = stackFrames.Select(x => x.GetMethod()).Select(m =>
                $"{m.DeclaringType?.FullName ?? "null"}.{m.Name}({string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))})");
            TypeName = type;
            FrameSignature = frames.ToList();
        }

        /// <summary>
        /// 根据异常的信息本身创建异常的关键特征。
        /// </summary>
        /// <param name="typeName">异常类型的完整名称。</param>
        /// <param name="frameSignature">
        /// 异常堆栈中的所有帧的方法签名，指的是在一个类型中不会冲突的最小部分，所以不含返回值和可访问性。
        /// 比如 private void Foo(Bar b); 方法，在这里会写成 Foo(Bar b)。
        /// </param>
        public ExceptionDescriptor(string typeName, IReadOnlyList<string> frameSignature)
        {
            TypeName = typeName;
            FrameSignature = frameSignature;
        }

        /// <summary>
        /// 判断此异常特征对象是否与另一个对象实例相等。
        /// 如果参数指定的对象是 <see cref="ExceptionDescriptor"/>，则判断特征是否相等。
        /// </summary>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((ExceptionDescriptor)obj);
        }

        /// <summary>
        /// 判断此异常特征与另一个异常特征是否是表示同一个异常。
        /// </summary>
        public bool Equals(ExceptionDescriptor other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(TypeName, other.TypeName) && FrameSignature.SequenceEqual(other.FrameSignature);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((TypeName != null ? StringComparer.InvariantCulture.GetHashCode(TypeName) : 0) * 397) ^
                       (FrameSignature != null ? FrameSignature.GetHashCode() : 0);
            }
        }

        /// <summary>
        /// 判断两个异常特征是否是表示同一个异常。
        /// </summary>
        public static bool operator ==(ExceptionDescriptor left, ExceptionDescriptor right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// 判断两个异常特征是否表示的不是同一个异常。
        /// </summary>
        public static bool operator !=(ExceptionDescriptor left, ExceptionDescriptor right)
        {
            return !Equals(left, right);
        }
    }
}
