using UnityEngine;
using Spire.Actors;
using Spire.Core;
using UnityEngine.InputSystem;

namespace Spire.Movement
{
    public class PlayerMovement : Mover
    {
        private float _speed = 5f; //placeholder until stats are finished. will get speed from there.
        private Vector2 _dir;

        [SerializeField]
        private CharacterBrain _playerBrain;

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        void OnEnable()
        {
            SquadMemberManager.OnControlSwap += UpdateBrain;
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        void OnDisable()
        {
            SquadMemberManager.OnControlSwap -= UpdateBrain;
        }
    
        private void Start()
        {
            UpdateBrain();
        }
        public override void CalcDir(InputAction.CallbackContext context)
        {
            _dir = context.ReadValue<Vector2>();
        }

        private void UpdateBrain()
        {
            _playerBrain = GetComponent<SquadMember>().brain;
            _playerBrain.GetMovingActor(this);
        }

        public override void Move()
        {
            if (!TimeState.gameIsPaused)
                transform.Translate(_dir * _speed * Time.deltaTime);
        }

    }
}