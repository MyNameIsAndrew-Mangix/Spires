using System;
using System.Collections.Generic;

namespace Spire.Stats
{
    public class Stat
    {
        protected bool hasChanged;
        protected float baseValue;
        protected float previousBaseValue;
        protected float modifiedValue;

        protected List<StatMod> statMods;

        public float Value
        {
            get
            {
                if (hasChanged || previousBaseValue != baseValue)
                {
                    previousBaseValue = baseValue;
                    modifiedValue = CalcFinalValue();
                    hasChanged = false;
                }
                return modifiedValue;
            }
        }

        public virtual void AddMod(StatMod mod)
        {
            hasChanged = true;
            statMods.Add(mod);
            statMods.Sort(CompareModifiedOrder);
        }

        public virtual bool RemoveMod(StatMod mod)
        {
            if (statMods.Remove(mod))
            {
                hasChanged = true;
                return true;
            }
            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = statMods.Count - 1; i >= 0; i--)
            {
                if (statMods[i].source == source)
                {
                    hasChanged = true;
                    didRemove = true;
                    statMods.RemoveAt(i);
                }
            }
            return didRemove;
        }

        protected virtual int CompareModifiedOrder(StatMod x, StatMod y)
        {
            if (x.order < y.order)
                return -1;
            else if (x.order > y.order)
                return 1;
            return 0;
        }

        protected virtual float CalcFinalValue()
        {
            float finalValue = baseValue;
            float sumPercentAdd = 0f;

            for (int i = 0; i < statMods.Count; i++)
            {
                StatMod mod = statMods[i];
                if (mod.statModType == StatModType.Flat)
                    finalValue += statMods[i].value;

                else if (mod.statModType == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.value;
                    if (i + 1 >= statMods.Count || statMods[i + 1].statModType != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
            }
            return (float)Math.Round(finalValue, 4);
        }
    }
}
