using Leopotam.EcsProto.QoL;
namespace SFramework.ECS.Proto.Runtime
{
    public interface ISFEntitySetup
    {
        void Setup(ref ProtoPackedEntityWithWorld packedEntity);
    }
}