using System;
using System.ComponentModel.DataAnnotations;

namespace Micro.AppRegistration.Api.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class StartsWith : ValidationAttribute
    {
        private readonly string _prefix;

        public StartsWith(string prefix) : base($"The {{0}} field must start with {prefix}")
        {
            _prefix = prefix;
        }

        public override bool IsValid(object value)
        {
            return value != null && value is string stringVal && stringVal.StartsWith(_prefix);
        }
    }
}