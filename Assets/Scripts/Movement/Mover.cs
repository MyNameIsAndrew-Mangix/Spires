using UnityEngine;
using Spire.Core;
using UnityEngine.InputSystem;

namespace Spire.Movement
{
    public abstract class Mover : MonoBehaviour, IActorMover
    {
        public virtual void CalcDir()
        {

        }
        public virtual void CalcDir(InputAction.CallbackContext context)
        {

        }
        public abstract void Move();
    }
}