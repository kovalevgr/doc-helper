using System;
using System.Runtime.Serialization;
using AutoMapper;
using DocHelper.Application.Common.Mappings;
using DocHelper.Domain.Dto;
using DocHelper.Domain.Entities;
using DocHelper.Domain.Entities.DoctorAggregate;
using NUnit.Framework;
using InformationDto = DocHelper.Domain.Dto.InformationDto;

namespace Application.UnitTests.Common.Mappings
{
    [TestFixture]
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Test]
        [TestCase(typeof(Specialty), typeof(SpecialtyDto))]
        [TestCase(typeof(Stats), typeof(StatsDto))]
        [TestCase(typeof(Information), typeof(InformationDto))]
        [TestCase(typeof(Doctor), typeof(DoctorListDto))]
        [TestCase(typeof(DoctorSpecialty), typeof(DoctorSpecialtyDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            return type.GetConstructor(Type.EmptyTypes) != null
                ? Activator.CreateInstance(type)
                : FormatterServices.GetUninitializedObject(type);
        }
    }
}