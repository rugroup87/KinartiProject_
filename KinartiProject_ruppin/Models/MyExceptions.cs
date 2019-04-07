using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinartiProject_ruppin.Models
{
    [Serializable]
    public class DuplicatePrimaryKeyException : Exception
    {
        public DuplicatePrimaryKeyException()
        { }

        public DuplicatePrimaryKeyException(string message)
            : base(message)
        { }

        public DuplicatePrimaryKeyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}