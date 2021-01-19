using UnityEngine;
using Spire.Actors;
using UnityEngine.InputSystem;

namespace Spire.Movement
{
    public class PlayerMovement : Mover
    {
        private float _speed = 5f; //placeholder until stats are finished. will get speed from there.
        private Vector2 _dir;

        [SerializeField]
        private PlayerBrain _playerBrain;

        private void Start()
        {
            _playerBrain.GetMovingActor(this);
        }
        public override void CalcDir(InputAction.CallbackContext context)
        {
            _dir = context.ReadValue<Vector2>();
        }

        public override void Move()
        {
            transform.Translate(_dir * _speed * Time.deltaTime);
        }

    }
}