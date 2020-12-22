using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spire.Squad
{
    public class SquadMemberManager : MonoBehaviour
    {
        /// <summary>
        /// When button is held down, do the following:
        /// 1. Slow down time
        /// 2. Open a radial menu.
        /// 3. If squad member selected isn't selected already, swap player control.
        /// </summary>
        /// for squad member if check:
        /// 1. figure out which squad member I am
        /// 2. check if trying to select same member
        /// 3. if selected member is player controlled, do nothing.
        /// 4. if selected member is not player controlled, body swap.
        /// 
        [SerializeField]
        private CameraFollow _cameraFollow;
        //private float _fixedDeltaTime;
        private int _playerControlledID;

        private List<SquadMember> _squadMembers = new List<SquadMember>();
        private List<int> _squadMemberIDs = new List<int>();

        public List<SquadMember> squadMembers { get => _squadMembers; }
        public List<int> squadMemberIDs { get => _squadMemberIDs; }

        //public List<SquadMember> SquadMemebers { get => _squadMembers; }
        // Start is called before the first frame update

        // private void Awake()
        // {
        //     _fixedDeltaTime = Time.fixedDeltaTime;
        // }
        void Start()
        {
            foreach (Transform child in transform)
            {
                _squadMembers.Add(child.gameObject.GetComponent<SquadMember>());
            }
            _playerControlledID = _squadMembers.Find(SquadMember => SquadMember.isPlayer == true).statBlock.memberId;
        }

        public void SwapControl(int originalId, int targetId)
        {
            //finds current PC and target NPSM from list
            SquadMember originalSquadM = _squadMembers.Find(SquadMember => SquadMember.statBlock.memberId == originalId);
            SquadMember swapTarget = _squadMembers.Find(SquadMember => SquadMember.statBlock.memberId == targetId);
            //current PC becomes NPSM. Camera follow target gets updated to NPSM getting swapped to PC.
            originalSquadM.UseAIBrain();
            _cameraFollow.UpdateFollowTarget(swapTarget.transform);
            swapTarget.UsePlayerBrain();
        }
    }
}