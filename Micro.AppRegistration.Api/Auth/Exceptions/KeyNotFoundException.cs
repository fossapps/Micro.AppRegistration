using System;

namespace Micro.AppRegistration.Api.Auth.Exceptions
{
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException(string message) : base(message)
        {
        }
    }
}
