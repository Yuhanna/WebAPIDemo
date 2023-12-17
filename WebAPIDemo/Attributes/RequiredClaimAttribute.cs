﻿namespace WebAPIDemo.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RequiredClaimAttribute : Attribute
    {
        public string ClaimType { get; }
        public string ClaimValue { get; }

        public RequiredClaimAttribute(string claimType, string cliamValue)
        {
            this.ClaimType = claimType;
            this.ClaimValue = cliamValue;   
        }
    }
}
