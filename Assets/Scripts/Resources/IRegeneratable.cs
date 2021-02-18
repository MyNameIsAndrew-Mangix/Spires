using System.Collections;

namespace Spire.Resources
{
    public interface IRegeneratable
    {
        float baseRegenRate { get; set; }
        float CalcRegenRate();
        IEnumerator RegenerateCoroutine();
    }
}