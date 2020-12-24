using UnityEngine;
using Spire.Squad;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Spire.GUI
{

    public class RadialMenu : MonoBehaviour
    {

        [SerializeField]
        private SquadMemberManager _squadMemberManager;
        [SerializeField]
        private Sprite[] _menuWedges;
        //WEDGES ARE IN CLOCKWISE ORDER, STARTING FROM TOP LEFT.

        private int[] _cachedIDs;

        private SquadMember _pC;

        // Start is called before the first frame update
        void Start()
        {
            _pC = _squadMemberManager.FindSquadMember(_squadMemberManager.PlayerControlledID);
            _cachedIDs = _squadMemberManager.squadMemberIDs.ToArray();
        }

        private void PlayerCheck()
        {
            if (_pC.isPlayer)
            {
                //Do nothing
            }
            else
            {
                //_squadMemberManager.SwapControl
            }
        }
        public void EnableDisable(InputAction.CallbackContext context)
        {
            // bool isPressed = context.started;
            // bool isReleased = context.canceled;
            bool isActive = context.ReadValueAsButton();

            if (this.gameObject.activeSelf)
            {
                //isActive = false;
                this.gameObject.SetActive(isActive);
            }

            else
            {
                //isActive = true;
                this.gameObject.SetActive(isActive);
            }

        }

        private void BodySwap()
        {

        }
    }
}