using UnityEngine;
using Spire.Core;

namespace Spire.Squad
{

    public class SquadMemberSwap : MonoBehaviour, IBulletTime
    {
        private float _fixedDeltaTime;

        /// <summary>
        /// When button is held down, do the following:
        /// 1. Slow down time
        /// 2. Open a radial menu.
        /// 3. If squad member selected isn't selected already, swap player control.
        /// </summary>
        /// for squad member if check:
        /// 1. figure out which squad member I am
        /// 2. check if trying to select same member
        /// 3. if selected member is player controlled, do nothing.
        /// 4. if selected member is not player controlled, body swap.
        private void Awake()
        {
            _fixedDeltaTime = Time.fixedDeltaTime;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void TimeSlowDown()
        {
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.4f;
            }
        }

        public void TimeSpeedUp()
        {
            if (Time.timeScale == 0.4f)
            {
                Time.timeScale = 1.0f;
            }
            Time.fixedDeltaTime = _fixedDeltaTime * Time.timeScale;
        }
    }
}