using System;
using System.Linq;
using AutoMapper;
using DocHelper.Domain.Common.Extensions;
using DocHelper.Domain.Common.Mappings;

namespace DocHelper.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly();
        }

        private void ApplyMappingsFromAssembly()
        {
            var types = AppDomain.CurrentDomain.GetDomainAssembly().GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                                 ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] {this});
            }
        }
    }
}