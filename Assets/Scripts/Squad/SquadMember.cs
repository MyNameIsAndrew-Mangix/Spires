using System;
using Spire.Core;
using UnityEngine;

namespace Spire.Squad
{
    public class SquadMember : MonoBehaviour
    {
        [SerializeField]
        private StatBlock statBlock;
        [SerializeField]
        private PlayerBrain _playerBrain;
        [SerializeField]
        private AIBrain _aIBrain;

        private bool _isPlayer = false;

        public bool IsPlayer
        {
            get { return _isPlayer; }
        }

        public StatBlock StatBlock { get => statBlock; }

        // Start is called before the first frame update
        void Start()
        {
            if (statBlock.name != null)
                this.name = statBlock.name;
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
            _isPlayer = false;
            _aIBrain.enabled = true;
        }
    }
}
