using System;
using System.Linq;
using System.Reflection;

namespace DocHelper.Application.Common.App
{
    public static class AppDomainExtensions
    {
        private const string DomainAssemblyName = "DocHelper.Domain";
        
        public static Assembly GetDomainAssembly(this AppDomain domain)
        {
            return domain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == DomainAssemblyName);
        }
    }
}