// -----------------------------------------------------------------------
// <copyright file="IEncryptionService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Encryption;

public interface IEncryptionService
{
    string Encrypt(string? text);

    string Decrypt(string? text);
}
