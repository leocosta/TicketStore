﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TicketStore.Infra.CrossCutting.Encryption
{
    public class SimplerAES
    {
        private static readonly byte[] KEY = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
        private static readonly byte[] Vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 221, 112, 79, 32, 114, 156 };
        private readonly ICryptoTransform _encryptor;
        private readonly ICryptoTransform _decryptor;
        private readonly UTF8Encoding _encoder;

        public SimplerAES()
        {
            var rm = new RijndaelManaged();
            _encryptor = rm.CreateEncryptor(KEY, Vector);
            _decryptor = rm.CreateDecryptor(KEY, Vector);
            _encoder = new UTF8Encoding();
        }

        public string Encrypt(string unencrypted)
        {
            return Convert.ToBase64String(Encrypt(_encoder.GetBytes(unencrypted)));
        }

        public string Decrypt(string encrypted)
        {
            return _encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
        }

        public byte[] Encrypt(byte[] buffer)
        {
            return Transform(buffer, _encryptor);
        }

        public byte[] Decrypt(byte[] buffer)
        {
            return Transform(buffer, _decryptor);
        }

        protected byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            var stream = new MemoryStream();
            using (var cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }
            return stream.ToArray();
        }
    }
}
