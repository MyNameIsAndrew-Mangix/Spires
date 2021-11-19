using UnityEngine;

namespace Spire.Actors
{
    public class SquadMember : MonoBehaviour
    {
        private CharacterBrain _brain;
        [SerializeField] private string _name;
        [SerializeField] private bool _isPlayer;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Sprite _menuIcon;
        // This is the same as public bool isPLayer { get { return _isPlayer; } }
        public bool isPlayer => _isPlayer;
        public CharacterBrain brain => _brain;
        public Sprite sprite => _sprite;
        public Sprite menuIcon => _menuIcon;

        private void Awake()
        {
            if (!_isPlayer)
                _brain = new AIBrain();
            else
                _brain = new PlayerBrain();
        }

        private void Start()
        {
            if (_name != null)
                this.name = _name;
            if (_sprite == null)
                Debug.LogError("Squad Member Sprite is NULL!");
            if (_menuIcon == null)
                Debug.LogError("Squad Member Menu Icon is NULL!");
        }
        private void Update()
        {
            _brain.Tick();
        }

        public void UsePlayerBrain()
        {
            _brain = new PlayerBrain();
            _isPlayer = true;
        }

        public void UseAIBrain()
        {
            _brain = new AIBrain();
            _isPlayer = false;
        }
    }
}