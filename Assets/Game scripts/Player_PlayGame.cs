using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PlayGame : MonoBehaviour
{
    public Verify VF;
    public Gameplay_Script GPS;
    public Missile_handler MH;
    public int Win = 0;

    public bool PlayerGuess(int x, int y)
    {
        VF.CheckingValues.X_ValueCheck = x;//send our values to the verify class 
        VF.CheckingValues.Y_ValueCheck = y;
        VF.CheckingValues.Check_Path = Application.persistentDataPath + "/AI_Ships.txt"; //then attach the correct path

        if (VF.Search_values()) // if we found a value add 1 to the Win, and return true 
        {
            Debug.LogWarning("found");
            Win = Win + 1;
            MH.Fireing();
            if (Win == 19) //if we had 19 successful hits then we won
            {
                Debug.Log("Player Win");
                string winner = "Player";
                GPS.Win(winner);
            }
            return true;
        }
        else //not found return false
        {
            Debug.Log("not found");
            return false;
        }
    }
}