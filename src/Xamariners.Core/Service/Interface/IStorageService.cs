using System.Reflection;
using System.Threading.Tasks;
using Xamariners.Core.Model;

namespace Xamariners.Core.Service.Interface
{
    using System.IO;

    public interface IStorageService
    {
        bool DeleteFS(IMediaFileInfo image);
        Task<Stream> Download(IMediaFileInfo image);
        Task<bool> EmptyStorageContainer();

        Task<string> Upload(IMediaFileInfo image, Stream imageData);
        void Initialise(string parentObjectType = "", bool isPublic = false);
        void InitFakeMediaStore(Assembly asm);
        string GetImagePath(IMediaFileInfo image);
    }
}