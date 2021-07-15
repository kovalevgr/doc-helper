using System;
using System.Linq;
using System.Reflection;

namespace DocHelper.Domain.Common.Extensions
{
    public static class AssemblyExtensions
    {
        private const string DomainAssemblyName = "DocHelper.Domain";

        public static Assembly GetDomainAssembly(this AppDomain domain)
        {
            return domain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == DomainAssemblyName);
        }
    }
}