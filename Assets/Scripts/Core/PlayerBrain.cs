using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spire.Movement;

namespace Spire.Core
{
    public class PlayerBrain : CharacterBrain
    {
        [SerializeField]
        private CharacterMovement _characterMovement;
        public override void Tick()
        {
            _characterMovement.PlayerMove();
        }
    }
}