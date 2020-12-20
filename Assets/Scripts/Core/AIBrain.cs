using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spire.Movement;

namespace Spire.Core
{
    public class AIBrain : CharacterBrain
    {
        [SerializeField]
        private CharacterMovement _characterMovement;
        public override void Tick()
        {
            //_characterMovement.AIMove();
        }
    }
}