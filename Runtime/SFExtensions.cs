using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;
namespace SFramework.ECS.Proto.Runtime
{
    public static partial class SFExtensions
    {
        public static IProtoSystems Add(this IProtoSystems ecsSystems, params ISFSystem[] systems)
        {
            foreach (var system in systems)
            {
                if (system is IProtoSystem protoSystem)
                {
                    ecsSystems.AddSystem(protoSystem);
                }
            }

            return ecsSystems;
        }
        
        public static bool TryGetEntity(this GameObject gameObject, out ProtoEntity entity)
        {
            return SFEntityMapping.GetEntity(gameObject, out entity);
        }
        
        public static bool TryGetEntityPacked(this GameObject gameObject, out ProtoPackedEntityWithWorld packedEntity)
        {
            return SFEntityMapping.GetEntityPacked(gameObject, out packedEntity);
        }
    }
}