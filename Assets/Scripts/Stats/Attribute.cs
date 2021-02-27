using UnityEngine;
namespace Spire.Stats
{
    [System.Serializable]
    public class Attribute
    {
        private bool _hasConfirmed = false;
        private bool _hasBaseSet = false;
        [SerializeField] private int _baseValue = 5;
        [SerializeField] private int _curValue;
        [SerializeField] private int _tempValue;


        public int baseValue { get => _baseValue; }
        public int currentValue
        {
            get
            {
                if (_curValue < _baseValue)
                    return _baseValue;
                else
                    return _curValue;
            }
        }

        public int tempValue { get => _tempValue; }
        public bool hasConfirmed { get => _hasConfirmed; }

        public void SetBaseValue(int i)
        {
            if (!_hasBaseSet)
            {
                _baseValue = i;
                _hasBaseSet = true;
            }

        }

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