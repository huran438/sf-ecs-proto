namespace SFramework.ECS.Proto.Runtime
{
    public interface IEcsAutoInit<T> where T : struct
    {
        void AutoInit(ref T c);
    }
}