using UnityEngine;

namespace Spire.Actors
{
    public class AIBrain : CharacterBrain
    {
        [SerializeField]
        private IActorMover _aIMover;
        public override void Tick()
        {
            //_aIMover.Move();
        }
    }
}