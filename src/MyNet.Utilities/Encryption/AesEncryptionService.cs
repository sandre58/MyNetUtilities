// -----------------------------------------------------------------------
// <copyright file="AesEncryptionService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Security.Cryptography;
using System.Text;

namespace MyNet.Utilities.Encryption;

public class AesEncryptionService(byte[] key) : IEncryptionService
{
    private const int KeyBytes = 16;
    private const int NonceBytes = 12;

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

    public byte[] Encrypt(byte[] toEncrypt)
    {
        var tag = new byte[KeyBytes];
        var nonce = new byte[NonceBytes];
        var cipherText = new byte[toEncrypt.Length];

        using var cipher = new AesGcm(key, KeyBytes);
        cipher.Encrypt(nonce, toEncrypt, cipherText, tag);

        return Concat(tag, Concat(nonce, cipherText));
    }

    public string Encrypt(string? text) => Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(text ?? string.Empty)));

    public byte[] Decrypt(byte[] cipherText)
    {
        var tag = SubArray(cipherText, 0, KeyBytes);
        var nonce = SubArray(cipherText, KeyBytes, NonceBytes);

        var toDecrypt = SubArray(cipherText, KeyBytes + NonceBytes, cipherText.Length - tag.Length - nonce.Length);
        var decryptedData = new byte[toDecrypt.Length];

        using var cipher = new AesGcm(key, KeyBytes);
        cipher.Decrypt(nonce, toDecrypt, tag, decryptedData);

        return decryptedData;
    }

    public string Decrypt(string? text) => !string.IsNullOrEmpty(text) ? Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(text))).TrimEnd('\0') : string.Empty;
}
