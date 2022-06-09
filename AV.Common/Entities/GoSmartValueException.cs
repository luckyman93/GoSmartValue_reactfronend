using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class GoSmartValueException : Exception
    {
        [Key]
        public int Id { get; set; }

        public IList<Exception> Exceptions { get; set; }
        public Exception Exception { get; set; }

        public GoSmartValueException(string message) : base(message)
        { }

        public GoSmartValueException(string message, Exception exception) : base(message)
        {
            Exceptions = new List<Exception>();
            Exception = exception;
        }

        public GoSmartValueException(IList<Exception> exceptions)
        {
            Exceptions = new List<Exception>();
            Exceptions = exceptions;
        }

        public void AddException(string message, Exception exception)
        {
            var error = new Exception(message, exception);
            Exceptions.Add(error);
        }
    }
}