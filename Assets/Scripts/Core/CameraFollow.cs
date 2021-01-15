using UnityEngine;

namespace Spire.Core
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform _player;
        [SerializeField]
        private Vector3 _offset; //will add offset functionality when "aiming" guns
        void Update()
        {
            transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
        }

        public void UpdateFollowTarget(Transform transform)
        {
            _player = transform;
        }
        //         digraph ScriptDependencies {
        //     size="6,6";
        //     node [color=lightblue2, style=filled];
        //     "Control/Input" -> "Movement";
        // "Control/Input" -> "Combat";
        // "Control/Input" -> "Core";
        //     "UI" -> "Combat";
        // "UI" -> "Inventories";
        // "UI" -> "Stats";
        // "UI" -> "Dialogue";
        // "Dialogue" -> "Questing";
        // "Inventories" -> "Saving";
        // "Inventories" -> "Stats";
        // "Inventories" -> "Technology";
        // "Combat" -> "Inventories";
        // "Combat" -> "Stats";
        // "Combat" -> "Saving";
        // "Combat" -> "Core";
        //     "Movement" -> "Core";
        //     "Movement" -> "Saving";
        //     "Movement" -> "Inventories";
        // "Questing" -> "Saving";
        // "Technology" -> "Saving";
        // }
    }
}