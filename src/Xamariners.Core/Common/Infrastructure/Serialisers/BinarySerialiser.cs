using System;
using System.IO;
using System.Reflection;
using CommonServiceLocator;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using PCLStorage;
using Xamariners.Core.Common.Helpers;
using Xamariners.Core.Common.Infrastructure.Serialisers;
using FileAccess = PCLStorage.FileAccess;

namespace Xamariners.Core.Common
{
	public static class BinarySerialiser
	{
		private const string CRYPTO_KEY = "9@&[fB3q6zw9Zi(7n$8T9HuZ?n]E9?$4XbnWoT7[ZzqWb";
		private static readonly object _lock = new object();

		public static void SerialiseObject<T>(string filePath, T objectToSerialize)
		{
			SerialiseObject<T>(filePath, objectToSerialize, null);
		}

		public static void SerialiseObject<T>(string filePath, T objectToSerialize, byte[] salt)
		{
			var fileSystem = ServiceLocator.Current.GetInstance<IFileSystem>();
			string folderName = Path.GetDirectoryName(filePath);
			
			lock (_lock)
			{ 
				FileHelper.CreateDirectoryRecursive(folderName);

				using (var stream = fileSystem.LocalStorage.CreateFileAsync(filePath, CreationCollisionOption.OpenIfExists).Result.OpenAsync(FileAccess.ReadAndWrite).Result)
				{
					stream.Position = 0;
					using (var writer = new BsonWriter(stream))
					{
						writer.CloseOutput = true;
						var serialiser = new JsonSerializer
						{
							ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
							TypeNameHandling = TypeNameHandling.All,
							MaxDepth = 25,
							Converters = { new MemoryStreamJsonConverter() }
						};
					   
						serialiser.Serialize(writer, objectToSerialize, typeof(T));
						stream.Flush();
					}
				}
			}
		}

		public static byte[] SerialiseObject<T>(T objectToSerialize)
		{
			using (var stream = new MemoryStream())
			{
				stream.Position = 0;
		 
				using (var writer = new BsonWriter(stream))
				{
					writer.CloseOutput = true;
					var serialiser = new JsonSerializer
					{
						ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
						MissingMemberHandling = MissingMemberHandling.Ignore,
						MaxDepth = 25,
						Converters = { new MemoryStreamJsonConverter() }
					};

					serialiser.Serialize(writer, objectToSerialize, typeof(T));
					var result = stream.ToArray();
					stream.Flush();
					return result;
				}
			}
		}

		public static T DeSerializeObject<T>(string filePath, byte[] salt = null) where T : class
		{
			var fileSystem = ServiceLocator.Current.GetInstance<IFileSystem>();

			string folderName = Path.GetDirectoryName(filePath);
			var folder = fileSystem.LocalStorage.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists).Result;
            
			using (var stream = folder.GetFileAsync(filePath).Result.OpenAsync(FileAccess.Read).Result)
			{
				if (stream.Length == 0)
				   return default(T);

				stream.Position = 0;
	   
				//if (salt != null)
				//    bytes = CryptoHelpers.DecryptAes(bytes, CRYPTO_KEY, salt);

				var serialiser = new JsonSerializer
				{
					TypeNameHandling = TypeNameHandling.All,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Converters = { new MemoryStreamJsonConverter() }
                };

				using (var reader = new BsonReader(stream, typeof(T).IsEnumerable(), DateTimeKind.Utc))
				{
					var result = serialiser.Deserialize<T>(reader);

					if (result == null)
						throw new Exception($"Error while DeSerializeObject for type {typeof(T).Name}");

					return (T)result;
				}
			}
        }
        
		public static T DeSerializeObject<T>(byte[] bytes)
		{
			return (T) DeSerializeObject(bytes, typeof(T));
		}

		public static object DeSerializeObject(byte[] bytes, Type type)
		{
			if (bytes == null || bytes.Length == 0)
				return type.GetTypeInfo().IsValueType ? Activator.CreateInstance(type) : null;
            
		    using (var stream = new MemoryStream(bytes))
		    {
		        stream.Position = 0;

		        var serialiser = new JsonSerializer
		        {
		            TypeNameHandling = TypeNameHandling.All,
                    Converters = { new MemoryStreamJsonConverter() }

                };

		        using (var reader = new BsonReader(stream, type.IsEnumerable(), DateTimeKind.Utc))
		        {

		            var result = serialiser.Deserialize(reader, type);

		            if (result == null)
		                throw new Exception(
		                    $"Error while DeSerializeObject for type {type.Name} with byte length {bytes.Length}");

		            return result;
		        }
		    }
		}
	}
}