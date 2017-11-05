using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WxjzgcjczyTimerService.model
{
    [Serializable]
    public class OutChiefPersonBody
    {
        [XmlArray("OutChiefPersonArray")]
        [XmlArrayItem("OutChiefPerson")]
        public List<OutChiefPerson> array = new List<OutChiefPerson>();
    }
    [Serializable]
    public class OutChiefPerson
    {
        public string CorpCode { get; set; }
        public string OrganID { get; set; }
        public string PersonType { get; set; }
        public string IDCardNo { get; set; }
        public string PersonName { get; set; }
        public string DutyDesc { get; set; }
        public string Mobile { get; set; }

    }


    [Serializable]
    public class OutConsInfoBody
    {
        [XmlArray("OutConsInfoArray")]
        [XmlArrayItem("OutConsInfo")]
        public List<OutConsInfo> array = new List<OutConsInfo>();
    }

    [Serializable]
    public class OutConsInfo
    {
        public string CorpCode { get; set; }
        public string OrganID { get; set; }
        public string IDCardNo { get; set; }
        public string PersonName { get; set; }
        public string RegNo { get; set; }
        public string RegType { get; set; }
        public string RegMajor { get; set; }
        public string IssueDate { get; set; }
        public string ValidDate { get; set; }
        public string SBCertCode { get; set; }
        public string SBValidDate { get; set; }
        public string Status { get; set; }
    }
}
