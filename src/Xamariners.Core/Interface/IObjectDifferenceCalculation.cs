using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.Core.Common.Model;

namespace Xamariners.Core.Interface
{
    public interface IObjectDifferenceCalculation
    {
        List<ObjectDifference> CompareWith<TEntity>(TEntity sourceEntity, TEntity comparedEntity, string[] membersToIgnore = null);
    }
}
