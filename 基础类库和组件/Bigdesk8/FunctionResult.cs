using System;

namespace Bigdesk8
{
    /// <summary>
    /// 功能、处理过程等的返回结果
    /// 用于不关注返回结果的功能处理函数
    /// </summary>
    public class FunctionResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FunctionResult()
        {
            this.Status = FunctionResultStatus.None;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        public FunctionResult(FunctionResultStatus status)
        {
            this.Status = status;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        /// <param name="message">异常情况的信息，提示、警告、错误信息都记录在这个属性上</param>
        public FunctionResult(FunctionResultStatus status, string message)
        {
            this.Status = status;
            this.Message = new Exception(message);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        /// <param name="exception">异常情况的信息，提示、警告、错误信息都记录在这个属性上</param>
        public FunctionResult(FunctionResultStatus status, Exception exception)
        {
            this.Status = status;
            this.Message = exception;
        }

        /// <summary>
        /// 功能函数在执行过程当中，出现的各种异常情况
        /// </summary>
        public FunctionResultStatus Status { get; set; }

        /// <summary>
        /// 异常情况的信息，提示、警告、错误信息都记录在这个属性上
        /// </summary>
        public Exception Message { get; set; }
    }

    /// <summary>
    /// 功能、处理过程等的返回结果
    /// 用于不关注返回结果的功能处理函数
    /// </summary>
    public sealed class FunctionResultException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FunctionResultException()
            : base("执行结果")
        {
            this.Result = new FunctionResult();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        public FunctionResultException(FunctionResultStatus status)
            : base("执行结果")
        {
            this.Result = new FunctionResult(status);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        /// <param name="message">异常情况的信息，提示、警告、错误信息都记录在这个属性上</param>
        public FunctionResultException(FunctionResultStatus status, string message)
            : base(message)
        {
            this.Result = new FunctionResult(status, message);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        /// <param name="exception">异常情况的信息，提示、警告、错误信息都记录在这个属性上</param>
        public FunctionResultException(FunctionResultStatus status, Exception exception)
            : base(exception.Message, exception)
        {
            this.Result = new FunctionResult(status, exception);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="result">功能函数返回结果</param>
        public FunctionResultException(FunctionResult result)
        {
            this.Result = result;
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        public FunctionResult Result { get; private set; }
    }

    /// <summary>
    /// 功能、处理过程等的返回结果
    /// 用于关注返回结果的功能处理函数
    /// </summary>
    /// <typeparam name="T">返回结果的类型</typeparam>
    public class FunctionResult<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FunctionResult()
        {
            this.Status = FunctionResultStatus.None;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        public FunctionResult(FunctionResultStatus status)
        {
            this.Status = status;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        /// <param name="message">异常情况的信息，提示、警告、错误信息都记录在这个属性上</param>
        public FunctionResult(FunctionResultStatus status, string message)
        {
            this.Status = status;
            this.Message = new Exception(message);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        /// <param name="exception">异常情况的信息，提示、警告、错误信息都记录在这个属性上</param>
        public FunctionResult(FunctionResultStatus status, Exception exception)
        {
            this.Status = status;
            this.Message = exception;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        /// <param name="message">异常情况的信息，提示、警告、错误信息都记录在这个属性上</param>
        /// <param name="result">函数返回结果</param>
        public FunctionResult(FunctionResultStatus status, string message, T result)
            : this(status, message)
        {
            this.Result = result;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        /// <param name="exception">异常情况的信息，提示、警告、错误信息都记录在这个属性上</param>
        /// <param name="result">函数返回结果</param>
        public FunctionResult(FunctionResultStatus status, Exception exception, T result)
            : this(status, exception)
        {
            this.Result = result;
        }

        /// <summary>
        /// 函数返回结果
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// 功能函数在执行过程当中，出现的各种异常情况
        /// </summary>
        public FunctionResultStatus Status { get; set; }

        /// <summary>
        /// 异常情况的信息，提示、警告、错误信息都记录在这个属性上
        /// </summary>
        public Exception Message { get; set; }
    }

    /// <summary>
    /// 功能、处理过程等的返回结果
    /// 用于关注返回结果的功能处理函数
    /// </summary>
    /// <typeparam name="T">返回结果的类型</typeparam>
    public sealed class FunctionResultException<T> : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FunctionResultException()
            : base("执行结果")
        {
            this.Result = new FunctionResult<T>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        public FunctionResultException(FunctionResultStatus status)
            : base("执行结果")
        {
            this.Result = new FunctionResult<T>(status);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        /// <param name="message">异常情况的信息，提示、警告、错误信息都记录在这个属性上</param>
        public FunctionResultException(FunctionResultStatus status, string message)
            : base(message)
        {
            this.Result = new FunctionResult<T>(status, message);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        /// <param name="exception">异常情况的信息，提示、警告、错误信息都记录在这个属性上</param>
        public FunctionResultException(FunctionResultStatus status, Exception exception)
            : base(exception.Message, exception)
        {
            this.Result = new FunctionResult<T>(status, exception);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        /// <param name="message">异常情况的信息，提示、警告、错误信息都记录在这个属性上</param>
        /// <param name="result">函数返回结果</param>
        public FunctionResultException(FunctionResultStatus status, string message, T result)
            : base(message)
        {
            this.Result = new FunctionResult<T>(status, message, result);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">功能函数在执行过程当中，出现的各种异常情况</param>
        /// <param name="exception">异常情况的信息，提示、警告、错误信息都记录在这个属性上</param>
        /// <param name="result">函数返回结果</param>
        public FunctionResultException(FunctionResultStatus status, Exception exception, T result)
            : base(exception.Message, exception)
        {
            this.Result = new FunctionResult<T>(status, exception, result);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="result">功能函数返回结果</param>
        public FunctionResultException(FunctionResult<T> result)
        {
            this.Result = result;
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        public FunctionResult<T> Result { get; private set; }
    }

    /// <summary>
    /// 功能、处理过程等的结果状态
    /// 功能函数在执行过程当中，出现的各种异常情况
    /// </summary>
    public enum FunctionResultStatus
    {
        /// <summary>
        /// 无，无异常情况，程序可以继续往下执行
        /// </summary>
        None = 0,

        /// <summary>
        /// 消息，有异常情况，需要提示用户，提示之后，程序可以继续往下执行
        /// 例如：保存成功后，提示用户保存已成功完成，适用此项。
        /// </summary>
        Info = 1,

        /// <summary>
        /// 警告，有异常情况，需要警告用户，并阻止程序继续往下执行
        /// 例如：有些必填项用户未填写，适用此项。
        /// </summary>
        Warn = 2,

        /// <summary>
        /// 错误，有异常情况，需要错误用户，并阻止程序继续往下执行
        /// 例如：用户无权限，立即跳转到错误消息页面，并阻止程序执行。
        /// </summary>
        Error = 3,
    }

    /// <summary>
    /// 简明版的功能、处理过程等的返回结果
    /// </summary>
    /// 特别，因为Web函数返回时，Exception不能序列化，故不能用FunctionResult作返回值
    public class SimpleResult
    {
        public bool ResultCode { get; set; }
        public string ResultMessage { get; set; }

        public SimpleResult()
        {
        }

        public SimpleResult(bool resultCode, string msg)
        {
            this.ResultCode = resultCode;
            this.ResultMessage = msg;
        }
    }

    /// <summary>
    /// 简明版的功能、处理过程等的返回结果
    /// </summary>
    /// 特别，因为Web函数返回时，Exception不能序列化，故不能用FunctionResult作返回值
    public class SimpleResult<T>
    {
        /// <summary>
        /// 结果状态
        /// </summary>
        public bool ResultCode { get; set; }

        /// <summary>
        /// 结果信息
        /// </summary>
        public string ResultMessage { get; set; }

        /// <summary>
        /// 结果对象
        /// </summary>
        public T ResultObject { get; set; }
    }
}
