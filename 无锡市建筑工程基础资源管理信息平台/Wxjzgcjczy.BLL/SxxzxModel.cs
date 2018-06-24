using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Wxjzgcjczy.BLL.model
{
    public class SxxzxModel
    {

    }

    public class Zzxx
    {
        public string zzlbId;
        public string zzlb;
        public string zzdjId;
        public string zzdj;
        public string zzxl;
        public string zzxlId;


        public Zzxx(string zzlbId, string zzlb, string zzdj, string zzdjId, string zzxl, string zzxlId)
        {
            this.zzlbId = zzlbId;
            this.zzlb = zzlb;
            this.zzdjId = zzdjId;
            this.zzdj = zzdj;
            this.zzxlId = zzxlId;
            this.zzxl = zzxl;

        }

    }

    [Serializable]
    public class ReturnInfo
    {

        public bool Status { get; set; }
        public string Description { get; set; }
        public bool LastPacket { get; set; }

    }

    public class CorpBasicInfoBody
    {
        [XmlArray("CorpBasicInfoArray")]
        [XmlArrayItem("CorpBasicInfo")]
        public List<CorpBasicInfo> array = new List<CorpBasicInfo>();
    }

    [Serializable]
    public class CorpBasicInfo
    {
        public string CorpCode { get; set; }
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
        public string FoundDate { get; set; }
        public string Fax { get; set; }
        public string LinkPhone { get; set; }
        public string UpdateDate { get; set; }

    }

    public class CorpChiefPersonBody
    {
        [XmlArray("CorpChiefPersonArray")]
        [XmlArrayItem("CorpChiefPerson")]
        public List<CorpChiefPerson> array = new List<CorpChiefPerson>();

    }

    /// <summary>
    /// 企业负责人
    /// </summary>
    [Serializable]
    public class CorpChiefPerson
    {
        public string CorpCode { get; set; }
        public string PersonType { get; set; }
        public string PersonName { get; set; }
        public string IDCardType { get; set; }
        public string IDCardNo { get; set; }
        public string Sex { get; set; }
        public string Birthday { get; set; }
        public string Nation { get; set; }
        public string Education { get; set; }
        public string DutyDesc { get; set; }
        public string ProTitle { get; set; }
        public string Mobile { get; set; }
        public string UpdateDate { get; set; }

    }

    public class CorpCertInfoBody
    {
        [XmlArray("CorpCertInfoArray")]
        [XmlArrayItem("CorpCertInfo")]
        public List<CorpCertInfo> array = new List<CorpCertInfo>();

    }

    /// <summary>
    /// 企业资质证书信息（TCorpCertInfo）
    /// </summary>
    [Serializable]
    public class CorpCertInfo
    {
        public string CorpCode { get; set; }
        public string CertCode { get; set; }
        public string CertType { get; set; }
        public string OrganID { get; set; }
        public string CertMaxLevel { get; set; }
        public string IssueOrgan { get; set; }
        public string IssueLevel { get; set; }
        public string IssueDate { get; set; }
        public string ValidDate { get; set; }
        public string PrintNo { get; set; }
        public string Status { get; set; }
        public string UpdateDate { get; set; }
    }

    public class CorpCertQualBody
    {
        [XmlArray("CorpCertQualArray")]
        [XmlArrayItem("CorpCertQual")]
        public List<CorpCertQual> array = new List<CorpCertQual>();

    }

    /// <summary>
    /// 企业资质(TCorpCertQual)
    /// </summary>
    [Serializable]
    public class CorpCertQual
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

    //人员信息

    public class PersonBasicInfoBody
    {
        [XmlArray("PersonBasicInfoArray")]
        [XmlArrayItem("PersonBasicInfo")]
        public List<PersonBasicInfo> array = new List<PersonBasicInfo>();
    }

    [Serializable]
    public class PersonBasicInfo
    {
        public string PersonName { get; set; }
        public string IDCardType { get; set; }
        public string IDCardNo { get; set; }
        public string Sex { get; set; }
        public string Birthday { get; set; }
        public string Nationality { get; set; }
        public string Education { get; set; }
        public string DutyDesc { get; set; }
        public string ProTitle { get; set; }
        public string UpdateDate { get; set; }



    }


    public class PersonRegCertBody
    {
        [XmlArray("PersonRegCertArray")]
        [XmlArrayItem("PersonRegCert")]
        public List<PersonRegCert> array = new List<PersonRegCert>();
    }
    /// <summary>
    /// 人员注册证书
    /// </summary>
    [Serializable]
    public class PersonRegCert
    {
        public string IDCardType { get; set; }
        public string IDCardNo { get; set; }
        public string PersonName { get; set; }
        public string RegType { get; set; }
        public string RegNo { get; set; }
        public string IssueDate { get; set; }
        public string IssueOrgan { get; set; }
        public string StampNo { get; set; }
        public string ValidDate { get; set; }
        public string QualCertNo { get; set; }
        public string QualIssueDate { get; set; }
        public string CorpCode { get; set; }
        public string CorpName { get; set; }
        public string Status { get; set; }
        public string UpdateDate { get; set; }
        public string PhotoBase64 { get; set; }

    }

    public class PersonRegMajorBody
    {
        [XmlArray("PersonRegMajorArray")]
        [XmlArrayItem("PersonRegMajor")]
        public List<PersonRegMajor> array = new List<PersonRegMajor>();
    }

    /// <summary>
    /// 人员注册专业
    /// </summary>
    [Serializable]
    public class PersonRegMajor
    {
        public string IDCardType { get; set; }
        public string IDCardNo { get; set; }
        public string PersonName { get; set; }
        public string RegType { get; set; }
        public string RegMajorCode { get; set; }
        public string RegMajorDesc { get; set; }
        public string RegIssueDate { get; set; }
        public string RegValidDate { get; set; }
        public string IsMaster { get; set; }
        public string QualCertNo { get; set; }
        public string QualIssueDate { get; set; }
        public string UpdateDate { get; set; }
    }



    public class PersonJobCertBody
    {
        [XmlArray("PersonJobCertArray")]
        [XmlArrayItem("PersonJobCertInfo")]
        public List<PersonJobCertInfo> array = new List<PersonJobCertInfo>();
    }

    /// <summary>
    /// 人员岗位（技能）证书
    /// </summary>
    [Serializable]
    public class PersonJobCertInfo
    {
        public string IDCardType { get; set; }
        public string IDCardNo { get; set; }
        public string PersonName { get; set; }
        public string JobCertType { get; set; }
        public string JobCertLevel { get; set; }
        public string CertMajor { get; set; }
        public string CertNo { get; set; }
        public string IssueDate { get; set; }
        public string IssueOrgan { get; set; }
        public string ValidDate { get; set; }
        public string CorpCode { get; set; }
        public string CorpName { get; set; }
        public string Status { get; set; }
        public string UpdateDate { get; set; }
    }
}
