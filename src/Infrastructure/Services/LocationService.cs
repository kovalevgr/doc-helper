using DocHelper.Application.Common.Interfaces;
using DocHelper.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace DocHelper.Infrastructure.Services
{
    public class LocationService : ILocationService
    {
        public Location Location { get; set; } = Location.GetCurrentLocation();

        private IConfiguration _configuration;

        public LocationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}