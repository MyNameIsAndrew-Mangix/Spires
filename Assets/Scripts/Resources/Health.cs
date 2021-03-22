using System.Collections;
using UnityEngine;
using Spire.Stats;

namespace Spire.Resources
{
    public class Health : Resource, IReducable
    {
        /// <summary>
        /// inherited fields are as follows:
        /// float baseValue, float maxValue, float currentValue, bool hasChanged, List(ResourceMod) resourceMods.
        /// TODO: DELEGATE COMBAT STATE TO ANOTHER SCRIPT.
        /// ways to check if in combat: interface in resources inherited by character, coroutine, statemanager.
        /// </summary>
        private float _cachedMax;

        [SerializeField] private bool _canRegen;
        [SerializeField] private bool _inCombat;

        private float _timeUntilFullHpOutOfCombat = 4f;

        [SerializeField] private StatBlock _statblock;

        private void Start()
        {
            InitializeHealth();
        }

        private void InitializeHealth()
        {
            CalcMaxValue();
            CacheCurrentMax();
            currentValue = maxValue;
        }
        public void TakeDamage(float amount, float defense)
        {
            currentValue -= (amount - defense);
            if (_canRegen)
                _canRegen = false;

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
            float stopwatch = 0f;
            float countDown = 3f;
            yield return new WaitWhile(() => _inCombat);
            if (!_inCombat)
            {
                while (!_canRegen) // waits for 3 seconds before being able to regen after being out of combat
                {
                    stopwatch += Time.deltaTime;
                    if (stopwatch >= countDown)
                        _canRegen = true;// has been out of combat for 3 seconds.
                }
                stopwatch = 0f;
                while (_canRegen && timeElapsed < _timeUntilFullHpOutOfCombat) //not in combat
                {
                    if (currentValue > 0 && currentValue != maxValue)
                    {
                        if (currentValue < maxValue)
                        {
                            if (currentValue >= maxValue - 1)
                                currentValue = maxValue;

                            float lerpStart = currentValue;
                            currentValue = Mathf.Lerp(lerpStart, maxValue, timeElapsed / _timeUntilFullHpOutOfCombat);
                            timeElapsed += Time.deltaTime;
                            yield return null;
                        }


                        if (currentValue == maxValue)
                            yield break;
                    }
                    yield break;
                }
                yield break;
            }
        }
    }
}