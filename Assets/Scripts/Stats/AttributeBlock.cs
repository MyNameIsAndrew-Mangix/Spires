using System.Collections.Generic;
using UnityEngine;

namespace Spire.Stats
{
    [System.Serializable]
    public class AttributeBlock : ScriptableObject
    {
        public Attribute strength; // Determines melee damage and weight capacity.
        public Attribute agility; //Determines gun accuracy and damage.
        public Attribute endurance; //Determines bonus stamina, bonus health, corrosion/poison mitigation, and intimidation.

        public Attribute wits; //How smart and perseptive a character is. Explosive damage, research + crafting speed, Tracking(?).
        public Attribute perseverance; // CC/status resist. if low perseverance might run away in fear OR frozen in fear or reluctant to advance (slow).
    }
}