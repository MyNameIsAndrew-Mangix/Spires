using Spire.Core;
using UnityEngine;

namespace Spire.Squad
{
    public class SquadMember : MonoBehaviour
    {
        [SerializeField]
        private BaseSquadMember _baseSquadMember;

        private bool _isPlayer = false;

        public bool IsPlayer
        {
            get { return _isPlayer; }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void PlayerControlled()
        {
            _isPlayer = true;
        }
    }
}
