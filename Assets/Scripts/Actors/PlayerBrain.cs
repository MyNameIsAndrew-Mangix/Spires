using UnityEngine;
using Spire.Core;

namespace Spire.Actors
{
    public class PlayerBrain : CharacterBrain
    {
        private IActorMover _playerMover;
        public override void Tick()
        {
            if (!TimeState.gameIsPaused)
            {
                _playerMover.Move();
            }

        }

        public void GetMovingActor(IActorMover actorPhysics)
        {
            _playerMover = actorPhysics;
        }
    }
}