using Spire.Core;

namespace Spire.Actors
{
    public class PlayerBrain : CharacterBrain
    {
        public override void Tick()
        {
            if (!TimeState.gameIsPaused)
            {
                
                base.iActorMover.Move();
            }
        }

        public override void GetMovingActor(IActorMover actorMover)
        {
            base.iActorMover = actorMover;
        }
    }
}