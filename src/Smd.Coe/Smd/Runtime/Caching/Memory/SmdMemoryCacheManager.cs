using Smd.Dependency;
using Smd.Runtime.Caching.Configuration;
using Castle.Core.Logging;

namespace Smd.Runtime.Caching.Memory
{
    /// <summary>
    /// Implements <see cref="ICacheManager"/> to work with MemoryCache.
    /// </summary>
    public class SmdMemoryCacheManager : CacheManagerBase
    {
        public ILogger Logger { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SmdMemoryCacheManager(  ICachingConfiguration configuration)
            : base( configuration)
        {

        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return new SmdMemoryCache(name)
            {
                Logger = Logger
            };
        }

        protected override void DisposeCaches()
        {
            foreach (var cache in Caches.Values)
            {
                cache.Dispose();
            }
        }
    }
}
