using UnityEngine;

namespace Spire.Actors
{
    public abstract class CharacterBrain
    {
        protected IActorMover iActorMover;
        public abstract void Tick();
        public abstract void GetMovingActor(IActorMover actorMover);
    }
}