using UnityEngine;
using Lowscope.Saving;

namespace Spire.Actors
{
    [System.Serializable]
    public class SquadMember : MonoBehaviour, ISaveable
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

        public struct SquadMemData
        {
            public string Name;
            public bool IsPlayer;
            public Sprite Sprite;
            public Sprite MenuIcon;
            public CharacterBrain Brain;
            public SquadMemData(string name, bool isPlayer, Sprite sprite, Sprite menuIcon, CharacterBrain brain)
            {
                Name = name;
                IsPlayer = isPlayer;
                Sprite = sprite;
                MenuIcon = menuIcon;
                Brain = brain;
            }
        }

        public string OnSave()
        {
            SquadMemData memData = new SquadMemData(_name, _isPlayer, _sprite, _menuIcon, _brain);
            return JsonUtility.ToJson(memData);
        }

        public void OnLoad(string data)
        {
            SquadMemData memData = JsonUtility.FromJson<SquadMemData>(data);
            _name = memData.Name;
            _isPlayer = memData.IsPlayer;
            _sprite = memData.Sprite;
            _menuIcon = memData.MenuIcon;
            _brain = memData.Brain;
        }

        public bool OnSaveCondition()
        {
            return true;
        }
    }
}