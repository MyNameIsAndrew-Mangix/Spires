using UnityEngine;

namespace Spire.Core
{
    public enum GameTimeState
    {
        Realtime,
        Halftime,
        Pause
    }
    public static class TimeState
    {
        public static bool gameIsPaused;
        [SerializeField]
        private static GameTimeState _gameTimeState;

        public static void SetRealtime()
        {
            _gameTimeState = GameTimeState.Realtime;
            gameIsPaused = false;
            Time.timeScale = 1f;
        }
        public static void SetHalfTime()
        {
            _gameTimeState = GameTimeState.Halftime;
            gameIsPaused = false;
            Time.timeScale = 0.5f;
        }
        public static void SetPause()
        {
            _gameTimeState = GameTimeState.Pause;
            gameIsPaused = true;
            Time.timeScale = 0f;
        }
    }
}