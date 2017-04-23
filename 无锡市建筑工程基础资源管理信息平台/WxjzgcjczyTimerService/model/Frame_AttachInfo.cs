using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WxsjzxTimerService.model
{
    [Serializable]
    public class Frame_AttachInfo
    {
        public string AttachGuid;
        public string CliengGuid;
        public string AttachName;
        public string UploadUserDisplayName;
        public string UploadDateTime;
        public string FileContent;
        public string FileType;
        public string FileInfo;
        public Frame_AttachInfo()
        {
            AttachGuid =String.Empty;
            CliengGuid = String.Empty;
            AttachName = String.Empty;
            UploadUserDisplayName = String.Empty;
            UploadDateTime = String.Empty;
            FileContent = String.Empty;
            FileType = String.Empty;
            FileInfo = String.Empty;

        }

    }
    [Serializable]
    public class Frame_AttachStorage
    {
        public string AttachGuid = String.Empty;
        public string CliengGuid = String.Empty;
        public string AttachName = String.Empty;
        public string UploadUserDisplayName = String.Empty;
        public string UploadDateTime = String.Empty;
        public string FileContent = String.Empty;
        public string FileType = String.Empty;
        public string FileInfo = String.Empty;

    }
}
