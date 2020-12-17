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

        }
    }
}
