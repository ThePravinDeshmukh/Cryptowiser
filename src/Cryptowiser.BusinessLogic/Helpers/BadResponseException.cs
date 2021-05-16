using System;
using System.Globalization;

namespace Cryptowiser.Models
{
    public class BadResponseException : Exception
    {
        private const string EXTERNAL_API_BAD_RESPONSE = "EXTERNAL_API_BAD_RESPONSE";
        public BadResponseException() : base() {}

        public BadResponseException(string errorCode, string errorMessage) : base($"{errorCode} {EXTERNAL_API_BAD_RESPONSE}: {errorMessage}") { }

        public BadResponseException(string errorCode, string errorMessage, params object[] args) 
            : base(String.Format(CultureInfo.CurrentCulture, $"{errorCode} {EXTERNAL_API_BAD_RESPONSE}: {errorMessage}", args))
        {
        }
    }
}