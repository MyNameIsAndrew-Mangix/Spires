using UnityEngine;
using UnityEngine.InputSystem;

namespace Spire.UI
{
    public class LazyRadial : MonoBehaviour
    {
        [SerializeField]
        private GameObject _radialGO;
        [SerializeField]
        private RadialMenu _radialMenu;
        private Vector2 _mouseCoordinate;

        private bool _isInCenter;
        // Update is called once per frame
        void Update()
        {
            if (_radialGO.activeInHierarchy)
            {
                GetMousePosition();
                CenterCheck(_isInCenter);
                LazySelection(CalculateAngle());
            }
        }



        public void CenterCheck(bool isInCenter)
        {
            _isInCenter = isInCenter;
        }

        private void GetMousePosition()
        {
            //Gets mouse position
            _mouseCoordinate.x = Mouse.current.position.ReadValue().x - (Screen.width / 2);
            _mouseCoordinate.y = Mouse.current.position.ReadValue().y - (Screen.height / 2);
            //makes mouse position x and Y range from 0f to 1f;
            _mouseCoordinate.Normalize();
        }

        private float CalculateAngle()
        {
            if (_mouseCoordinate != Vector2.zero)
            {
                //If mouse is not in 0,0, convert its position to a 360 degree relative to center of screen starting at 12 o'clock;
                float angle = Mathf.Atan2(_mouseCoordinate.y, -_mouseCoordinate.x) / Mathf.PI;
                angle *= 180f;
                angle -= 45f;
                if (angle < 0f)
                {
                    angle += 360f;
                    return angle;
                }
                return angle;
            }
            else
            {
                Debug.LogError("Mouse is directly in center");
                return 0f;
            }
        }
        private void LazySelection(float angle)
        {
            if (!_isInCenter)
            {
                //splits the radial menu into 4 areas that are 90 degrees wide.
                for (int i = 0; i < _radialMenu.radialSegments.Length - 1; i++)
                {
                    if (angle > i * 90 && angle < (i + 1) * 90)
                    {
                        //TODO: Animate and add sound
                    }
                }
            }

        }
    }
}