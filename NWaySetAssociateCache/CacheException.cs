using System;
using System.Collections.Generic;
using System.Text;

namespace NWaySetAssociateCache
{
    public class CacheException : Exception
    {
        public CacheException(string message, Exception ex = null) : base(message, ex) { }
    }
}
