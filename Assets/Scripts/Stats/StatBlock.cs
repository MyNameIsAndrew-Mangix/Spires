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
        [SerializeField] private AttributeBlock _attributeBlock;
        //STR MODIFIED   
        [SerializeField] private Stat _carryWeight;
        [SerializeField] private Stat _meleeDamage;
        //AGI MODIFIED
        private float _spreadFactor;
        [SerializeField] private float _bulletSpread;
        [SerializeField] private Stat _gunDamage;
        //END MODIFIED
        [SerializeField] private Stat _poisonResist;
        [SerializeField] private Stat _intimidation;
        [SerializeField] private Stat _defense;
        //WITS MODIFIED
        [SerializeField] private Stat _specialDamage;
        [SerializeField] private Stat _researchSpeed;
        [SerializeField] private Stat _craftingSpeed;
        //PER MODIFIED
        [SerializeField] private Stat _ccResist;

        public Stat carryWeight { get => _carryWeight; }
        public Stat meleeDamage { get => _meleeDamage; }
        public float bulletSpread { get => _bulletSpread; }
        public Stat gunDamage { get => _gunDamage; }
        public Stat poisonResist { get => _poisonResist; }
        public Stat intimidation { get => _intimidation; }      //works against perseverance for fear mechanic and target priority.
        public Stat defense { get => _defense; }
        public Stat specialDamage { get => _specialDamage; }
        public Stat researchSpeed { get => _researchSpeed; }
        public Stat craftingSpeed { get => _craftingSpeed; }
        public Stat ccResist { get => _ccResist; }

        public void CalcBaseStats()
        {
            _carryWeight.SetBaseValue(_attributeBlock.strength.currentValue * 2);
            _meleeDamage.SetBaseValue(_attributeBlock.strength.currentValue * 3);
            _spreadFactor = 10 - (_attributeBlock.agility.currentValue * 0.15f); //for guns, make a method that takes in _bulletSpread and does math based on the gun's accuracy.
            _bulletSpread = Random.Range(-_spreadFactor, _spreadFactor);
            _gunDamage.SetBaseValue(_attributeBlock.agility.currentValue * 3);
            _poisonResist.SetBaseValue(0.1f + _attributeBlock.endurance.currentValue * 0.03f);
            _intimidation.SetBaseValue(_attributeBlock.endurance.currentValue * 0.5f);
            _defense.SetBaseValue(_attributeBlock.endurance.currentValue * 2);
            _specialDamage.SetBaseValue(_attributeBlock.wits.currentValue * 3);
            _researchSpeed.SetBaseValue(_attributeBlock.wits.currentValue * 5);
            _craftingSpeed.SetBaseValue(_attributeBlock.wits.currentValue * 5);
            _ccResist.SetBaseValue(0.15f - _attributeBlock.perseverance.currentValue * 0.05f);

        }
        //CRITS WILL BE DETERMINED BY THE AMOUNT OF KNOWLEDGE OF AN ENEMY, WITH A SOFT CAP TO AVOID GAME-BREAKING CRITS.
    }
}