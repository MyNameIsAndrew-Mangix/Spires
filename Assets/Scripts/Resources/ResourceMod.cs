using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spire.Resources
{
    public enum ResourceModType
    {
        Flat = 100,
        PercentAdd = 200,
    }
    public class ResourceMod
    {
        public readonly float value;
        public readonly int order;
        public readonly ResourceModType resourceModType;
        public readonly object source;

        public ResourceMod(float value, ResourceModType type, int order, object source)
        {
            this.value = value;
            resourceModType = type;
            this.order = order;
            this.source = source;
        }
    }

}