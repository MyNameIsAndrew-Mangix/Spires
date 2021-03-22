using UnityEngine;

namespace Spire.Stats
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewBaseAttributeBlock", menuName = "Stats/Base Attribute Block")]
    public class BaseAttributesSO : ScriptableObject
    {
        public int baseStrength; // Determines melee damage and weight capacity.
        public int baseAgility; //Determines gun accuracy and damage.
        public int baseEndurance; //Determines bonus stamina, bonus health, corrosion/poison mitigation, and intimidation.
        public int baseWits; //How smart and perseptive a character is. Explosive damage, research + crafting speed, Tracking(?).
        public int basePerseverance; // CC/status resist. if low perseverance might run away in fear OR frozen in fear or reluctant to advance (slow).
    }
}