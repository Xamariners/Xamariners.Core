using System;
using System.Collections.Generic;

namespace Xamariners.Core.Interface
{
    public interface ICacheUpdater
    {
        void StartCacheUpdater(bool updateLocalRepository);

        void StopCacheUpdater();

        void UpdateCache(bool updateLocalRepository, bool forceUpdate = false);

        bool IsRunning { get; }

        IEnumerable<T> GetCachedData<T>(Func<T, bool> filter = null, Func<IEnumerable<T>> fallback = null);

        void RemoveItem<T>(Guid id);

        void StartUpdaters(bool updateLocalRepository);
    }
}