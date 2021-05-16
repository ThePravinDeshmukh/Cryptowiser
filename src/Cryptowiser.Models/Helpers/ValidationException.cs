using System;
using System.Collections.Generic;
using System.Globalization;

namespace Cryptowiser.Models
{
    public class ValidationException : Exception
    {
        private const string VALIDATION_FAILED = "VALIDATION_FAILED";
        public ValidationException() : base() {}
        public ValidationException(string errorCode, string errorMessage) : base($"{errorCode} {VALIDATION_FAILED}: {errorMessage}") { }

        public ValidationException(string errorCode, string errorMessage, params object[] args) 
            : base(String.Format(CultureInfo.CurrentCulture, $"{errorCode} {VALIDATION_FAILED}: {errorMessage}", args))
        {
        }
    }
}