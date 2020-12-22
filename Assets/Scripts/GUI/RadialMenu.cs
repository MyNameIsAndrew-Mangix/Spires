using UnityEngine;
using Spire.Squad;
namespace Spire.GUI
{

    public class RadialMenu : MonoBehaviour
    {
        private Color _mouseOverColor = Color.gray;
        private Color _originalColor = Color.black;



        private SquadMember _squadMember;
        /// <summary>
        /// MAKE CENTER ELEMENT OF PIE CHART CURRENTLY SELECTED CHARACTER, THEN MAKE 4 DIVIONS OF THE ACTUAL CHART FOR THE NON-SELECTED SQUAD MEMEBERS
        /// CAN ONLY RUN WITH A TEAM UP TO 4 AI SQUAD MEMBERS (5 TOTAL SQUAD MEMBERS INCLUDING PLAYER)
        /// </summary>

        // Start is called before the first frame update
        void Start()
        {
            _squadMember = GetComponent<SquadMember>();
        }


        private void HoverCover()
        {

        }

        private void PlayerCheck()
        {
            if (_squadMember.isPlayer)
            {
                //Do nothing
            }
            else
            {
                BodySwap();
            }
        }

        private void BodySwap()
        {
            //
            throw new System.NotImplementedException();
        }
    }
}