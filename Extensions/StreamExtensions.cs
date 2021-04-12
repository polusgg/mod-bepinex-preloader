using System;
using System.IO;
using System.Security.Cryptography;

namespace ModClientPreloader.Extensions
{
    public static class StreamExtensions
    {
        public static string MD5Hash(this Stream stream)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "");
        }

        public static byte[] ReadByteArray(this Stream stream)
        {
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}