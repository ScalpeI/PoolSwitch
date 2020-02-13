﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.Model.Hash
{
    public class hash_hmac
    {
        private Encoding encoding = Encoding.UTF8;

        public string sha1(string mess, string key)
        {
            var keyByte = encoding.GetBytes(key);
            using (var hmacsha1 = new HMACSHA1(keyByte))
            {
                hmacsha1.ComputeHash(encoding.GetBytes(mess));
                return ByteToString(hmacsha1.Hash).ToLower();
            }
        }
        static string ByteToString(byte[] buff)
        {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++)
                sbinary += buff[i].ToString("X2"); /* hex format */
            return sbinary;
        }

    }
}
