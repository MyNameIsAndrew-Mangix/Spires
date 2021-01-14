using UnityEngine;
using UnityEngine.InputSystem;

namespace Spire.Core
{
    public class CharacterMovement : MonoBehaviour
    {
        //[SerializeField]
        //private InputAction _moveAction;
        private float _speed = 5f;
        private Vector2 _dir;

        public void CalcDir(InputAction.CallbackContext context)
        {
            _dir = context.ReadValue<Vector2>();
        }

        public void PlayerMove()
        {
            transform.Translate(_dir * _speed * Time.deltaTime);
        }

        public void AIMove()
        {
            throw new System.NotImplementedException();
        }
    }

}
