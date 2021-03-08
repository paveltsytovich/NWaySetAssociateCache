using NUnit.Framework;
using NWaySetAssociateCache;
using NSubstitute;

namespace NWaySetAssociateCacheTests
{
    public class CacheTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TryPutGetDataToCacheShouldBeSuccessTest()
        {
            //Arrange
            var algorithm = Substitute.For<Algorithm<int, string>>();
            int cacheSize = 16;
            int nSet = 4;
            int key = 1;
            string expectedValue = "A";

            var cache = new Cache<int, string>(cacheSize, nSet, algorithm);
            cache.Put(key, expectedValue);

            //Actual
            var actualValue = cache.Get(key);

            //Assert
            Assert.AreEqual(expectedValue, actualValue);
            algorithm.Received().Add(key, expectedValue);
            algorithm.Received().Update(key);
        }

        [Test]
        public void TrySetTooMuchNWaysShouldBeErrorTest()
        {
            //Arrange
            var algorithm = Substitute.For<Algorithm<int, int>>();
            int cacheSize = 4;
            int nSet = 20;

            //Actual
            //Assert
            Assert.Throws<CacheException>(() => new Cache<int, int>(cacheSize, nSet, algorithm));
        }

        [Test]
        public void TryGetNotExistedValueShouldBeErrorTest()
        {
            //Arrange
            var algorithm = Substitute.For<Algorithm<int, int>>();
            int cacheSize = 20;
            int nSet = 5;
            int key = 123;

            var cache = new Cache<int, int>(cacheSize, nSet, algorithm);

            //Actual
            //Assert
            Assert.Throws<CacheException>(() => cache.Get(key));
        }
        
        [Test]
        public void TryPutExistedKeyValuePairShouldBeErrorTest()
        {
            //Arrange
            var algorithm = Substitute.For<Algorithm<double, int>>();
            int cacheSize = 25;
            int nSet = 8;
            double key = 123.5;
            int value = 345;

            var cache = new Cache<double, int>(cacheSize, nSet, algorithm);
            cache.Put(key, value);

            //Actual
            //Assert
            Assert.Throws<CacheException>(() => cache.Put(key, value));
        }
    }
}