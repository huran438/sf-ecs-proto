using SFramework.Configs.Runtime;
namespace SFramework.ECS.Proto.Runtime
{
    public class SFWorldAttribute : SFIdAttribute
    {
        public SFWorldAttribute() : base(typeof(SFWorldsConfig), -1)
        {
        }
    }
}