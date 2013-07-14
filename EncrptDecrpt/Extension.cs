﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EncrptDecrpt
{

    /// <summary>
    /// This Class is Extension of byte array for future use to encrypt the plain text with more byte arrays. 
    /// These are extension methods of bytes.
    /// </summary>
    public static class Extension
    {

        #region Static Methods
        
        public static string ToHexString(this byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 2);
            foreach (byte b in data)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        public static string ToBase64String(this byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static string ToUTF8String(this byte[] data)
        {
            return new UTF8Encoding().GetString(data);
        }

        public static byte[] ToByteArray(this string str)
        {
            return ASCIIEncoding.ASCII.GetBytes(str);
        }

        public static byte[] ToByteArrayUTF8(this string str)
        {
            return new UTF8Encoding().GetBytes(str);
        }

        public static byte[] ToByteArrayBase64(this string str)
        {
            return Convert.FromBase64String(str);
        } 

        #endregion

    }
}
