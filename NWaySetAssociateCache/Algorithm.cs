using System;
using System.Collections.Generic;
using System.Text;

namespace NWaySetAssociateCache
{
    public abstract class Algorithm<KeyType, ValueType>
    {
        protected LinkedList<KeyValuePair<KeyType, ValueType>> CacheList { get; private set; }
        /// <summary>
        /// Constructor for Algorithm of clearing cache.
        /// </summary>
        public Algorithm()
        {
            CacheList = new LinkedList<KeyValuePair<KeyType, ValueType>>();
        }

        /// <summary>
        /// Adds key/value pair to cache list.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public abstract void Add(KeyType key, ValueType value, Action<KeyType, ValueType> addAlgorithm = null);

        /// <summary>
        /// Updates the key/value pair position in cache list.
        /// </summary>
        /// <param name="key"></param>
        public abstract void Update(KeyType key, Action<KeyType, ValueType> updateAlgorithm = null);

        /// <summary>
        /// Removes key/value pair from cache list.
        /// </summary>
        /// <param name="removeAlgorithm"></param>
        public abstract void Remove(Action<KeyType, ValueType> removeAlgorithm = null);

        /// <summary>
        /// Gives key to remove.
        /// </summary>
        /// <returns></returns>
        public abstract KeyType GetKeyToRemove(Action<KeyType, ValueType> getKeyToRemoveAlgorithm = null);
    }
}
