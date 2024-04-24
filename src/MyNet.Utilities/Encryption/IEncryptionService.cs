// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Encryption
{
    public interface IEncryptionService
    {
        string Encrypt(string? text);

        string Decrypt(string? text);
    }
}
