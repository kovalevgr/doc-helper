using System.Collections.Generic;
using System.Threading.Tasks;
using DocHelper.Application.Doctor.Command.CreateDoctorCommand;
using DocHelper.Domain.Dto.Doctor.Create;
using DocHelper.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Doctor.Command
{
    using DocHelper.Domain.Entities.DoctorAggregate;
    using static Testing;

    public class CreateDoctorCommandTest : TestBase
    {
        [Test]
        public async Task ShouldCreateDoctor()
        {
            var command = new CreateDoctorCommand
            {
                Alias = "alias",
                FirstName = "FirstName",
                LastName = "LastName",
                MiddleName = "MiddleName",
                Titles = "Titles",
                WorkExperience = 1,
                Description = "Description",
                Photo = "Photo",
                Informations = new List<InformationDto>(
                    new []
                    {
                        new InformationDto
                        {
                            Type = InformationType.Courses,
                            Title = "title",
                            Priority = 1
                        }
                    }
                ),
                Specialties = new List<SpecialtyDto>(
                    new []
                    {
                        new SpecialtyDto
                        {
                            Title = "Title",
                            Alias = "Alias"
                        }
                    }
                )
            };

            var doctorId = await SendAsync(command);
            
            var item = await FindAsync<Doctor>(doctorId);
            
            item.Should().NotBeNull();
            item.Id.Should().Be(doctorId);
            item.Description.Should().Be(command.Description);
            item.Alias.Should().Be(command.Alias);
            item.FirstName.Should().Be(command.FirstName);
            item.LastName.Should().Be(command.LastName);
            item.MiddleName.Should().Be(command.MiddleName);
            item.Titles.Should().Be(command.Titles);
            item.WorkExperience.Should().Be(command.WorkExperience);
            item.Photo.Should().Be(command.Photo);
        }
    }
}