using Leopotam.EcsProto;
using SFramework.Core.Runtime;
namespace SFramework.ECS.Proto.Runtime
{
    public abstract class SFSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem, ISFInjectable, ISFSystem
    {
        public void Init(IProtoSystems systems)
        {
            this.Inject();
            OnInit(systems);
        }
        public void Run()
        {
            OnRun();
        }

        public void Destroy()
        {
            OnDestroy();
        }

        protected abstract void OnInit(IProtoSystems systems);
        protected abstract void OnRun();
        protected abstract void OnDestroy();
    }
}
