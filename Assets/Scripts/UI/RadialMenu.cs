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
        private int[] _cachedIDs;
        private Dictionary<int, Sprite> _squadMemIDAndSprite = new Dictionary<int, Sprite>();
        private SquadMember _pC;

        public Image[] radialSegments { get => _radialSegments; }
        //TODO: Play an animation for when menu opens
        //make it lazy selection
        //comment the code

        //for lazy selection: maybe make the inner-square hitbox large, and if mouse is in x degree range && not inside inner-square hitbox, become lazy

        void Start()
        {
            UpdateCachedPlayer();
            _cachedIDs = _squadMemberManager.squadMemberIDs.ToArray();
            for (int i = 0; i < _cachedIDs.Length; i++)
            {
                _squadMemIDAndSprite.Add(_cachedIDs[i], _squadMemberManager.squadMembers[i].Sprite);
            }
            if (_radialMenu == null)
                Debug.LogError("_radialMenu is NULL");
        }

        public void CompareSprite(Image sender)
        {
            //When the icon on the radial menu is clicked, cycles through all segments' sprites and compares them to whichever one was clicked on.
            for (int i = 0; i < _radialSegments.Length; i++)
            {
                //If the sprite clicked on matches the sprite of the corresponding squad member, check if the squad member is already controlled or not before switching. SEE: PreSwapPlayerCheck
                if (sender.sprite == _squadMemIDAndSprite[_cachedIDs[i]])
                    PreSwapPlayerCheck(_cachedIDs[i]);
            }
        }
        public void ShowHide(InputAction.CallbackContext context)
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
        private void UpdateRadialIcons()
        {
            for (int i = 0; i < _cachedIDs.Length; i++)
            {
                _radialSegments[i].sprite = _squadMemIDAndSprite[_cachedIDs[i]];
                if (_radialSegments[i].sprite == null)
                {
                    _radialSegments[i].sprite = _defaultMissingIcon;
                }
            }
        }
        private void PreSwapPlayerCheck(int target)
        {
            //Takes in the ID of the squad member being checked.
            if (!_squadMemberManager.squadMembers[target].isPlayer)
            {
                //If the target is NOT a player, swap control to the target
                _squadMemberManager.SwapControl(_pC.memberId, target);
                UpdateCachedPlayer();
            }
        }
        private void UpdateCachedPlayer()
        {
            //Updates which squad member is currently active.
            _pC = _squadMemberManager.FindSquadMemberById(_squadMemberManager.PlayerControlledID);
        }
    }
}