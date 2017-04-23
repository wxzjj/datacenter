using System;
using System.Collections.Specialized;
using Bigdesk2010.Data;

namespace Bigdesk2010.MobileServiceChannel
{
    /// <summary>
    /// 简化版移动服务接口
    /// </summary>
    /// Lite：adj. 简化的
    public interface ILiteMobileService
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="receiveNumbers">收信号码组</param>
        /// <param name="content">短信内容</param>
        /// <returns>短信发送结果</returns>
        string SendSms(StringCollection receiveNumbers, string content);
    }

    /// <summary>
    /// 创建精简移动服务通道的工厂
    /// </summary>
    public static class LMSChannelFactory
    {
        /// <summary>
        /// 创建精简移动服务通道
        /// </summary>
        /// <param name="channelName">通道名称</param>
        /// <returns></returns>
        public static ILiteMobileService CreateLMSChannel(string channelName)
        {
            switch (channelName)
            {
                case "JassonMAS":
                    return new JassonMASChannel();
            }
            return null;
        }
    }
}
