using System.Threading.Tasks;
using Doctor;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Channel = Grpc.Net.Client.GrpcChannel;

namespace CabinetApp.Components.Doctor
{
    public class CreateDoctorComponent : ComponentBase
    {
        [CanBeNull] private DoctorData Doctor;
        private Channel ChannelBase { get; }

        protected async Task Create()
        {
            var client = new DoctorRPCService.DoctorRPCServiceClient(ChannelBase);

            var response = await client.CreateDoctorAsync(Doctor);
        }
    }
}