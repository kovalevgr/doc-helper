using System.Collections.Generic;
using System.Threading.Tasks;
using DocHelper.Application.City.Queries.ListCities;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.City.Queries
{
    using static Testing;

    public class ListCitiesQueryTest : TestBase
    {
        [Test]
        public async Task ShouldReturnAllLists()
        {
            var cities = new List<DocHelper.Domain.Entities.City>
            {
                new() {Name = "Kiev", Alias = "kiev"},
                new() {Name = "Dnipro", Alias = "dnipro"},
                new() {Name = "Lviv", Alias = "lviv"},
            };

            foreach (var city in cities)
            {
                await AddAsync(city);
            }
            
            var query = new ListCitiesQuery();

            var result = await SendAsync(query);
            
            result.Should().HaveCount(6);
        }
    }
}