using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.Core.Common.Interfaces
{
    public interface IGeoLocation : IGeoCoordinate
    {
        double? Distance { get; set; }
    }
}
