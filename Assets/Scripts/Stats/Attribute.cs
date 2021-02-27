using UnityEngine;
namespace Spire.Stats
{
    [System.Serializable]
    public class Attribute
    {
        private bool _hasConfirmed = false;
        [SerializeField] private int _baseValue = 5;
        [SerializeField] private int _curValue;
        [SerializeField] private int _tempValue;


        public int baseValue { get => _baseValue; }
        public int currentValue { get => _curValue; }
        public int tempValue { get => _tempValue; }
        public bool hasConfirmed { get => _hasConfirmed; }

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