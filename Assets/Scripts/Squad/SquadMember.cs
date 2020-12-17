using System;
using Spire.Core;
using UnityEngine;

namespace Spire.Squad
{
    public class SquadMember : MonoBehaviour
    {
        [SerializeField]
        private StatBlock _baseSquadMember;

        private PlayerBrain _playerBrain;
        private AIBrain _aIBrain;

        private bool _isPlayer = false;

        public bool IsPlayer
        {
            get { return _isPlayer; }
        }
        // Start is called before the first frame update
        void Start()
        {
            //Initialization
            _aIBrain = GetComponent<AIBrain>();
            _playerBrain = GetComponent<PlayerBrain>();
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
        public void PlayerControlled()
        {
            _aIBrain.enabled = false;
            _playerBrain.enabled = true;
            _isPlayer = true;
        }
        //enables AI control && disables PC control
        public void AIControlled()
        {
            _playerBrain.enabled = false;
            _isPlayer = false;
            _aIBrain.enabled = true;
        }
    }
}
