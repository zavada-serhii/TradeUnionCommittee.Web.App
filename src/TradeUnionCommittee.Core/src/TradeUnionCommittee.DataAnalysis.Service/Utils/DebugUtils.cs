using System;
using System.IO;
using ServiceStack.Text;

namespace TradeUnionCommittee.DataAnalysis.Service.Utils
{
    public static class DebugUtils
    {
        public static string Serialize(this object data, FileType extension)
        {
            switch (extension)
            {
                case FileType.Json:
                    return JsonSerializer.SerializeToString(data);
                case FileType.Xml:
                    return XmlSerializer.SerializeToString(data);
                case FileType.Csv:
                    return CsvSerializer.SerializeToString(data);
                default:
                    throw new ArgumentOutOfRangeException(nameof(extension), extension, null);
            }
        }

        public static void SerializeAndSaveToFile(this object data, string pathToSave, FileType extension)
        {
            string fileName = Guid.NewGuid().ToString();
            string fileExtension = GetFileExtension(extension);
            string path = Path.Combine(pathToSave, $"{fileName}{fileExtension}");
            string serializedData = data.Serialize(extension);

            using FileStream fstream = new FileStream(path, FileMode.OpenOrCreate);
            byte[] array = System.Text.Encoding.Default.GetBytes(serializedData);
            fstream.Write(array, 0, array.Length);
        }

        //-----------------------------------------------------------------------------------------------

        private static string GetFileExtension(FileType extension)
        {
            switch (extension)
            {
                case FileType.Json:
                    return ".json";
                case FileType.Xml:
                    return ".xml";
                case FileType.Csv:
                    return ".csv";
                default:
                    throw new ArgumentOutOfRangeException(nameof(extension), extension, null);
            }
        }

        public enum FileType
        {
            Json,
            Xml,
            Csv
        }
    }
}