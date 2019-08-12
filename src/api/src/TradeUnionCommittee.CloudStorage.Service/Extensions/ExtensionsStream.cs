using System.IO;

namespace TradeUnionCommittee.CloudStorage.Service.Extensions
{
    public static class ExtensionsStream
    {
        public static byte[] ReadAsBytes(this Stream stream)
        {
            var buffer = new byte[16384];
            using (var memoryStream = new MemoryStream())
            {
                int count;
                while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                    memoryStream.Write(buffer, 0, count);
                return memoryStream.ToArray();
            }
        }
    }
}