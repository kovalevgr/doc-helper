using System;
using System.Reflection;
using AutoMapper;
using DocHelper.Domain.Dto;

namespace DocHelper.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = new[] {typeof(DoctorListDto), typeof(SpecialtyDto), typeof(StatsDto), typeof(InformationDto)};

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