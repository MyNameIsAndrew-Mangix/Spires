using System;
using Spire.Core;
using UnityEngine;

namespace Spire.Squad
{
    public class SquadMember : MonoBehaviour
    {
        [SerializeField]
        private StatBlock _statBlock;
        [SerializeField]
        private PlayerBrain _playerBrain;
        [SerializeField]
        private AIBrain _aIBrain;

        [SerializeField]
        private bool _isPlayer = false;
        [SerializeField]
        private Sprite _sprite;

        public bool isPlayer
        {
            get { return _isPlayer; }
        }

        public StatBlock statBlock { get => _statBlock; }
        public Sprite Sprite { get => _sprite; }

        //Make a base class called Character that all characters will inherit from. Will have a stat block, sprite, maybe some other stuff like AI brain.

        // Start is called before the first frame update
        void Start()
        {
            if (_statBlock.name != null)
                this.name = _statBlock.name;
            if (!_isPlayer)
            {
                _aIBrain.enabled = true;
                _playerBrain.enabled = false;
            }
            else
            {
                _aIBrain.enabled = false;
                _playerBrain.enabled = true;
            }

            #region NULL CHECK
            if (_aIBrain == null)
                Debug.LogError("_aIBrain is NULL");
            if (_playerBrain == null)
                Debug.LogError("_playerBrain is NULL");
            #endregion
        }

        // Update is called once per frame
        void Update()
        {
            //if this isn't PC, uses AI logic. else uses PC logic.
            if (!_isPlayer)
            {
                _aIBrain.Tick();
            }
            else
                _playerBrain.Tick();
        }

        //enables PC control && disables AI control
        public void UsePlayerBrain()
        {
            _aIBrain.enabled = false;
            _playerBrain.enabled = true;
            _isPlayer = true;
        }
        //enables AI control && disables PC control
        public void UseAIBrain()
        {
            _playerBrain.enabled = false;
            _aIBrain.enabled = true;
            _isPlayer = false;
        }
    }
}
