using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Response
{
    public static class ResponseMessages
    {
        //register messages
        public const string AccountRegisteredSuccess = "Account registered successfully.";
        public const string AccountRegistrationFailed = "Account registration failed. Please try again.";
        public const string AccountAlreadyExists = "An account with this email or phone number already exists.";

        // phone messages
        public const string AccountNotFound = "Account with this phone number not found.";
        public const string PhoneNumberRequired = "Phone number is required.";
        public const string OperationFailed = "Operation failed. Please try again.";
        public const string AccountFoundSuccess = "Account found successfully.";
    }
}
