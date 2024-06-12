using Leopotam.EcsProto.QoL;
namespace SFramework.ECS.Proto.Runtime
{
    public interface ISFEntity
    {
        ProtoPackedEntityWithWorld EcsPackedEntity { get; }
    }
}