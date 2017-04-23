using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WxjzgcjczyTimerService.model
{
    [Serializable]
    public class OutCorpBasicInfoBody
    {
        [XmlArray("OutCorpBasicInfoArray")]
        [XmlArrayItem("OutCorpBasicInfo")]
        public List<OutCorpBasicInfo> array = new List<OutCorpBasicInfo>();
    }
    public class OutCorpBasicInfo
    {
        public string CorpCode { get; set; }
        public string OrganID { get; set; }
        public string CorpName { get; set; }
        public string LicenseNo { get; set; }
        public string IsForeign { get; set; }
        public string FLicenseNo { get; set; }
        public string ProvinceCode { get; set; }
        public string CityCode { get; set; }
        public string CountyCode { get; set; }
        public string RegAddress { get; set; }
        public string CorpAddress { get; set; }
        public string PostCode { get; set; }
        public string LegalMan { get; set; }
        public string LegalManID { get; set; }
        public string LegalManDuty { get; set; }
        public string LegalManProTitle { get; set; }
        public string CorpType { get; set; }
        public string CorpTypeDesc { get; set; }
        public string RegCapital { get; set; }
        public string FactCapital { get; set; }
        public string RegCapitalUnit { get; set; }
        public string FoundDate { get; set; }
        public string Fax { get; set; }
        public string LinkPhone { get; set; }
        public string UpdateDate { get; set; }

    }

    public class OutCorpCertQualBody
    {
        [XmlArray("OutCorpCertQualArray")]
        [XmlArrayItem("OutCorpCertQual")]
        public List<OutCorpCertQual> array = new List<OutCorpCertQual>();

    }

    /// <summary>
    /// 企业资质(TCorpCertQual)
    /// </summary>
    [Serializable]
    public class OutCorpCertQual
    {
        public string CorpCode { get; set; }
        public string CertCode { get; set; }
        public string CertType { get; set; }
        public string TradeType { get; set; }
        public string MajorType { get; set; }
        public string TitleLevel { get; set; }
        public string TitleDesc { get; set; }
        public string IsMaster { get; set; }
        public string ApproveDate { get; set; }
        public string ScopeDesc { get; set; }
        public string ApproveOrgan { get; set; }
        public string ApproveLevel { get; set; }
        public string CancelDate { get; set; }
        public string Status { get; set; }
        public string UpdateDate { get; set; }



    }


}

