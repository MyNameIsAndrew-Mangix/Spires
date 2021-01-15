using UnityEngine;

namespace Spire.Core
{
    public class PlayerBrain : CharacterBrain
    {
        private IActorMover _playerMover;
        public override void Tick()
        {
            _playerMover.Move();
        }

        public void GetMovingActor(IActorMover actorPhysics)
        {
            _playerMover = actorPhysics;
        }
    }
}