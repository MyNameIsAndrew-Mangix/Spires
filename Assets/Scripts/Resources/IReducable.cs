using System.Collections;

namespace Spire.Resources
{
    public interface IReducable
    {
        void CacheCurrentMax();
        IEnumerator ReduceMax(int amount, float duration);
    }
}