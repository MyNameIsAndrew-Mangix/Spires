using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spire.Inventory;

namespace Spire.Combat
{ 
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField]
        private int _bulletSpeed;
        // Update is called once per frame
        void Update()
        {
            transform.position += transform.up * Time.deltaTime * _bulletSpeed;
        }
    }
}