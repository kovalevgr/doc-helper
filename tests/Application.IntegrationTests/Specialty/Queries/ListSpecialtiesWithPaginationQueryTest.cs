using System.Threading.Tasks;
using DocHelper.Application.Specialty.Queries.ListSpecialtiesWithPagination;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Specialty.Queries
{
    using static Testing;

    public class ListSpecialtiesWithPaginationQueryTest : TestBase
    {
        [Test]
        public async Task ShouldReturnAllLists()
        {
            var query = new ListSpecialtiesWithPaginationQuery();

            var result = await SendAsync(query);
            
            result.Should().NotBeEmpty();
        }
    }
}