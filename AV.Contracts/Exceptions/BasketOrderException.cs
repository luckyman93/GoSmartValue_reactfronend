using System;

namespace AV.Contracts.Exceptions
{
    public class BasketOrderException : Exception
    {
        public BasketOrderException()
        { }

        public BasketOrderException(string message)
            : base(message)
        { }

        public BasketOrderException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
