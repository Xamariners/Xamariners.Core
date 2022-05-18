using System;
using System.IO;
using System.Runtime.Serialization;
using CommonServiceLocator;
using PCLStorage;
using FileAccess = PCLStorage.FileAccess;

namespace Xamariners.Core.Common
{
    public static class DataContractSerialiser
    {
        public static void SerialiseObject<T>(string filePath, T objectToSerialize)
        {
            var fileSystem = ServiceLocator.Current.GetInstance<IFileSystem>();
            string folderName = Path.GetDirectoryName(filePath);

            fileSystem.LocalStorage.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists).Wait();

            var file = fileSystem.LocalStorage.CreateFileAsync(filePath, CreationCollisionOption.OpenIfExists).Result;
            using (var stream = file.OpenAsync(FileAccess.ReadAndWrite).Result)
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(stream, objectToSerialize);
            }
        }

        public static T DeSerializeObject<T>(string filePath) where T : class
        {
            var fileSystem = ServiceLocator.Current.GetInstance<IFileSystem>();

            string folderName = Path.GetDirectoryName(filePath);
            var folder = fileSystem.LocalStorage.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists).Result;
            var file = folder.GetFileAsync(filePath).Result;

            using (var stream = file.OpenAsync(FileAccess.Read).Result)
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                byte[] bytes = new byte[stream.Length];
                stream.Position = 0;
                var result = serializer.ReadObject(stream);

                return (T)result;
            }
        }

        public static T DeSerializeObject<T>(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return default(T);

            using (var stream = new MemoryStream(bytes))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                var result = serializer.ReadObject(stream);

                return (T)result;
            }
        }

        public static object DeSerializeObject(byte[] bytes, Type type)
        {
            if (bytes == null || bytes.Length == 0)
                return null;
         
            using (var stream = new MemoryStream(bytes))
            {
                DataContractSerializer serializer = new DataContractSerializer(type);
                var result =  serializer.ReadObject(stream);

                return result;
            }
        }

        public static byte[] SerialiseObject<T>(T objectToSerialize)
        {
            using (var stream = new MemoryStream())
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(stream, objectToSerialize);

                var result = stream.ToArray();

                return result;
            }
        }

    }
}