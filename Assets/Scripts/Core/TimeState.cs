using UnityEngine;

namespace Spire.Core
{
    public enum TimeSpeed
    {
        Realtime,
        Halftime,
        Pause
    }
    public class TimeState : MonoBehaviour
    {
        private static bool _gameIsPaused;
        private static TimeSpeed _timeSpeed;
        private static TimeSpeed _previousTimeSpeed;
        public static bool gameIsPaused { get => _gameIsPaused; }
        public static TimeSpeed TimeSpeed { get => _timeSpeed; }

        public static void SetRealtime()
        {
            _previousTimeSpeed = _timeSpeed;
            _timeSpeed = TimeSpeed.Realtime;
            _gameIsPaused = false;
            Time.timeScale = 1f;
        }
        public static void SetHalfTime()
        {
            _previousTimeSpeed = _timeSpeed;
            _timeSpeed = TimeSpeed.Halftime;
            _gameIsPaused = false;
            Time.timeScale = 0.5f;
        }
        public static void SetPause()
        {
            if (_timeSpeed == TimeSpeed.Pause)
                Resume();
            else
            {
                _timeSpeed = TimeSpeed.Pause;
                _gameIsPaused = true;
                Time.timeScale = 0f;
            }
        }

        public static void Resume()
        {
            _timeSpeed = _previousTimeSpeed;
            if (_timeSpeed == TimeSpeed.Realtime)
                SetRealtime();
            else
                SetHalfTime();
        }
    }
}