using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_ButtonHandler : MonoBehaviour
{

    public GameObject attack_grid;
    private bool grid_but_check;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void attackgrid()
    {
        if (grid_but_check == false)
        {
            attack_grid.SetActive(true);
            grid_but_check = true;
        }
        else if (grid_but_check == true)
        {
            attack_grid.SetActive(false);
            grid_but_check = false;
        }
    }
}
