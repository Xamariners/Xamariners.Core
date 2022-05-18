using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLocator;
using PCLStorage;
using FileAccess = PCLStorage.FileAccess;


namespace Xamariners.Core.Common.Helpers
{
    using System.Globalization;

    public static class FileHelper
    {
        public static void CreateDirectoryRecursive(string dirPath)
        {
            if (DirectoryExists(dirPath) != null)
                return;

            var fileSystem = ServiceLocator.Current.GetInstance<IFileSystem>();

            string[] pathParts = dirPath.Split('\\').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            
            if(pathParts.Length == 1 && pathParts[0]== dirPath)
                pathParts = dirPath.Split('/').Where(x => !string.IsNullOrEmpty(x)).ToArray();

            for (var i = 0; i < pathParts.Length; i++)
            {
                if (i > 0)
                    pathParts[i] = Path.Combine(pathParts[i - 1], pathParts[i]);

                var pathPart = (fileSystem.LocalStorage.GetFolderAsync(pathParts[i])).Result;

                if (pathPart != null)
                    fileSystem.LocalStorage.CreateFolderAsync(pathParts[i], CreationCollisionOption.OpenIfExists);
            }
        }

        public static string CreateFile(string filePath, Stream data)
        {
            IFile file = null;
            var fileSystem = ServiceLocator.Current.GetInstance<IFileSystem>();
            var dirPath = Path.GetDirectoryName(filePath);
            var fileName = Path.GetFileName(filePath);

            try
            {
                file = FileExists(fileName);

                if (file != null)
                    return filePath;

                var dir = fileSystem.LocalStorage.GetFolderAsync(dirPath).Result;

                file = dir.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting).Result;

                if (string.IsNullOrEmpty(file?.Path)) throw new Exception($"Error creating file {filePath}");

                using (var fs = file.OpenAsync(FileAccess.ReadAndWrite).Result)
                {
                    data.Seek(0, SeekOrigin.Begin);
                    data.CopyTo(fs);
                }

                return file != null ? filePath : null;
            }
            catch (Exception ex)
            {
                TraceHelpers.WriteToTrace(ex);
                return null;
            }
            finally
            {
                file = null;
            }
        }

        public static string CreateFileAndDirectory(string filePath, Stream data)
        {
            var dirPath = Path.GetDirectoryName(filePath);
            CreateDirectoryRecursive(dirPath);

            var file = CreateFile(filePath, data);

            if(FileExists(filePath) == null)
                TraceHelpers.WriteToTrace($"CreateFileAndDirectory not created: {file}");

            return file;


        }

        public static async Task<Stream> GetFile(string filePath)
        {
            var file = FileExists(filePath);

            if (file == null)
            {
                TraceHelpers.WriteToTrace($"GetFile: Cannot find {filePath}");
                return null;
            }
            
            try
            {
                return await file.OpenAsync(FileAccess.Read);
            }
            catch(Exception ex)
            {
                TraceHelpers.WriteToTrace($"GetFile error fetching {file.Path} : {ex.Message}");
                return null;
                // ignored
            }
        }

        public static async Task<IEnumerable<IFile>> SortFileBySize(this IEnumerable<IFile> files)
        {
            var sortedFiles = new Dictionary<IFile, long>();

            foreach (var file in files)
            {
                using (var s = await GetFile(file.Path))
                    sortedFiles.Add(file, s.Length);
            }

            sortedFiles.ToList().Sort((f, s) => f.Value.CompareTo(s.Value));

            return sortedFiles.Keys;
        }

        public static IFile FileExists(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;

            var dirPath = Path.GetDirectoryName(filePath);
            var fileName = Path.GetFileName(filePath);

            var dir = DirectoryExists(dirPath);

            var file = dir?.GetFileAsync(fileName).Result;

            return file;
        }

        public static IFolder DirectoryExists(string dirPath)
        {
            var fileSystem = ServiceLocator.Current.GetInstance<IFileSystem>();
    
            var dir = fileSystem.LocalStorage.GetFolderAsync(dirPath).Result;

            return dir;
        }
    }
}