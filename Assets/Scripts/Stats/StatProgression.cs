using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spire.Stats
{
    public enum CharacterType
    {
        // Squad members will be referred to as PlayerX and TraitorX until given names.
        Player1,
        Player2,
        Player3,
        Player4,
        Traitor1,
        GenericNPC,
        GenericWeakEnemy,
        GenericMediumEnemy,
        GenericStrongEnemy,
    }

    [CreateAssetMenu(fileName = "StatProgression", menuName = "Stats/New Progression", order = 0)]
    public class StatProgression : ScriptableObject
    {
        [SerializeField] CharacterProgression[] CharacterProgressions;

        [System.Serializable]
        class CharacterProgression
        {
            public CharacterType characterType;
            public float[] baseHealth;
            public float[] baseStamina;
            //public 
        }
    }
}