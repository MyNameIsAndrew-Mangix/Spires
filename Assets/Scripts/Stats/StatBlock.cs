﻿using UnityEngine;
using Lowscope.Saving;

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
    public class StatBlock : MonoBehaviour, ISaveable
    {
        [SerializeField] private AttributeBlock _attributeBlock;
        //STR MODIFIED   
        [SerializeField] private Stat _carryWeight = new Stat();
        [SerializeField] private Stat _meleeDamage = new Stat();
        //AGI MODIFIED
        private float _spreadFactor;
        [SerializeField] private float _bulletSpread;
        [SerializeField] private Stat _gunDamage = new Stat();
        //END MODIFIED
        [SerializeField] private Stat _poisonResist = new Stat();
        [SerializeField] private Stat _intimidation = new Stat();
        [SerializeField] private Stat _defense = new Stat();
        //WITS MODIFIED
        [SerializeField] private Stat _specialDamage = new Stat();
        [SerializeField] private Stat _researchSpeed = new Stat();
        [SerializeField] private Stat _craftingSpeed = new Stat();
        //PER MODIFIED
        [SerializeField] private Stat _ccResist = new Stat();

        public AttributeBlock attributeBlock { get => _attributeBlock; }
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

        private void Start()
        {
            if (!HasMoreAttributesThanBase(_attributeBlock))
                _attributeBlock.AssignBaseValues();
            CalcBaseStats();
        }

        private bool HasMoreAttributesThanBase(AttributeBlock attributeBlock)
        {
            if (_attributeBlock.AttributeSum() > _attributeBlock.BaseAttributeSum())
                return true;
            else
                return false;
        }

        public void CalcBaseStats()
        {
            _carryWeight.SetBaseValue(_attributeBlock.strength.baseValue * 2);
            _meleeDamage.SetBaseValue(_attributeBlock.strength.baseValue * 3);
            _spreadFactor = 10 - (_attributeBlock.agility.baseValue * 0.15f); //for guns, make a method that takes in _bulletSpread and does math based on the gun's accuracy.
            _bulletSpread = Random.Range(-_spreadFactor, _spreadFactor);
            _gunDamage.SetBaseValue(_attributeBlock.agility.baseValue * 3);
            _poisonResist.SetBaseValue(0.1f + _attributeBlock.endurance.baseValue * 0.03f);
            _intimidation.SetBaseValue(_attributeBlock.endurance.baseValue * 0.5f);
            _defense.SetBaseValue(_attributeBlock.endurance.baseValue * 2);
            _specialDamage.SetBaseValue(_attributeBlock.wits.baseValue * 3);
            _researchSpeed.SetBaseValue(_attributeBlock.wits.baseValue * 5);
            _craftingSpeed.SetBaseValue(_attributeBlock.wits.baseValue * 5);
            _ccResist.SetBaseValue(0.15f - _attributeBlock.perseverance.baseValue * 0.05f);
        }

        public string OnSave()
        {
            return JsonUtility.ToJson(_attributeBlock);
        }

        public void OnLoad(string data)
        {
            _attributeBlock = JsonUtility.FromJson<AttributeBlock>(data);
        }

        public bool OnSaveCondition()
        {
            return true;
        }
        //CRITS WILL BE DETERMINED BY THE AMOUNT OF KNOWLEDGE OF AN ENEMY, WITH A SOFT CAP TO AVOID GAME-BREAKING CRITS.
    }
}