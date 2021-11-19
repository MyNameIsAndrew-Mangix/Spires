using UnityEngine;
using Spire.Actors;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Spire.Core;


namespace Spire.UI
{

    public class RadialMenu : MonoBehaviour
    {
        [SerializeField] private SquadMemberManager _squadMemberManager;
        [SerializeField] private GameObject _radialMenu;
        [SerializeField] private Image[] _radialSegments;
        [SerializeField] private Sprite _defaultMissingIcon;
        private SquadMember _pC;

        public Image[] radialSegments { get => _radialSegments; }
        //TODO: Play an animation for when menu opens
        //make it lazy selection
        //comment the code

        //for lazy selection: maybe make the inner-square hitbox large, and if mouse is in x degree range && not inside inner-square hitbox, become lazy

        void Start()
        {
            UpdateCachedPlayer();
            if (!_radialMenu)
                Debug.LogError("_radialMenu is NULL");
        }

        public void CompareSprite(Image sender)
        {
            //When the icon on the radial menu is clicked, cycles through all segments' sprites and compares them to whichever one was clicked on.
            for (int i = 0; i < _radialSegments.Length; i++)
            {
                //If the sprite clicked on matches the sprite of the corresponding squad member, check if the squad member is already controlled or not before switching. SEE: PreSwapPlayerCheck
                if (sender.sprite == _squadMemberManager.squadMembers[i].sprite)
                    PreSwapPlayerCheck(_squadMemberManager.squadMembers[i]);
            }
        }
        public void ShowHide(InputAction.CallbackContext context)
        {
            if (!TimeState.gameIsPaused)
            {
                UpdateRadialIcons();
                bool isActive = context.ReadValueAsButton();
                if (!_radialMenu.activeSelf)
                {
                    //isActive = true, ergo the menu is open.
                    TimeState.SetHalfTime();
                    _radialMenu.SetActive(isActive);
                }
                else
                {
                    //isActive = false, ergo the menu is closed.
                    TimeState.SetRealtime();
                    _radialMenu.SetActive(isActive);
                }
            }
        }
        private void UpdateRadialIcons()
        {
            for (int i = 0; i < _squadMemberManager.squadMembers.Count; i++)
            {
                _radialSegments[i].sprite = _squadMemberManager.squadMembers[i].sprite;
                if (_radialSegments[i].sprite == null)
                {
                    _radialSegments[i].sprite = _defaultMissingIcon;
                }
            }
        }
        private void PreSwapPlayerCheck(SquadMember target)
        {
            //Takes in the ID of the squad member being checked.
            if (!_squadMemberManager.FindSquadMember(target).isPlayer)
            {
                //If the target is NOT a player, swap control to the target
                Debug.Log("Found squaddies. Swapping control from: " + _pC + " to: " + target);
                _squadMemberManager.SwapControl(_pC, target);
                UpdateCachedPlayer();
            }
        }
        private void UpdateCachedPlayer()
        {
            //Updates which squad member is currently active.
            _pC = _squadMemberManager.FindSquadMember(_squadMemberManager.playerControlledMem);
        }
    }
}