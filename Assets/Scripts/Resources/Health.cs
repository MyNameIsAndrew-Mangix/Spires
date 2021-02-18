using System.Collections;

namespace Spire.Resources
{
    public class Health : Resource, IDamageable, IReducable, IRegeneratable
    {
        /// <summary>
        /// inherited fields are as follows:
        /// float baseValue, float maxValue, float curValue;
        /// </summary>
        public float cachedMax { get => maxValue; set => throw new System.NotImplementedException(); }
        public float baseRegenRate { get => CalcRegenRate(); set => throw new System.NotImplementedException(); }

        public override void CalcMaxValue()
        {
            maxValue = baseValue;
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

        public void TakeDamage(float amount)
        {
            throw new System.NotImplementedException();
        }
        public void Heal(float amount)
        {
            throw new System.NotImplementedException();
        }

    }
}