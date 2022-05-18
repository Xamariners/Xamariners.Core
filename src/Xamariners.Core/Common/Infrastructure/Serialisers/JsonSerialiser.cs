using System;
using System.IO;
using CommonServiceLocator;
using Newtonsoft.Json;
using PCLStorage;
using Xamariners.Core.Common.Helpers;
using FileAccess = PCLStorage.FileAccess;

namespace Xamariners.Core.Common
{
    public static class JsonSerialiser
    {
        public static string Serialise<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        public static T Deserialise<T>(string data)
        {
            if (data == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SerialiseObject<T>(string filePath, T objectToSerialize)
        {
            var fileSystem = ServiceLocator.Current.GetInstance<IFileSystem>();
            string folderName = Path.GetDirectoryName(filePath);

            fileSystem.LocalStorage.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists).Wait();

            var file = fileSystem.LocalStorage.CreateFileAsync(filePath, CreationCollisionOption.OpenIfExists).Result;
            
            using (var stream = file.OpenAsync(FileAccess.ReadAndWrite).Result)
            {
                var json = Serialise(objectToSerialize);
                var bytes = json.GetBytes();
                stream.Write(bytes, 0, bytes.Length);
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
                byte[] bytes = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(bytes, 0, (int)stream.Length);

                if (bytes.Length == 0)
                    return default(T);

                var json = bytes.GetString();
                var result = Deserialise<T>(json);

                if (result == null)
                    throw new Exception($"Error while DeSerializeObject for type {typeof(T).Name} with byte length {bytes.Length}");

                return (T)result;
            }
        }
    }
}
