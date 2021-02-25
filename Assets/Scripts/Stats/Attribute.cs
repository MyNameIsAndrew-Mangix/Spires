using UnityEngine;
namespace Spire.Stats
{
    [System.Serializable]
    public enum AttributeType
    {
        Strength = 0,
        Agility = 1,
        Endurance = 2,
        Wits = 3,
        Perseverance = 4
    }
    [System.Serializable]
    public class Attribute
    {
        private bool _hasConfirmed = false;
        [SerializeField] private AttributeType _attributeType;
        [SerializeField] private int _baseValue = 5;
        [SerializeField] private int _curValue;
        [SerializeField] private int _tempValue;


        public int baseValue { get => _baseValue; }
        public int currentValue { get => _curValue; }
        public int tempValue { get => _tempValue; }
        public bool hasConfirmed { get => _hasConfirmed; }
        public AttributeType attributeType { get => _attributeType; }

        // public Attribute(int baseValue, bool hasConfirmed, AttributeType type)
        // {
        //     this._baseValue = baseValue;
        //     this._hasConfirmed = hasConfirmed;
        //     this._attributeType = type;
        // }

        // public Attribute(int baseValue, AttributeType type) : this(baseValue, false, type) { }
        // public Attribute(AttributeType type) : this(5, false, type) { }

        public void IncreaseAttribute()
        {
            if (_tempValue < _baseValue)
                _tempValue = _baseValue;

            if (!_hasConfirmed)
                _tempValue++;

            if (hasConfirmed)
            {
                _curValue = tempValue;
                _hasConfirmed = false;
            }
        }

        public void DecreaseTempAttribute()
        {
            if (!hasConfirmed && _tempValue > _curValue)
                _tempValue--;
        }

        public void ConfirmAttributeChanges()
        {
            _hasConfirmed = true;
            IncreaseAttribute();
        }
    }
}