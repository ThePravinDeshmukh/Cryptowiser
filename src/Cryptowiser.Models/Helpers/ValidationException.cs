using System;
using System.Collections.Generic;
using System.Globalization;

namespace Cryptowiser.Models
{
    public class ValidationException : Exception
    {
        private const string VALIDATION_FAILED = "VALIDATION_FAILED";
        public ValidationException(string errorCode, string errorMessage) : base($"{errorCode} {VALIDATION_FAILED}: {errorMessage}") { }

    }
}