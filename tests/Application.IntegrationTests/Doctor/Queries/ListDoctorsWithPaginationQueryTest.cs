using System.Threading.Tasks;
using DocHelper.Application.Doctor.Queries.ListDoctorsWithPagination;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Doctor.Queries
{
    using static Testing;

    public class ListDoctorsWithPaginationQueryTest
    {
        [Test]
        public async Task ShouldReturnAllLists()
        {
            var query = new ListDoctorsWithPaginationQuery();

            var result = await SendAsync(query);
            
            result.Should().NotBeEmpty();
        }
    }
}