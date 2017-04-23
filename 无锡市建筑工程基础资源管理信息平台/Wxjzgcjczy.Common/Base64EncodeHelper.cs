using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wxjzgcjczy.Common
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
           
        public  string Base64Encode(string toEncodingStr)
        {
            if (string.IsNullOrEmpty(toEncodingStr))
                return String.Empty;
            byte[] bytes = encode.GetBytes(toEncodingStr);
            return Convert.ToBase64String(bytes);
        }

        public  string Base64Decode(string decodedStr)
        {
            if (string.IsNullOrEmpty(decodedStr))
                return String.Empty;
            byte[] bytes = Convert.FromBase64String(decodedStr);
            return encode.GetString(bytes);
        }
    }
}
