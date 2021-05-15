using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptowiser.Models.Helpers
{
    public struct Constants
    {
        public const string CRYPTOWISER_CONTEXT = "CryptowiserContext";
        public const string PASSWORD_EMPTY_ERROR = "Password is required";
        public const string USERNAME_TAKEN = "Username is already taken";
        public const string PASSWORD = "password";
        public const string PASSWORD_HASH = "passwordHash";
        public const string VALUE_EMPTY_ERROR = "Value cannot be empty or whitespace only string.";
        public const string USER_NOT_FOUND = "User not found";

        public const string INVALID_LENGTH_128 = "Invalid length of password salt (128 bytes expected).";
        public const string INVALID_LENGTH_64 = "Invalid length of password hash (64 bytes expected).";
    }
}
