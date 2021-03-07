using UnityEngine;
using Spire.Stats;
using Spire.Resources;

namespace Spire.Actors
{
    public class SquadMember : MonoBehaviour
    {

        private CharacterBrain _brain;
        [SerializeField] private string _name;
        [SerializeField] private StatBlock _statBlock;
        [SerializeField] private int _memberId;
        [SerializeField] private bool _isPlayer = false;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Health _health;

        // This is the same as public bool isPLayer { get { return _isPlayer; } }
        public int memberId => _memberId;
        public bool isPlayer => _isPlayer;
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

        private void OnEnable()
        {
            _memberId = GetInstanceID();
        }
        void Start()
        {
            if (_name != null)
                this.name = _name;
            if (_statBlock == null)
                Debug.LogError("_statblock is NULL");

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
