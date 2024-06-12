using Leopotam.EcsProto.QoL;
using SFramework.Core.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
namespace SFramework.ECS.Proto.Runtime
{
    [HideMonoScript]
    [DisallowMultipleComponent]
    public sealed class SFEntity : SFView, ISFEntity
    {
        public ProtoPackedEntityWithWorld EcsPackedEntity => _ecsPackedEntity;

        [SFInject]
        private ISFWorldsService _worldsService;

        [SFWorld]
        [SerializeField]
        private string _world;
        
        private ProtoPackedEntityWithWorld _ecsPackedEntity;
        private bool _injected;
        private ISFEntitySetup[] _components;

        private bool _isRootEntity;

        protected override void Init()
        {
            if (_injected) return;
            _components = GetComponents<ISFEntitySetup>();
            _isRootEntity = transform.parent == null || transform.parent.GetComponentInParent<SFEntity>(true) == null;
            _injected = true;
        }

        public void OnEnable()
        {
            var world = _worldsService.GetWorld(_world);
            var entity = world.NewEntity();
            _ecsPackedEntity = world.PackEntityWithWorld(entity);

            SFEntityMapping.AddMapping(gameObject, ref _ecsPackedEntity);
            
            world.Pool(typeof(GameObjectRef)).AddRaw(entity);
            world.Pool(typeof(TransformRef)).AddRaw(entity);
            world.Pool(typeof(GameObjectRef)).SetRaw(entity, new GameObjectRef { value = gameObject });
            world.Pool(typeof(TransformRef)).SetRaw(entity, new TransformRef { value = transform });
            
            if (_isRootEntity)
            {
                world.Pool(typeof(RootEntity)).AddRaw(entity);
            }
            
            foreach (var entitySetup in _components)
            {
                entitySetup.Setup(ref _ecsPackedEntity);
            }

        }

        public void OnDisable()
        {
            SFEntityMapping.RemoveMapping(gameObject);

            if (_ecsPackedEntity.Unpack(out var world, out var entity))
                world.DelEntity(entity);
        }
    }
}