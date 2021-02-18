using System.Collections;

namespace Spire.Resources
{
    public interface IReducable
    {
        float cachedMax { get; set; }
        void CacheCurrentMax();
        IEnumerator ReduceMax(float duration);
    }
}