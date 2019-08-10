﻿using MemoryCache.Testing.Common.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace MemoryCache.Testing.Moq {
    /// <summary>
    /// Factory for creating mock/mocked instances.
    /// </summary>
    public class MockFactory {
        private static readonly ILogger Logger = LoggerHelper.CreateLogger(typeof(MockFactory));

        /// <summary>
        /// Creates a memory cache mock.
        /// </summary>
        /// <returns>A memory cache mock.</returns>
        public static Mock<IMemoryCache> CreateMemoryCacheMock() {
            var mock = new Mock<IMemoryCache>();
            
            mock.Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Callback((object key) => {
                    Logger.LogDebug("Cache CreateEntry invoked");
                })
                .Returns((object key) => new CacheEntryFake(key, mock));
            
            return mock;
        }

        /// <summary>
        /// Creates a mocked memory cache.
        /// </summary>
        /// <returns>A mocked memory cache.</returns>
        public static IMemoryCache CreateMockedMemoryCache() {
            return CreateMemoryCacheMock().Object;
        }
    }
}
