using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//הקלאסים פה זהם בישביל אקספשנים שאני מייצר על דברים שאני רוצה
//כל פעם צריך לבנות קלאס חדש בישביל אקספשן חדש שאני רוצה לשים
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

    [Serializable]
    public class MissingHeaderException : Exception
    {
        public MissingHeaderException()
        { }

        public MissingHeaderException(string message)
            : base(message)
        { }

        public MissingHeaderException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}