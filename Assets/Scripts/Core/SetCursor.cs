using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Spire.GM
{
    public class SetCursor : MonoBehaviour
    {

        public Texture2D defaultCursorTexture;
        public Texture2D combatCursorTexture;
        public Texture2D noLoFCursorTexture;

        public Vector2 crossHairOffSet;
        private void Start()
        {
            //crossHairOffSet = new Vector2(defaultCursorTexture.width / 2, defaultCursorTexture.height / 2);

            Cursor.SetCursor(defaultCursorTexture, crossHairOffSet, CursorMode.Auto);
        }

    }
}