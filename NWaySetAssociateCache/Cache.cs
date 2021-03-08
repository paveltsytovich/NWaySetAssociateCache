using System;
using System.Collections.Generic;
using System.Linq;

namespace NWaySetAssociateCache
{
    public class Cache<KeyType, ValueType>
    {
        /// <summary>
        /// Store datablocks of cache.
        /// </summary>
        private Dictionary<KeyType, ValueType>[] _cacheBlocks;

        /// <summary>
        /// Determines an algorithm of cache updating and removing unused key/value pairs.
        /// </summary>
        private Algorithm<KeyType, ValueType> _algorithm;

        /// <summary>
        /// Determines all cache size.
        /// </summary>
        private readonly int _cacheSize;
        public int CacheSize
        {
            get => _cacheSize;
        }

        /// <summary>
        /// Number of ways.
        /// </summary>
        private int _nSet;

        private const int maxSetNumber = 8;

        public int NSet
        {
            get
            {
                return _nSet;
            }
            set
            {
                _nSet = (value <= maxSetNumber && value > 0) ? value : throw new CacheException("N ways number exceeds cache size.");
            }
        }

        /// <summary>
        /// Contsructor for cache memory.
        /// </summary>
        /// <param name="cacheSize">Defines cache size</param>
        /// <param name="nSet">Defines ways' quantity</param>
        /// <param name="algorithm">Defines the cleaning/updating cache algorithm</param>
        public Cache(int cacheSize, int nSet, Algorithm<KeyType, ValueType> algorithm)
        {
            _algorithm = algorithm;
            _cacheSize = cacheSize;
            NSet = nSet;
            _cacheBlocks = new Dictionary<KeyType, ValueType>[NSet];
        }

        /// <summary>
        /// Gets Block Index from 0 to N-1(ways number)
        /// </summary>
        /// <returns>Block Index</returns>
        public int GetDataBlockIndex(KeyType key)
        {
            return Math.Abs(key.GetHashCode() % NSet);
        }

        /// <summary>
        /// Puts key/value pair to cache memory.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void Put(KeyType key, ValueType value)
        {
            var index = GetDataBlockIndex(key);

            if (_cacheBlocks[index] == null)
            {
                _cacheBlocks[index] = new Dictionary<KeyType, ValueType> { };
            }

            try
            {
                if (_cacheBlocks[index].Count == CacheSize / NSet)
                {
                    var keyToRemove = _algorithm.GetKeyToRemove();
                    _cacheBlocks[index].Remove(keyToRemove);

                    _algorithm.Remove();
                }

                _cacheBlocks[index].Add(key, value);

                _algorithm.Add(key, value);
            }
            catch
            {
                throw new CacheException("Cannot add key/value pair to cache.");
            }
        }

        /// <summary>
        /// Gets the Value in cache by key.
        /// </summary>
        /// <typeparam name="T">Parametrs type</typeparam>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public ValueType Get(KeyType key)
        {
            int index = GetDataBlockIndex(key);
            ValueType value;

            if (_cacheBlocks[index] == null || !_cacheBlocks[index].TryGetValue(key, out value))
            {
                throw new CacheException("Cache error: Cannot get value by key.");
            }

            _algorithm.Update(key);

            return value;
        }
    }
}
