using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WxsjzxTimerService.model
{
    [Serializable]
    public class DataBody
    {
        [XmlArray("EpointDataBody")]
        [XmlArrayItem("许可项目")]
        public List<Xkxm> xkxms;
        public DataBody()
        {
            xkxms = new List<Xkxm>();
        }
    }

    [Serializable]
    public class Xkxm
    {
        [XmlElement("项目信息")]
        public Jscin_SGXK_ProjectInfo projectInfo;
        [XmlElement("工程信息")]
        public Gcxx gcxx;
        [XmlElement("证明文件")]
        public Zmwj zmwj;

        public Xkxm()
        {
            projectInfo = new Jscin_SGXK_ProjectInfo();
            gcxx = new Gcxx();
            zmwj = new Zmwj();
        }
    }

    [Serializable]
    public class Gcxx
    {
        [XmlElement("ApplyConstInfo")]
        public Jscin_SGXK_ApplyConstInfo applyConstInfo;
        [XmlArray("CanJianDanW")]
        [XmlArrayItem("DataList")]
        public List<Jscin_SGXK_CanJianDanW> canJianDanW;
        [XmlArray("ApplyConstDetailInfo")]
        [XmlArrayItem("DataList")]
        public List<Jscin_SGXK_ApplyConstDetailInfo> applyConstDetailInfo;
        public Gcxx()
        {
            applyConstInfo = new Jscin_SGXK_ApplyConstInfo();
            canJianDanW = new List<Jscin_SGXK_CanJianDanW> ();
            applyConstDetailInfo = new List<Jscin_SGXK_ApplyConstDetailInfo>();

        }

    }

    [Serializable]
    public class SgxkDataBody
    {
        [XmlArray("EpointDataBody")]
        [XmlArrayItem("ApplyConstInfo")]
        public List<Jscin_SGXK_ApplyConstInfo> list_applyConstInfo;
        public SgxkDataBody()
        {
            list_applyConstInfo = new List<Jscin_SGXK_ApplyConstInfo>();
        }
    }
    [Serializable]
    public class XmxxDataBody
    {
        [XmlArray("EpointDataBody")]
        [XmlArrayItem("Jscin_SGXK_ProjectInfo")]
        public List<Jscin_SGXK_ProjectInfo> list_projectInfo;
        public XmxxDataBody()
        {
            list_projectInfo = new List<Jscin_SGXK_ProjectInfo>();
        }
    }

    [Serializable]
    public class Zmwj
    {
        [XmlElement("AttachInfo")]
        public Jscin_Fund_AttachInfo attachInfo;
        [XmlArray("Attach")]
        [XmlArrayItem("DataList")]
        public List<Ap_sgxksqb_clqd> frame_AttachInfo;
        public Zmwj()
        {
            //attachInfo = new Jscin_Fund_AttachInfo();
            //frame_AttachInfo = new List<Frame_AttachInfo>();
        }
    }
}
