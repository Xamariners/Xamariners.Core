using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.Core.Model.Enums;

namespace Xamariners.Core.Interface
{
    public interface IServerState
    {
        ObjectType MemberType { get; set; }
    }
}
