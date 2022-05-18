using System;
using System.Diagnostics.CodeAnalysis;

namespace Xamariners.Core.Interface
{
    [Flags]
    [SuppressMessage("Microsoft.Naming", "CA1714:FlagsEnumsShouldHavePluralNames")]
    public enum EntityState
    {
        Detached = 1,
        Unchanged = 2,
        Added = 4,
        Deleted = 8,
        Modified = 16,
    }
}