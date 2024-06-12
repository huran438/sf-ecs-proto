using System;
using Sirenix.OdinInspector;
namespace SFramework.ECS.Proto.Runtime
{
    [Serializable]
    public class SFSystemContainer
    {
        [HideLabel]
        [HorizontalGroup]
        public bool Enabled = true;
        
        [HideLabel]
        [HorizontalGroup]
        public string System;
    }
}