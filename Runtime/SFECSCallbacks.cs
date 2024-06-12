using System;
using UnityEngine;
namespace SFramework.ECS.Proto.Runtime
{
    public class SFECSCallbacks : MonoBehaviour
    {
        public event Action OnFixedUpdate = () => { };
        public event Action OnUpdate = () => { };
        public event Action OnLateUpdate = () => { };

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void FixedUpdate()
        {
            OnFixedUpdate.Invoke();
        }

        private void Update()
        {
            OnUpdate.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate.Invoke();
        }
    }
}