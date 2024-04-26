// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Security.Cryptography;
using System.Text;

namespace MyNet.Utilities.Encryption
{
    public class AesEncryptionService(byte[] key) : IEncryptionService
    {
        private const int KEY_BYTES = 16;
        private const int NONCE_BYTES = 12;

        private readonly byte[] _key = key;

        public byte[] Encrypt(byte[] toEncrypt)
        {
            var tag = new byte[KEY_BYTES];
            var nonce = new byte[NONCE_BYTES];
            var cipherText = new byte[toEncrypt.Length];

            using var cipher = new AesGcm(_key, KEY_BYTES);
            cipher.Encrypt(nonce, toEncrypt, cipherText, tag);

            return Concat(tag, Concat(nonce, cipherText));
        }

        public string Encrypt(string? text) => Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(text ?? string.Empty)));

        public byte[] Decrypt(byte[] cipherText)
        {
            var tag = SubArray(cipherText, 0, KEY_BYTES);
            var nonce = SubArray(cipherText, KEY_BYTES, NONCE_BYTES);

            var toDecrypt = SubArray(cipherText, KEY_BYTES + NONCE_BYTES, cipherText.Length - tag.Length - nonce.Length);
            var decryptedData = new byte[toDecrypt.Length];

            using var cipher = new AesGcm(_key, KEY_BYTES);
            cipher.Decrypt(nonce, toDecrypt, tag, decryptedData);

            return decryptedData;
        }

        public string Decrypt(string? text) => !string.IsNullOrEmpty(text) ? Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(text))).TrimEnd('\0') : string.Empty;

        public static byte[] Concat(byte[] a, byte[] b)
        {
            var output = new byte[a.Length + b.Length];

            for (var i = 0; i < a.Length; i++)
            {
                output[i] = a[i];
            }

            for (var j = 0; j < b.Length; j++)
            {
                output[a.Length + j] = b[j];
            }

            return output;
        }

        public static byte[] SubArray(byte[] data, int start, int length)
        {
            var result = new byte[length];

            Array.Copy(data, start, result, 0, length);

            return result;
        }
    }
}
