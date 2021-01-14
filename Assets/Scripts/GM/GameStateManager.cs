using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spire.GM
{
    public enum GameTimeState
    {
        Realtime,
        Halftime,
        Pause
    }
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField]
        private GameTimeState _gameTimeState;
        private float _DeltaTime;
        private void Awake()
        {
            //cache the deltatime;
            _DeltaTime = Time.fixedDeltaTime;
        }
    }
}