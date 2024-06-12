using Leopotam.EcsProto;
using SFramework.Core.Runtime;
namespace SFramework.ECS.Proto.Runtime
{
    public interface ISFWorldsService : ISFService
    {
        public ProtoWorld GetWorld(string name = "");
        public ProtoWorld[] Worlds { get; }
    }
}