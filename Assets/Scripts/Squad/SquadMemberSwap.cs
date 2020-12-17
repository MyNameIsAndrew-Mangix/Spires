using UnityEngine;
using Spire.Core;
using System.Collections.Generic;
using Spire.Squad;

namespace Spire.GM
{

    public class SquadMemberSwap : MonoBehaviour, IBulletTime
    {
        [SerializeField]
        private CameraFollow _cameraFollow;
        //caching starting deltatime
        private float _fixedDeltaTime;
        private List<SquadMember> _squadMembers = new List<SquadMember>();

        private int _playerControlledID;

        public List<SquadMember> SquadMemebers { get => _squadMembers; }

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
        private void Awake()
        {
            _fixedDeltaTime = Time.fixedDeltaTime;
        }
        private void Start()
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
            {
                _squadMembers.Add(GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<SquadMember>());
            }
            _playerControlledID = _squadMembers.Find(SquadMember => SquadMember.IsPlayer == true).StatBlock.memberId;
        }

        public void SwapControl(int originalId, int targetId)
        {
            //finds current PC and target NPSM from list
            SquadMember originalSquadM = _squadMembers.Find(SquadMember => SquadMember.StatBlock.memberId == originalId);
            SquadMember swapTarget = _squadMembers.Find(SquadMember => SquadMember.StatBlock.memberId == targetId);
            //current PC becomes NPSM. Camera follow target gets updated to NPSM getting swapped to PC.
            originalSquadM.UseAIBrain();
            _cameraFollow.UpdateFollowTarget(swapTarget.transform);
            swapTarget.UsePlayerBrain();
        }

        public void TimeSlowDown()
        {
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.4f;
            }
        }

        public void TimeSpeedUp()
        {
            if (Time.timeScale == 0.4f)
            {
                Time.timeScale = 1.0f;
            }
            Time.fixedDeltaTime = _fixedDeltaTime * Time.timeScale;
        }
    }
}