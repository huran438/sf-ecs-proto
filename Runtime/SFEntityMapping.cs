using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;
namespace SFramework.ECS.Proto.Runtime
{
    public static class SFEntityMapping
    {
        private static readonly Dictionary<int, ProtoPackedEntityWithWorld> _packedEntities;

        static SFEntityMapping()
        {
            _packedEntities = new Dictionary<int, ProtoPackedEntityWithWorld>();
        }

        static internal void AddMapping(GameObject gameObject, ref ProtoWorld world, ref ProtoEntity entity)
        {
            _packedEntities[gameObject.GetInstanceID()] = world.PackEntityWithWorld(entity);
        }

        static internal void AddMapping(GameObject gameObject, ref ProtoPackedEntityWithWorld entity)
        {
            _packedEntities[gameObject.GetInstanceID()] = entity;
        }

        static internal void RemoveMapping(GameObject gameObject)
        {
            var instanceId = gameObject.GetInstanceID();
            
            if (_packedEntities.ContainsKey(instanceId))
            {
                _packedEntities.Remove(instanceId);
            }
        }

        public static bool GetEntity(GameObject gameObject, out ProtoWorld world, out ProtoEntity entity)
        {
            var instanceId = gameObject.GetInstanceID();
            
            if (_packedEntities.TryGetValue(instanceId, out var packedEntityWithWorld))
            {
                return packedEntityWithWorld.Unpack(out world, out entity);
            }
            
            entity = default;
            world = default;
            return false;
        }
        
        public static bool GetEntity(GameObject gameObject, out ProtoEntity entity)
        {
            var instanceId = gameObject.GetInstanceID();
            
            if (_packedEntities.TryGetValue(instanceId, out var packedEntityWithWorld))
            {
                return packedEntityWithWorld.Unpack(out _, out entity);
            }
            
            entity = default;
            return false;
        }

        public static bool GetEntityPacked(GameObject gameObject, out ProtoPackedEntityWithWorld packedEntity)
        {
            var instanceId = gameObject.GetInstanceID();
            
            if (_packedEntities.TryGetValue(instanceId, out var packedEntityWithWorld))
            {
                packedEntity = packedEntityWithWorld;
                return true;
            }

            packedEntity = default;
            return false;
        }

        public static void Clear()
        {
            _packedEntities.Clear();
        }
    }
}