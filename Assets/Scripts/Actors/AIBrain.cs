using UnityEngine;

namespace Spire.Actors
{
    public class AIBrain : CharacterBrain
    {
        public override void GetMovingActor(IActorMover actorMover)
        {
            base.iActorMover = actorMover;
        }

        public override void Tick()
        {
            //_aIMover.Move();
        }
    }
}