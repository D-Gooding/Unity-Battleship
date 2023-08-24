using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Random = UnityEngine.Random;

public class AI_PlayGame : MonoBehaviour
{
    public Verify VF; // connection to verify class
    public Gameplay_Script GPS;
    public GameObject Player_Grid;
    public Explode_Handler EH;
    public Splash_handler SP;
    public AI_Missile AM;
    public int Win = 0;

    private bool Found_Ship_Last_Turn = false;  //all variables that are needed
    private bool Current_Ship_notComplete = false;
    private List<string> Found_Ships_Coordinates = new List<string>(); //creates arrays for when we read our text file
    private bool IsVertical;
    private bool GoOtherWay;
    private bool P = true;
    private bool u = true;
    private bool firstTimeForNewWay = true;
    public bool AI_turn = false; // For Debuging AI

    public class ShipLen
    {
        public int B1L = 4;
        public int B2L = 4;
        public int CL = 5;
        public int S1L = 3;
        public int S2L = 3;

    }
    public ShipLen SLL = new ShipLen();

    // Start is called before the first frame update
    void Start()
    {
        create_text_AIPastGuess(); //set up the text file
        VF.CheckingValues.Checking_LengthCurrentShip = 100;//set the checking value to a large value so it cant trip the next part.
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AI_Logic()
    {
        if (Current_Ship_notComplete == true) // if we havent completed our current ship
        {
            if (Found_Ships_Coordinates.Count == 1) //we havent figured out if the ship is vertical or horizontal
            {
                Debug.Log("checking for 2");
                AI_Only2Guesses();
            }
            else if (Found_Ships_Coordinates[0][0] == Found_Ships_Coordinates[1][0]) //we can find out if the ship is vertical or not (by comparing the x and y coordinates of the two guesses)
            {
                IsVertical = true;
                Debug.Log("vertical");
                AI_NormalGuess();
            }
            else if (Found_Ships_Coordinates[0][2] == Found_Ships_Coordinates[1][2])
            {
                IsVertical = false;
                Debug.Log("Not vertical");
                AI_NormalGuess();
            }
            else if (Found_Ships_Coordinates.Count >= 3) //Just guess normally 
            {
                AI_NormalGuess();
            }

        }
        else // if new guess
        {
            firstTimeForNewWay = true;
            VF.CheckingValues.X_ValueCheck = Random.Range(0, 10); //random values
            VF.CheckingValues.Y_ValueCheck = Random.Range(0, 10);
            VF.CheckingValues.Check_Path = Application.persistentDataPath + "/AI_PastGuesses.txt"; // search if we have guessed them
            //while (VF.CheckingValues.X_ValueCheck % 2 == 0 && VF.CheckingValues.Y_ValueCheck % 2 == 0)
           //{
            while (VF.Search_values()) // if we have then try another random value till we find one we havent
            {
                Debug.Log("RETRY");
                VF.CheckingValues.X_ValueCheck = Random.Range(0, 10);
                VF.CheckingValues.Y_ValueCheck = Random.Range(0, 10);
            }
            //}
            Debug.Log("final" + VF.CheckingValues.X_ValueCheck); // once we have found them
            Debug.Log("final" + VF.CheckingValues.Y_ValueCheck);
        }
        VF.CheckingValues.Check_Path = Application.persistentDataPath + "/player_Ship.txt"; //set the path to the player ships

        if (VF.Search_values()) // see if our coordinates are in there with our verify class
        {
            AM.HIT(VF.CheckingValues.X_ValueCheck, VF.CheckingValues.Y_ValueCheck,true);//explode at that position
            Found_Ships_Coordinates.Add(VF.CheckingValues.X_ValueCheck + "," + VF.CheckingValues.Y_ValueCheck); // if it it add to the the found ship coordinates
            File.AppendAllText(Application.persistentDataPath + "/AI_PastGuesses.txt", VF.CheckingValues.X_ValueCheck + "," + VF.CheckingValues.Y_ValueCheck + "\n"); //write to the guessed text file
            Current_Ship_notComplete = true; // sets some varaibles that will be useful
            Found_Ship_Last_Turn = true;
            Debug.LogWarning("X" + VF.CheckingValues.X_ValueCheck);
            Debug.LogWarning("Y" + VF.CheckingValues.Y_ValueCheck);
            Debug.Log(Found_Ships_Coordinates.Count + "--------------------------------------------------------------------------------");
            Win = Win + 1;
            //return back
        }
        else
        {
            AM.HIT(VF.CheckingValues.X_ValueCheck, VF.CheckingValues.Y_ValueCheck,false);//if we havent found the ship add the splash at that position.
            Found_Ship_Last_Turn = false; // sets this if we didnt find a ship
            Debug.Log("not found at " + VF.CheckingValues.X_ValueCheck + " " + VF.CheckingValues.Y_ValueCheck);
            File.AppendAllText(Application.persistentDataPath + "/AI_PastGuesses.txt", VF.CheckingValues.X_ValueCheck + "," + VF.CheckingValues.Y_ValueCheck + "U" + "\n"); // Save in the passed guesses text file so they're not guess again 

            if(GoOtherWay == true && Found_Ships_Coordinates.Count == 3) //we have found a ship so we need to reset
            {
                Debug.Log("GOTHER");
                firstTimeForNewWay = true;
                AI_turn = false;
                Found_Ships_Coordinates.Clear();
                Current_Ship_notComplete = false;
                P = true;
                u = true;
            }
        }
        if (VF.CheckingValues.Checking_LengthCurrentShip == 0 && Found_Ships_Coordinates.Count >= 3) // this tells us if we have found the whole ship
        {
            Debug.Log("Completed Ship");
            firstTimeForNewWay = true;
            AI_turn = false;
            Found_Ships_Coordinates.Clear();
            Current_Ship_notComplete = false;
            P = true;
            u = true;
        }
        if(Win == 19)
        {
            Debug.Log("AI WIN!!!!!!!!");
            string Winner = "AI";
            GPS.Win(Winner);
        }
        AI_turn = false;
        return;

    }

    public void AI_NormalGuess() //this is for when we have guessed over 2 values correctly 
    {

        VF.CheckingValues.Check_Path = Application.persistentDataPath + "/AI_PastGuesses.txt";
        //(int)char.GetNumericValue(Found_Ships_Coordinates[(Found_Ships_Coordinates.Count - 1)][0])

        if ((IsVertical == false) && ((int)char.GetNumericValue(Found_Ships_Coordinates[(Found_Ships_Coordinates.Count - 1)][0]) == 9) || (Found_Ship_Last_Turn == false))
        { // if we reach the barrier or if we havent found a ship on our current course 
            GoOtherWay = true; // then we have to go the other way
            Debug.Log("Going other way");
        }
        if ((IsVertical == true) && ((int)char.GetNumericValue(Found_Ships_Coordinates[(Found_Ships_Coordinates.Count - 1)][2]) == 9) || (Found_Ship_Last_Turn == false)) //same bt for Y
        {
            GoOtherWay = true;
            Debug.Log("Going other way");
        }
        VF.CheckingValues.X_ValueCheck = ((int)char.GetNumericValue(Found_Ships_Coordinates[(Found_Ships_Coordinates.Count-1)][0])+1);
        if (VF.Search_values())
        {
            GoOtherWay = true;
        }
        VF.CheckingValues.Y_ValueCheck = ((int)char.GetNumericValue(Found_Ships_Coordinates[(Found_Ships_Coordinates.Count-1)][2])+1);
        if (VF.Search_values())
        {
            GoOtherWay = true;
        }
        if ((IsVertical == false) && (GoOtherWay == false)) // if we have a change in x and we havent been told to go the other way, keep going forward
        {
            Debug.Log("test 0");
            VF.CheckingValues.Y_ValueCheck = (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][2])));
            VF.CheckingValues.X_ValueCheck = (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[Found_Ships_Coordinates.Count - 1][0])) + 1);
            return;

        }
        if ((IsVertical == false) && (GoOtherWay = true))// if we now do have to go the other way 
        {
            if (firstTimeForNewWay == true)// if we have never gone the other way yet we have to start from the first value in our list as we know it is the smallest.
            {
                Debug.Log("NewWay F");
                VF.CheckingValues.X_ValueCheck = (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][0])) - 1);
                VF.CheckingValues.Y_ValueCheck = (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][2])));
                firstTimeForNewWay = false;
            }
            else// we just go back on our last value 
            {
                Debug.Log("NewWay N");
                VF.CheckingValues.X_ValueCheck = (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[Found_Ships_Coordinates.Count - 1][0])) - 1);
                VF.CheckingValues.Y_ValueCheck = (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][2])));
            }
            return;
        }
        if ((IsVertical == true) && (GoOtherWay == false)) //same for the Y \/
        {
            VF.CheckingValues.X_ValueCheck = ((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][0]));
            VF.CheckingValues.Y_ValueCheck = (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[Found_Ships_Coordinates.Count - 1][2])) + 1);
            return;

        }
        if ((IsVertical == true) && (GoOtherWay == true))
        {
            if (firstTimeForNewWay == true)
            {
                Debug.Log("NewWay F");
                VF.CheckingValues.X_ValueCheck = ((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][0]));
                VF.CheckingValues.Y_ValueCheck = (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][2])) - 1);
                firstTimeForNewWay = false;
            }
            else
            {
                Debug.Log("NewWay N");
                VF.CheckingValues.X_ValueCheck = ((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][0]));
                VF.CheckingValues.Y_ValueCheck = (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[Found_Ships_Coordinates.Count - 1][2])) - 1);
            }
            return; // /\
        }
    }

    public void AI_Only2Guesses() // this is for our second guess, by having a P and u value we can have a total of 4 guesses and know where we are at all times.
    {
        if (P == true && u == true)
        {
            if (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][0])) == 9) // if our value is 9 then we know that the next one will be 10 which will be outside
            {
                P = false;
                u = true;
                AI_Only2Guesses(); //try again
            }
            else
            {
                VF.CheckingValues.X_ValueCheck = (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][0])) + 1); // else we try it in verify 
                VF.CheckingValues.Y_ValueCheck = ((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][2]));
                P = false; //sets for the next time if needed
                u = true;
                firstTimeForNewWay = true;//sets the first time to true, this is to correct the ordering for the rest of the guessing
                GoOtherWay = false; //next guess should be in the positive direction
                Debug.Log("right");
                return;
            }
        }
        else if (P == false && u == true)
        {
            if (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][2])) == 9)
            {
                P = true;
                u = false;
                AI_Only2Guesses();
            }
            else
            {
                VF.CheckingValues.X_ValueCheck = ((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][0]));
                VF.CheckingValues.Y_ValueCheck = (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][2])) + 1);
                P = true;
                u = false;
                firstTimeForNewWay = true;
                GoOtherWay = false; // the next guess will be in the negative direction
                Debug.Log("up");
                VF.CheckingValues.Check_Path = Application.persistentDataPath + "/AI_PastGuesses.txt";
                if (VF.Search_values())
                {
                    AI_Only2Guesses();
                }
                return;
            }
        }
        else if (P == true && u == false)
        {
            if (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][0])) == 0)//next value will be outside.
            {
                P = false;
                u = false;
                AI_Only2Guesses();
            }
            else
            {
                VF.CheckingValues.X_ValueCheck = (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][0])) - 1);
                VF.CheckingValues.Y_ValueCheck = ((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][2]));
                P = false;
                u = false;
                firstTimeForNewWay = false;
                GoOtherWay = true;
                Debug.Log("left");
                VF.CheckingValues.Check_Path = Application.persistentDataPath + "/AI_PastGuesses.txt";
                if (VF.Search_values()) // haved we had this value before if so try guessing again.
                {
                    AI_Only2Guesses();
                }
                return;
            }

        }
        else if (P == false && u == false)
        {
            if (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][2])) == 0)
            {
                firstTimeForNewWay = true;
                AI_turn = false;
                Found_Ships_Coordinates.Clear();
                Current_Ship_notComplete = false;
                P = true;
                u = true;
                return;
            }
            VF.CheckingValues.X_ValueCheck = ((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][0]));
            VF.CheckingValues.Y_ValueCheck = (((int)System.Char.GetNumericValue(Found_Ships_Coordinates[0][2])) - 1);
            firstTimeForNewWay = false;
            GoOtherWay = true;
            Debug.Log("down");
            VF.CheckingValues.Check_Path = Application.persistentDataPath + "/AI_PastGuesses.txt";
            if (VF.Search_values()) // given this is the last possible place we can assume that we are gussing across two ships so we require a reset
            {
                firstTimeForNewWay = true;
                AI_turn = false;
                Found_Ships_Coordinates.Clear();
                Current_Ship_notComplete = false;
                P = true;
                u = true;
            }
            return;
        }
        else
        {
            firstTimeForNewWay = true; // given this is the last possible place we can assume that we are gussing across two ships so we require a reset (catch)
            AI_turn = false;
            Found_Ships_Coordinates.Clear();
            Current_Ship_notComplete = false;
            P = true;
            u = true;
        }
    }

    void create_text_AIPastGuess() // creates our past guesses text file
    {
        string AIGuessPath = Application.persistentDataPath + "/AI_PastGuesses.txt"; //save in the Users temperory memory
        FileStream stream = new FileStream(AIGuessPath, FileMode.Create); // start the file stream and create the text file
        stream.Close(); //Stop the file stream so it can be accessed again
        using (stream = new FileStream(AIGuessPath, FileMode.Truncate)) //start a file stream and remove and data that is in the file 
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(""); // clears file on start.
            }
        }
        stream.Close();
    }
}
