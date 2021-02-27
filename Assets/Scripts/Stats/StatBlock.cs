using UnityEngine;

namespace Spire.Stats
{
    public enum EnemyType
    {
        Bipedal_Screecher,
        Quadrapedal_Harasser,
        Quadralpedal_Hunter,
        Hexapedal_Sentinel,
        BOSS
        //more later?
    }
    public enum characterState
    {
        Roaming,
        Ambush,
        Combat,
        Dead
    }
    public enum characterCombatState
    {
        Slowed,
        Stunned,
        Feared,
        Exhausted
    }

    [System.Serializable]
    public class StatBlock
    {
        //STR MODIFIED   
        [SerializeField] private Stat _carryWeight;
        [SerializeField] private Stat _meleeDamage;
        //AGI MODIFIED
        [SerializeField] private Stat _bulletSpread;
        [SerializeField] private Stat _gunDamage;
        //END MODIFIED
        [SerializeField] private Stat _poisonResist;
        [SerializeField] private Stat _intimidation;
        //WITS MODIFIED
        [SerializeField] private Stat _explosiveDamage;
        [SerializeField] private Stat _researchSpeed;
        [SerializeField] private Stat _craftingSpeed;
        //PER MODIFIED
        [SerializeField] private Stat _ccResist;

        [SerializeField] private AttributeBlock _attributeBlock;

        public Stat carryWeight { get => _carryWeight; }
        public Stat meleeDamage { get => _meleeDamage; }
        public Stat bulletSpread { get => _bulletSpread; }
        public Stat gunDamage { get => _gunDamage; }
        public Stat poisonResist { get => _poisonResist; }
        public Stat intimidation { get => _intimidation; }      //works against perseverance for fear mechanic.
        public Stat explosiveDamage { get => _explosiveDamage; }
        public Stat researchSpeed { get => _researchSpeed; }
        public Stat craftingSpeed { get => _craftingSpeed; }
        public Stat ccResist { get => _ccResist; }

        //CRITS WILL BE DETERMINED BY THE AMOUNT OF KNOWLEDGE OF AN ENEMY, WITH A SOFT CAP TO AVOID GAME-BREAKING CRITS.
    }
}