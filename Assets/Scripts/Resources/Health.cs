using System.Collections;

namespace Spire.Resources
{
    public class Health : Resource, IReducable, IRegeneratable
    {
        /// <summary>
        /// inherited fields are as follows:
        /// float baseValue, float maxValue, float curValue, bool hasChanged, List(ResourceMod) resourceMods.
        /// </summary>

        private IDamageable damageable;
        public float cachedMax { get => maxValue; set => throw new System.NotImplementedException(); }
        public float baseRegenRate { get => CalcRegenRate(); set => throw new System.NotImplementedException(); }

        public void GetDamage(IDamageable damageable)
        {

        }

        public override void CalcMaxValue()
        {
            throw new System.NotImplementedException();
        }
        public void CacheCurrentMax()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator ReduceMax(float duration)
        {
            throw new System.NotImplementedException();
        }

        public float CalcRegenRate()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator RegenerateCoroutine()
        {
            throw new System.NotImplementedException();
        }
    }
}