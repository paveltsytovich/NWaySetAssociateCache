using NUnit.Framework;
using NWaySetAssociateCache;
using System;

namespace NWaySetAssociateCacheTests
{
    public class MRUAlgorithmTests
    {
        [Test]
        public void AddEntryCasheListShouldBeSuccess()
        {
            //Arrange
            int key = 178;
            int expectedValue = 678;
            var algorithm = new MRUAlgorithm<int, int>();

            algorithm.Add(key, expectedValue);

            //Actual
            var actualValue = algorithm.GetValue(key);
            //Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void AddEntryViaCacheShouldBeSuccess()
        {
            //Arrange
            int cacheSize = 16;
            int nSet = 3;
            string key1 = "key1";
            string expectedValue1 = "value1";
            string key2 = "key2";
            string expectedValue2 = "value2";
            string key3 = "key3";
            string expectedValue3 = "value3";

            var algorithm = new MRUAlgorithm<string, string>();

            var cache = new Cache<string, string>(cacheSize, nSet, algorithm);
            cache.Put(key1, expectedValue1);
            cache.Put(key2, expectedValue2);
            cache.Put(key3, expectedValue3);

            //Actual
            var actualValue1 = algorithm.GetValue(key1);
            var actualValue2 = algorithm.GetValue(key2);
            var actualValue3 = algorithm.GetValue(key3);

            //Assert
            Assert.AreEqual(expectedValue1, actualValue1);
            Assert.AreEqual(expectedValue2, actualValue2);
            Assert.AreEqual(expectedValue3, actualValue3);
        }

        [Test]
        public void TryGetFromNullCacheShouldBeErrorTest()
        {
            //Arrange
            string key = "134";

            var algorithm = new MRUAlgorithm<string, string>();

            //Actual
            //Assert
            Assert.Throws<InvalidOperationException>(() => algorithm.GetValue(key));
        }

        [Test]
        public void GetEntryViaCacheShouldBeLast()
        {
            //Arrange
            int cacheSize = 256;
            int nSet = 5;
            string key1 = "1";
            string value1 = "1";
            string key2 = "2";
            string value2 = "2";

            var algorithm = new MRUAlgorithm<string, string>();

            var cache = new Cache<string, string>(cacheSize, nSet, algorithm);

            cache.Put(key1, value1);
            cache.Put(key2, value2);
            cache.Get(key1);

            //Actual
            //Assert
            Assert.IsTrue(algorithm.IsKeyValuePairLast(key1));
        }

        [Test]
        public void RemoveEntryViaAlgorithmShouldBeSuccess()
        {
            //Arrange
            int cacheSize = 255;
            int nSet = 1;
            double key = 6.4;
            double value = 2.2;

            var algorithm = new MRUAlgorithm<double, double>();

            var cache = new Cache<double, double>(cacheSize, nSet, algorithm);
            cache.Put(key, value);

            algorithm.Remove();

            //Actual
            //Assert
            Assert.Throws<InvalidOperationException>(() => algorithm.GetValue(key));
            Assert.Throws<InvalidOperationException>(() => cache.Get(key));
        }

        [Test]
        public void UpdateEntryViaAlgorithmShouldBeLast()
        {
            //Arrange
            int key1 = 1;
            int value1 = 1;
            int key2 = 2;
            int value2 = 2;

            var algorithm = new MRUAlgorithm<int, int>();
            algorithm.Add(key1, value1);
            algorithm.Add(key2, value2);
            algorithm.Update(key1);

            //Actual
            //Assert
            Assert.IsTrue(algorithm.IsKeyValuePairLast(key1));
        }

        [Test]
        public void TryUpdateAlreadyLastEntryTest()
        {
            //Arrange
            int cacheSize = 10;
            int nSet = 5;
            string key = "134";
            string value = "2221";

            var algorithm = new MRUAlgorithm<string, string>();
            var cache = new Cache<string, string>(cacheSize, nSet, algorithm);
            cache.Put(key, value);

            //Actual
            //Assert
            Assert.DoesNotThrow(() => algorithm.Update(key));
        }

        [Test]
        public void TryRemovePairFromAlgorithmShouldBerRemovedFromCacheTest()
        {
            //Arrange
            int cacheSize = 10;
            int nSet = 5;
            int key = 134;
            string value = "2221";

            var algorithm = new MRUAlgorithm<int, string>();
            var cache = new Cache<int, string>(cacheSize, nSet, algorithm);

            cache.Put(key, value);

            algorithm.Remove();

            //Actual
            //Assert
            Assert.Throws<InvalidOperationException>(() => cache.Get(key));
            Assert.Throws<InvalidOperationException>(() => algorithm.GetValue(key));
        }
    }
}
