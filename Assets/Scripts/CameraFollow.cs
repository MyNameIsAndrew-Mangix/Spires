using UnityEngine;

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
}
