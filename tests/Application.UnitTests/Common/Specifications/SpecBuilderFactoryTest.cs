using System.Collections.Generic;
using DocHelper.Application.Common.Specifications;
using DocHelper.Domain.Common.Interfaces;
using DocHelper.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.Common.Specifications
{
    [TestFixture]
    public class SpecBuilderFactoryTest
    {
        private readonly Mock<IApplicationDbContext> _dbContext;

        public SpecBuilderFactoryTest()
        {
            _dbContext = new Mock<IApplicationDbContext>();
        }
        
        public static IEnumerable<BaseEntity> TestCases()
        {
            yield return new Specialty();
            yield return new City();
        }
        
        [TestCaseSource("TestCases")]
        public void ShouldSupportSpecBuilderByEntityType<T>(T entity) where T : BaseEntity
        {
            var builder = new SpecBuilderFactory(_dbContext.Object);

            builder.Create<T>();
        }
    }
}