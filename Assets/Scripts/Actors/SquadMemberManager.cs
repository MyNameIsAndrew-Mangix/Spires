using UnityEngine;
using Spire.Core;
using System.Collections.Generic;
using System;
namespace Spire.Actors
{
    public class SquadMemberManager : MonoBehaviour
    {
        [SerializeField] private CameraFollow _cameraFollow;
        private SquadMember _playerControlledMem;
        [SerializeField] private List<SquadMember> _squadMembers = new List<SquadMember>();
        public static Action OnControlSwap;
        public List<SquadMember> squadMembers { get => _squadMembers; }
        public SquadMember playerControlledMem { get => _playerControlledMem; }

        // Start is called before the first frame update
        void Start()
        {
            foreach (Transform child in transform)
            {
                _squadMembers.Add(child.gameObject.GetComponent<SquadMember>());
            }
            _playerControlledMem = _squadMembers.Find(SquadMember => SquadMember.isPlayer == true);
        }

        public void SwapControl(SquadMember curPlayerControlled, SquadMember tarToControl)
        {
            curPlayerControlled.UseAIBrain();
            tarToControl.UsePlayerBrain();
            _playerControlledMem = tarToControl;
            OnControlSwap();

        }

        public SquadMember FindSquadMember(SquadMember memToFind)
        {
            SquadMember quaesitum = _squadMembers.Find(SquadMember => SquadMember == memToFind);
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