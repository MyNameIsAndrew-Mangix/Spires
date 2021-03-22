using System.Collections.Generic;
using UnityEngine;

namespace Spire.Resources
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] protected float baseValue;
        protected bool hasChanged;
        [SerializeField] protected float maxValue;
        [SerializeField] protected float currentValue;
        [SerializeField] protected List<ResourceMod> resourceMods;

        public virtual void CalcMaxValue()
        {

        }

        public virtual void AddMod(ResourceMod mod)
        {
            hasChanged = true;
            resourceMods.Add(mod);
            resourceMods.Sort(CompareModOrder);
        }

        public virtual bool RemoveMod(ResourceMod mod)
        {
            if (resourceMods.Remove(mod))
            {
                hasChanged = true;
                return true;
            }
            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = resourceMods.Count - 1; i >= 0; i--)
            {
                if (resourceMods[i].source == source)
                {
                    hasChanged = true;
                    didRemove = true;
                    resourceMods.RemoveAt(i);
                }
            }
            return didRemove;
        }

        protected virtual int CompareModOrder(ResourceMod x, ResourceMod y)
        {
            if (x.order < y.order)
                return -1;
            else if (x.order > y.order)
                return 1;
            return 0;
        }
    }
}