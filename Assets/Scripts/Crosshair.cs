using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    public Texture2D cursorArrow;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void OnEnable(){
        //Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        Vector2 hotSpot = new Vector2(cursorArrow.width / 2f, cursorArrow.height / 2f);
        Cursor.SetCursor(cursorArrow, hotSpot, CursorMode.Auto);
    }

    public void OnDisable(){
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
