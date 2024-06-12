using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsProto;
using SFramework.Configs.Runtime;
using UnityEngine;
namespace SFramework.ECS.Proto.Runtime
{
    public class SFWorldsService : ISFWorldsService
    {
        private readonly Dictionary<string, ProtoWorld> _ecsWorlds = new();
        private readonly List<ProtoSystems> _lateUpdateSystems = new();
        private readonly List<ProtoSystems> _updateSystems = new();
        private readonly List<ProtoSystems> _fixedUpdateSystems = new();
        private readonly SFECSCallbacks _callbacks;

        private ProtoWorld _defaultWorld;
        public ProtoWorld[] Worlds => _ecsWorlds.Values.ToArray();

        public ProtoWorld GetWorld(string name = "")
        {
            if (string.IsNullOrEmpty(name)) return _defaultWorld;

            return _ecsWorlds.TryGetValue(name, out var world) ? world : _defaultWorld;
        }

        SFWorldsService(ISFConfigsService provider)
        {
            _defaultWorld = new ProtoWorld(new DefaultAspect());
            _callbacks = new GameObject("SF_ECS_CALLBACKS").AddComponent<SFECSCallbacks>();
            _callbacks.OnFixedUpdate += FixedUpdate;
            _callbacks.OnUpdate += Update;
            _callbacks.OnLateUpdate += LateUpdate;

            var repositories = provider.GetRepositories<SFWorldsConfig>();

            foreach (var repository in repositories)
            {
                foreach (SFWorldNode worldContainer in repository.Children)
                {
                    AddWorldContainer(worldContainer);
                }
            }
        }

        private ProtoWorld AddWorldContainer(SFWorldNode worldNode)
        {
            if (_ecsWorlds.ContainsKey(worldNode.Id)) return null;
            
            var world = new ProtoWorld(new DefaultAspect());
            var fixedUpdateSystems = new ProtoSystems(world);
            var updateSystems = new ProtoSystems(world);
            var lateUpdateSystems = new ProtoSystems(world);

            foreach (var system in worldNode.FixedUpdateSystems)
            {
                if (!system.Enabled) continue;
                var systemType = Type.GetType(system.System);
                if (systemType == null) continue;
                var systemInstance = Activator.CreateInstance(systemType) as IProtoSystem;
                if (systemInstance == null) continue;
                fixedUpdateSystems.AddSystem(systemInstance);
            }

            foreach (var system in worldNode.UpdateSystems)
            {
                if (!system.Enabled) continue;
                var systemType = Type.GetType(system.System);
                if (systemType == null) continue;
                var systemInstance = Activator.CreateInstance(systemType) as IProtoSystem;
                if (systemInstance == null) continue;
                updateSystems.AddSystem(systemInstance);
            }

            foreach (var system in worldNode.LateUpdateSystems)
            {
                if (!system.Enabled) continue;
                var systemType = Type.GetType(system.System);
                if (systemType == null) continue;
                var systemInstance = Activator.CreateInstance(systemType) as IProtoSystem;
                if (systemInstance == null) continue;
                lateUpdateSystems.AddSystem(systemInstance);
            }

            fixedUpdateSystems.Init();
            updateSystems.Init();
            lateUpdateSystems.Init();


            _fixedUpdateSystems.Add(fixedUpdateSystems);
            _updateSystems.Add(updateSystems);
            _lateUpdateSystems.Add(lateUpdateSystems);
            _ecsWorlds[worldNode.Id] = world;
            return world;
        }


        private void FixedUpdate()
        {
            foreach (var systems in _fixedUpdateSystems)
            {
                systems.Run();
            }
        }

        private void Update()
        {
            foreach (var systems in _updateSystems)
            {
                systems.Run();
            }
        }

        private void LateUpdate()
        {
            foreach (var systems in _lateUpdateSystems)
            {
                systems.Run();
            }
        }

        public void Dispose()
        {
            _fixedUpdateSystems.Clear();
            _updateSystems.Clear();
            _lateUpdateSystems.Clear();

            _callbacks.OnFixedUpdate -= FixedUpdate;
            _callbacks.OnUpdate -= Update;
            _callbacks.OnLateUpdate -= LateUpdate;
        }
    }
}
