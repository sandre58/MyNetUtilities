// -----------------------------------------------------------------------
// <copyright file="StreamExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class StreamExtensions
{
    private const int DefaultBufferSize = 80 * 1024;

    public static void WriteImage(this Stream stream, byte[] thumbnail)
        => stream.Write(thumbnail.AsSpan(0, thumbnail.Length));

    public static async Task WriteImageAsync(this Stream stream, byte[] thumbnail, CancellationToken? token = null)
        => await stream.WriteAsync(thumbnail.AsMemory(0, thumbnail.Length), token ?? CancellationToken.None).ConfigureAwait(false);

    public static void WriteInXml<T>(this Stream stream, T item)
    {
        var writer = new System.Xml.Serialization.XmlSerializer(typeof(T), typeof(T).GetNestedTypes());
        writer.Serialize(stream, item);
    }

    public static async Task WriteAsXmlAsync<T>(this Stream stream, T item, CancellationToken? token = null)
        => await Task.Run(() => stream.WriteInXml(item), token ?? CancellationToken.None).ConfigureAwait(false);

    public static byte[]? ReadImage(this Stream stream)
    {
        using var ms = new MemoryStream();
        stream.CopyTo(ms, DefaultBufferSize);
        var result = ms.ToArray();
        return result.Length > 0 ? result : null;
    }

    public static async Task<byte[]?> ReadImageAsync(this Stream stream, CancellationToken? token = null)
    {
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms, DefaultBufferSize, token ?? CancellationToken.None).ConfigureAwait(false);
        var result = ms.ToArray();
        return result.Length > 0 ? result : null;
    }

    public static T? ReadInXml<T>(this Stream stream)
    {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T), typeof(T).GetNestedTypes());
        using var reader = XmlReader.Create(stream);
        return (T?)serializer.Deserialize(reader);
    }

    public static async Task<T?> ReadAsXmlAsync<T>(this Stream stream, CancellationToken? token = null)
        => await Task.Run(stream.ReadInXml<T>, token ?? CancellationToken.None).ConfigureAwait(false);
}
