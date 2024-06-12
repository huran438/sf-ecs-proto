using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
namespace SFramework.ECS.Proto.Runtime
{
    public class DefaultAspect : ProtoAspectInject
    {
        public readonly ProtoPool<GameObjectRef> GameObjectRefPool;
        public readonly ProtoPool<TransformRef> TransformRefPool;
        public readonly ProtoPool<RootEntity> RootEntityPool;
    }
}
