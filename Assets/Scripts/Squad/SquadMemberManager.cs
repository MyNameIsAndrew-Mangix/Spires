using System.Collections.Generic;
using UnityEngine;
using Spire.GM;

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
        public int PlayerControlledID { get => _playerControlledID; }
        // Start is called before the first frame update
        void Start()
        {
            foreach (Transform child in transform)
            {
                _squadMembers.Add(child.gameObject.GetComponent<SquadMember>());
            }
            for (int i = 0; i < _squadMembers.Count; i++)
            {
                _squadMemberIDs.Add(_squadMembers[i].statBlock.memberId);
            }
            _playerControlledID = _squadMembers.Find(SquadMember => SquadMember.isPlayer == true).statBlock.memberId;
        }

        public void SwapControl(int originalId, int targetId)
        {
            //finds current PC and target NPSM from list
            SquadMember originalSquadM = FindSquadMember(originalId);
            SquadMember swapTarget = FindSquadMember(targetId);
            //current PC becomes NPSM. Camera follow target gets updated to NPSM getting swapped to PC.
            originalSquadM.UseAIBrain();
            _cameraFollow.UpdateFollowTarget(swapTarget.transform);
            swapTarget.UsePlayerBrain();
            _playerControlledID = swapTarget.statBlock.memberId;
        }

        //Finds and returns current PC.
        public SquadMember FindSquadMember(int id)
        {   //finds squad member via ID
            SquadMember quaesitum = _squadMembers.Find(SquadMember => SquadMember.statBlock.memberId == id);
            //if squad member isn't found (i.e. is NULL, doesn't exist) return null, else return squad member.
            if (quaesitum == null)
            {
                Debug.LogError("quaesitum is NULL");
                return null;
            }
            else
                return quaesitum;
        }
    }
}