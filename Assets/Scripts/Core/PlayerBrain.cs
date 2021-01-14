using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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