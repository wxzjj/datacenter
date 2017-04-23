using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WxsjzxTimerService.Common
{
    public class Base64EncodeHelper
    {
        private Encoding encode;
        public Base64EncodeHelper()
        {
            encode = Encoding.GetEncoding("GBK");

        }
        public Base64EncodeHelper(Encoding encode)
        {
            this.encode = encode;
        }

        public string Base64Encode(string toEncodingStr)
        {
            if (string.IsNullOrEmpty(toEncodingStr))
                return String.Empty;
            byte[] bytes = encode.GetBytes(toEncodingStr);
            return Convert.ToBase64String(bytes);
        }
        public string Base64Encode(byte[] toEncodingBytes)
        {
            if (toEncodingBytes == null)
                return String.Empty;
            return Convert.ToBase64String(toEncodingBytes);
        }
      

        public string Base64Decode(string decodedStr)
        {
            if (string.IsNullOrEmpty(decodedStr))
                return String.Empty;
            byte[] bytes = Convert.FromBase64String(decodedStr);
            return encode.GetString(bytes);
        }

        public byte[] Base64DecodeToBytes(string decodedStr)
        {
            if (string.IsNullOrEmpty(decodedStr))
                return null;
            byte[] bytes = Convert.FromBase64String(decodedStr);
            return bytes;
        }
        
    }
}
