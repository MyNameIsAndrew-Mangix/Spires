using System.Collections;
using UnityEngine;
using Spire.Stats;
using System;

namespace Spire.Resources
{
    public class Health : Resource, IReducable, IDamageable
    {
        /// <summary>
        /// inherited fields are as follows:
        /// float baseValue, float maxValue, float currentValue, bool hasChanged, List(ResourceMod) resourceMods.
        /// </summary>
        private float _cachedMax;

        [SerializeField] private bool _canRegen;
        [SerializeField] private bool _inCombat;

        private float _timeUntilFullHpOutOfCombat = 4f;

        [SerializeField] private StatBlock _statblock;

        private void Awake()
        {
            InitializeHealth();
        }

        private void InitializeHealth()
        {
            CalcMaxValue();
            CacheCurrentMax();
            currentValue = maxValue;
        }
        //for combat do a thing with checking if it's damageable or not.
        public void TakeDamage(float amount, float defense)
        {
            currentValue -= (amount - defense);
        }

        public void StartRegenRoutine()
        {
            StartCoroutine(RegenerateCoroutine());
        }

        public void Heal(float amount, float modifier)
        {
            currentValue += (amount *= modifier);
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
            _cachedMax = maxValue;
        }

        public IEnumerator ReduceMax(int amount, float duration)
        {
            throw new System.NotImplementedException();
        }

        public float CalcRegenRate(float percentOverTime)
        {
            return 0f;
        }

        private IEnumerator RegenerateCoroutine()
        {
            Debug.Log("RegenCoroutine started");
            float timeElapsed = 0f;

            while (!_inCombat && _canRegen && timeElapsed < _timeUntilFullHpOutOfCombat) //not in combat
            {
                if (currentValue > 0 && currentValue != maxValue)
                {
                    if (currentValue < maxValue)
                    {
                        float lerpStart = currentValue;
                        currentValue = Mathf.Lerp(lerpStart, maxValue, timeElapsed / _timeUntilFullHpOutOfCombat);
                        timeElapsed += Time.deltaTime;
                        yield return null;
                    }
                    currentValue = maxValue;

                    if (currentValue == maxValue)
                        yield return null;
                }
                yield return null;
            }
            yield return null;
        }
    }
}
}