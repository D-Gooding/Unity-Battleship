using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type_control : MonoBehaviour
{
    // Start is called before the first frame update
    public ShipPlacer SP;
    public GameObject attack_grid;
    private string Type;

    private bool grid_but_check;

    void Start()
    {
        Type = ("Submarine");
        SP.boat_type(Type); //Sets the default boat to place down as a submarine
        return;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Submarine() //if sub button clicked
    {
        Type = ("Submarine"); //sets type to submarine
        SP.boat_type(Type); //sends to the ship placer script
        return;

    }
    public void Carrier() //if Carrier vutton clicked 
    {
        Type = ("Carrier"); //sets type to carrier
        SP.boat_type(Type); //sends to ship placer script
        return;
    }
    public void BattleShip() //if Battleship button clicked
    {
        Type = ("BattleShip"); //set type to battle ship
        SP.boat_type(Type);  //Sends to ship placer script
        return;
    }
    public void attackgrid()
    {
        if(grid_but_check == false)
        {
            attack_grid.SetActive(true);
            grid_but_check = true;
        }
        else if(grid_but_check == true)
        {
            attack_grid.SetActive(false);
            grid_but_check = false;
        }
    }
}
