using UnityEngine;
using Spire.Stats;

namespace Spire.Actors
{
    public class SquadMember : MonoBehaviour
    {
        [SerializeField]
        private StatBlock _statBlock;
        // [SerializeField]
        // private PlayerBrain _playerBrain;
        // [SerializeField]
        // private AIBrain _aIBrain;

        private CharacterBrain _brain;

        [SerializeField]
        private bool _isPlayer = false;
        [SerializeField]
        private Sprite _sprite;

        public bool isPlayer
        {
            get { return _isPlayer; }
        }

        // This is the same as public Statblock statBlock { get { return _statBlock; } }
        public StatBlock statBlock => _statBlock;
        public Sprite Sprite => _sprite;

        public CharacterBrain brain => _brain;

        //Make a base class called Character that all characters will inherit from. Will have a stat block, sprite, maybe some other stuff like AI brain.

        private void Awake()
        {
            if (!_isPlayer)
            {
                _brain = new AIBrain();
            }
            else
            {
                _brain = new PlayerBrain();
            }
        }
        void Start()
        {
            if (_statBlock.name != null)
                this.name = _statBlock.name;
        }

        // Update is called once per frame
        void Update()
        {
            _brain.Tick();
        }

        //enables PC control && disables AI control
        public void UsePlayerBrain()
        {
            _brain = new PlayerBrain();
            _isPlayer = true;
        }
        //enables AI control && disables PC control
        public void UseAIBrain()
        {
            _brain = new AIBrain();
            _isPlayer = false;
        }
    }
}
