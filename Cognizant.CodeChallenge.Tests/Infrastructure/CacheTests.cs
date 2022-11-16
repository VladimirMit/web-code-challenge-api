using System.Threading.Tasks;
using Application.CodeChallenge.Infrastructure.Cache;
using NUnit.Framework;

namespace Application.CodeChallenge.Tests.Infrastructure
{
    internal class CacheTests
    {
        [Test]
        public void Cache_GetElement_WhenNoElement_ShouldReturnNull()
        {
            var cache = new InMemoryCache();

            var element = cache.Get("key");
            
            Assert.IsNull(element);
        }

        [Test]
        public void Cache_AddElement_ShouldAddAndGetElement()
        {
            var cache = new InMemoryCache();
            var expectedElement = new {id = 1, name = "test"};

            cache.Add("key", expectedElement);
            var element = cache.Get("key");
            
            Assert.AreEqual(expectedElement, element);
        }

        [Test]
        public void Cache_AddElement_ShouldUpdateElement()
        {
            var cache = new InMemoryCache();
            var oldElement = new {id = 1, name = "test"};
            cache.Add("key", oldElement);

            var newElement = new {Id = 2, Name = "test2"};
            cache.Add("key", newElement);
            
            var element = cache.Get("key");
            Assert.AreEqual(newElement, element);
        }
        
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public void Cache_AddElement_AboveCustomLimit_ShouldDeleteOldest(int customEntryCount)
        {
            var cache = new InMemoryCache(customEntryCount);

            for (int i = 0; i < customEntryCount; i++)
            {
                cache.Add($"key{i}", new {Id = 1, Name = $"Test{i}"});
            }
            
            cache.Add("Test", new {Id = 999});
            
            Assert.IsNull(cache.Get("key0"));
            Assert.AreEqual(customEntryCount, cache.EntryCount);
        }

        [Test]
        public void Cache_AddElement_FromMultipleThreads_ShouldWorkCorrect()
        {
            var cache = new InMemoryCache(100);

            Parallel.For(0, 1000, new ParallelOptions {MaxDegreeOfParallelism = 8}, (i) =>
            {
                cache.Add((i % 150).ToString(), new {Id = i});
                Assert.LessOrEqual(cache.EntryCount, cache.EntryCountLimit);
            });
        }
    }
}
