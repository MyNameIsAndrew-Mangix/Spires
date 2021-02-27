using System.Collections.Generic;
using UnityEngine;

namespace Spire.Stats
{
    [System.Serializable]
    public class AttributeBlock : ScriptableObject
    {
        public Attribute strength;
        public Attribute agility;
        public Attribute endurance;
        public Attribute wits;
        public Attribute perseverance;
    }
}