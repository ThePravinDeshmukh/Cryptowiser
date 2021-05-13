using System;
using System.Globalization;

namespace Cryptowiser.Models
{
    // Custom exception class for throwing application specific exceptions (e.g. for validation) 
    // that can be caught and handled within the application
    public class ValidationException : Exception
    {
        public ValidationException() : base() {}

        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, params object[] args) 
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}