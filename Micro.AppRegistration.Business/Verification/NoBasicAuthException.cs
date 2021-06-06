using System;

namespace Micro.AppRegistration.Business.Verification
{
    public class NoBasicAuthException : Exception
    {
        public NoBasicAuthException() : base("Basic authentication must be present for this query")
        {
        }
    }
}
