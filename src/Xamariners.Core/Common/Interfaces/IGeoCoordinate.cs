using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.Core.Common.Interfaces
{
    public interface IGeoCoordinate
    {
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}