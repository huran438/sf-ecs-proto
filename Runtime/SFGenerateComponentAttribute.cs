using System;
namespace SFramework.ECS.Proto.Runtime
{
    [AttributeUsage(AttributeTargets.Struct)]
    public class SFGenerateComponentAttribute : Attribute
    {
        public Type CustomBaseType;
        
        public SFGenerateComponentAttribute()
        {
            CustomBaseType = null;
        }
        
        public SFGenerateComponentAttribute(Type customBaseType)
        {
            CustomBaseType = customBaseType;
        }
    }
}