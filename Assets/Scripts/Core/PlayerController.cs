using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spire.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _bullet;

        void Start()
        {

        }


        void Update()
        {
        }

        private void Fire()
        {
            Instantiate(_bullet, transform.position, transform.rotation);
        }
    }
}