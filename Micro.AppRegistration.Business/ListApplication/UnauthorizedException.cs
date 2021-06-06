using System;

namespace Micro.AppRegistration.Business.ListApplication
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("You don't have permission to access this resource")
        {
        }
    }
}
