using System.Collections;
using UnityEngine;
using Spire.Stats;
using System;

namespace Spire.Resources
{
    public class Health : Resource, IReducable, IRegeneratable
    {
        /// <summary>
        /// inherited fields are as follows:
        /// float baseValue, float maxValue, float curValue, bool hasChanged, List(ResourceMod) resourceMods.
        /// </summary>

        private IDamageable damageable;
        public float cachedMax { get => maxValue; set => CacheCurrentMax(); }
        public float baseRegenRate { get => CalcRegenRate(); set => throw new System.NotImplementedException(); }

        [SerializeField] private StatBlock _statblock;

        private void Awake()
        {
            InitializeHealth();
        }

        private void InitializeHealth()
        {
            CalcMaxValue();
            CacheCurrentMax();
        }

        public void GetDamage(IDamageable iDamageable)
        {
            damageable = iDamageable;
        }

        public override void CalcMaxValue()
        {
            //get current level to add to calculation (10 HP per level)
            int hpPerEndurancePnt = 20;
            int hpIncrease = (int)(baseValue + (hpPerEndurancePnt *= _statblock.attributeBlock.strength.currentValue));
            maxValue = hpIncrease;
        }
        public void CacheCurrentMax()
        {
            cachedMax = maxValue;
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