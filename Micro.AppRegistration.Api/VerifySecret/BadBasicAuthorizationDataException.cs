using System;

namespace Micro.AppRegistration.Api.VerifySecret
{
    public class BadBasicAuthorizationDataException : Exception
    {
        public BadBasicAuthorizationDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}