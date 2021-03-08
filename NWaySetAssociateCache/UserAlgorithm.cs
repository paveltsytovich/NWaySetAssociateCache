using System;
using System.Collections.Generic;
using System.Text;

namespace NWaySetAssociateCache
{
    public class UserAlgorithm<KeyType, ValueType> : Algorithm<KeyType, ValueType>
    {
        /// <summary>
        /// Constructor for User Algorithm.
        /// </summary>
        public UserAlgorithm() : base() { }

        /// <summary>
        /// Adds key/value pair to User cache list.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public override void Add(KeyType key, ValueType value, Action<KeyType, ValueType> addAlgorithm = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gives Key to Remove
        /// </summary>
        /// <returns></returns>
        public override KeyType GetKeyToRemove(Action<KeyType, ValueType> getKeyToRemoveAlgorithm = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes key/value pair from User cache list.
        /// </summary>
        public override void Remove(Action<KeyType, ValueType> removeAlgorithm = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the key/value pair position on first in User cache list.
        /// </summary>
        /// <param name="key"></param>
        public override void Update(KeyType key, Action<KeyType, ValueType> updateAlgorithm = null)
        {
            throw new NotImplementedException();
        }
    }
}
