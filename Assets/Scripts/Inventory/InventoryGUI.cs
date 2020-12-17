using UnityEngine;

public class InventoryGUI : MonoBehaviour
{
    private bool _inventoryOn = false;

    public Vector2 windowPosition = new Vector2(0, 0);
    public Vector2 windowSize = new Vector2(360, 360);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            if (!_inventoryOn)
            {
                _inventoryOn = true;
            }
            else if(_inventoryOn)
            {
                _inventoryOn = false;
            }
        }
    
    }

    public void OnGUI()
    {
        if (_inventoryOn)
        {
            GUI.BeginGroup(new Rect(windowPosition.x, windowPosition.y, windowSize.x, windowSize.y));
        }
    }
}
