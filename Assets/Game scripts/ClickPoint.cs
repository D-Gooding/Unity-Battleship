using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ClickPoint : MonoBehaviour
{
    private Grid grid;
    public PlaceShipDown PSD;
    public ButtonsManagement BM;

    public GameObject Error;

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();//attaches the grid to the variable grid
    }

    // Update is called once per frame
    void Update()
    {
        if (PSD.AllShipsUsed == 5) // if we have used all of our ships set our buttons to go.
        {
            BM.Confirm_Button.UI_MODEL.SetActive(false);
            BM.Start_Button.UI_MODEL.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0)) //waits for a left mouse click                  
        {
            RaycastHit hitInfo;          //sets a Raycast hit to hitinfo
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //calaculates from the camera posistion and where you clicked on the screen to the correct place on the screen
            if (Physics.Raycast(ray, out hitInfo))   //if the raycast hits a point on the grid then
            {
                if (hitInfo.point.y <= 0.4)
                {
                    PSD.PlaceShipNearAsync(hitInfo.point);        //start the ship placement function on the correct position
                }
            }
        }
    }
}
