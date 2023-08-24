using UnityEngine;
using System.IO;
using System;

public class Verify : MonoBehaviour
{
    public class Check //create the check class, tis includes the path for the file aswell as the x and y value to check 
    {
        public string Check_Path;
        public int X_ValueCheck;
        public int Y_ValueCheck;
        public int Checking_LengthCurrentShip; //this is used to return the length of the max length of the ship

    }
    public Check CheckingValues = new Check(); //insatiates 

    public AI_PlayGame APG;

    private int valCharx; //private x and y
    private int valChary;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Search_values()
    {
        StreamReader inp_stm = new StreamReader(CheckingValues.Check_Path); //start stream reading 
        //Debug.Log("---------------------search---------------------------");
        while (!inp_stm.EndOfStream) //loop till and of text file
        {
            string inp_ln = inp_stm.ReadLine(); //set current line to inp_ln
            valCharx = (int)Char.GetNumericValue(inp_ln[0]); //change the 1st value to a interger 
            
            valChary = (int)Char.GetNumericValue(inp_ln[2]); //change 3rd to an interger 
            
            int r = valCharx;
            if (valCharx == CheckingValues.X_ValueCheck && valChary == CheckingValues.Y_ValueCheck) //if we have a match
            {
                if (CheckingValues.Check_Path == Application.persistentDataPath + "/player_Ship.txt") //if we are the AI guessing
                {
                    Debug.Log("YAYYAYAAY");
                    if ("" + (inp_ln[4]) == "B") //if the ship we have found iis a battleship
                    {
                        if("" + (inp_ln[14]) == "1") // if its BattleShip1
                        {
                            APG.SLL.B1L= APG.SLL.B1L - 1; // minus 1 from the battleship 1 length
                            CheckingValues.Checking_LengthCurrentShip = APG.SLL.B1L;
                        }
                        else
                        {
                            APG.SLL.B2L = APG.SLL.B2L - 1; // minus 1 from the battleship 2 length
                            CheckingValues.Checking_LengthCurrentShip = APG.SLL.B2L;
                        }

                    }
                    if ("" + (inp_ln[4]) == "c") //if its a carrier then we minus 1 from the length
                    {
                        APG.SLL.CL = APG.SLL.CL - 1;
                        CheckingValues.Checking_LengthCurrentShip = APG.SLL.CL;
                    }
                    if ("" + (inp_ln[4]) == "S") //submarine
                    {
                        if ("" + (inp_ln[13]) == "1")
                        {
                            APG.SLL.S1L = APG.SLL.S1L - 1;
                            CheckingValues.Checking_LengthCurrentShip = APG.SLL.S1L;
                        }
                        else
                        {
                            APG.SLL.S2L = APG.SLL.S2L - 1;
                            CheckingValues.Checking_LengthCurrentShip = APG.SLL.S2L;
                        }
                    }
                }
                inp_stm.Close(); //close stream reader
                return true;     //return true as we found a ship
            }
        }
        inp_stm.Close(); //close stream reader
        return false;    //return false
    }
}
