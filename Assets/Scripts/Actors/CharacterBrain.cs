using UnityEngine;

namespace Spire.Actors
{
    [System.Serializable]
    public abstract class CharacterBrain
    {
        protected IActorMover iActorMover;
        public abstract void Tick();
        public abstract void GetMovingActor(IActorMover actorMover);
    }
}