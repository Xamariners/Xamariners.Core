using System;
using System.Runtime.Serialization;
using Xamariners.Core.Common.Enum;
using Xamariners.Core.Model.Enums;
using Xamariners.Core.Model.Interface;

namespace Xamariners.Core.Model
{
    using System.IO;

    public interface IMediaFileInfo : ICoreObject
    {
        [DataMember]
        MediaContainer Container { get; set; }

        [DataMember]
        string Extension { get; set; }

        [DataMember]
        string FileName { get; set; }

        [DataMember]
        Stream MediaFileData { get; set; }

        [DataMember]
        int Order { get; set; }

        [DataMember]
        Guid? ParentObjectID { get; set; }

        [DataMember]
        ObjectType ParentObjectType { get; set; }

        [DataMember]
        MediaType MediaType { get; set; }
    }
}