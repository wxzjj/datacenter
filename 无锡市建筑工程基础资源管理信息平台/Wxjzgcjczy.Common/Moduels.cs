using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8;

namespace Wxjzgcjczy.Common
{
    /// <summary>
    /// 公共分组数据字段
    /// </summary> 
    public class GgfzInfoModel
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupDesc { get; set; }
        public string GroupType { get; set; }

        public string GroupIDS { get; set; }
        public string GroupNameS { get; set; }

        public int SjmlID { get; set; }
        public string SjmlName { get; set; }
        public string SjmlTel { get; set; }
        public string SjmlEmail { get; set; }
        public string SjmlMobile { get; set; }

        //查询条件
        public string Searchertion { get; set; }
    }
    public enum ProcessResult
    {
        用户名或密码错误 = 1,
        数据表名不正确 = 2,
        保存失败和失败原因 = 3,
        未找到对应项目 = 5,
        数据保存成功 =0,
        内部错误 = 6,
        接口关闭 = 7

    }

    public struct ProcessResultData
    {
        public ProcessResult code;
        public string message;
        public string ResultMessage
        {
            get {
                string messagePlus = this.code.ToString();
                if (!string.IsNullOrEmpty(this.message))
                {
                    messagePlus = messagePlus + ", " + this.message;
                }
                return string.Format("{0} {1}",this.code.ToInt32().ToString().PadLeft(3, '0'),messagePlus);
            }
        }
    }
}
