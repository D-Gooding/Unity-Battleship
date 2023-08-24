using UnityEngine;
using System.IO;
using System;
using System.Collections;

public class Player_Verify : MonoBehaviour
{
    public UPNUM UNS; //class refrences 
    public PlaceShipDown PSD;
    public ButtonsManagement BM;

    private int a = 0;

    public GameObject Error;

    private bool Rkey = false;
    public class CheckValues // our checking values sub class
    {
        public Vector3 CheckingPosition; 
        public string CheckingName;
        public GameObject CurrentVessel;

    }
    private static bool OutsideZone; //for our collider class

    public CheckValues ToCheckValues = new CheckValues(); //creates a child
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)== true)
        {
            Rkey = true;
        }
    }

    public void outside_box()//this finds out if the ship is in the collider from the collider script 
    {
        if (OutsideZone == false)
        {
            OutsideZone = true;
            Debug.Log("OUT");
        }
        else
        {
            return;
        }
    }
    public void InTheBox() //this finds out if the ship is not in the collider from the collider script 
    {
        if (OutsideZone == true)
        {
            OutsideZone = false; //this sets outside zone value to false, assuming the last value was true (stops high resource use)
            Debug.Log("IN");
        }
        else
        {
            return;
        }
    }

    public void BeginChecking()
    {
        StartCoroutine(wait_r());
    }

    public IEnumerator wait_r() //This is the Single thread task function (allows while loops withput crashing)
    {

        PSD.ShipCurrentlyBeingUsed = true;
        BM.Confirm_Button.UI_MODEL.SetActive(false); //Stops the confirm button from being active
        while (true)
        {
            if (OutsideZone == true) //if boat outside zone
            {
                Debug.Log("shipCOnOUt");
                BM.Confirm_Button.UI_MODEL.SetActive(false); //sets the button to deactive
                if ((Rkey) || BM.Rotate_Button.Status == true) //if R key is pressed
                {
                    Debug.Log("2");
                    BM.Rotate_Button.Status = false;
                    Rkey = false;
                    ToCheckValues.CurrentVessel.transform.Rotate(Vector3.up, 90); //rotates the ship
                    a = a + 1;
                    if (a == 4)
                    {
                        a = 0;
                    }
                }
                yield return null;
            }
            else
            {
                BM.Confirm_Button.UI_MODEL.SetActive(true); //the boat is not in the collider therfore is in the playa area so you can confirm the position
                Debug.Log("SHIPCONIN");
                Debug.Log(ToCheckValues.CheckingPosition);

                if ((Rkey)|| BM.Rotate_Button.Status == true) //if R key is pressed
                {
                    Debug.Log("2");
                    Rkey = false;
                    BM.Rotate_Button.Status = false;
                    ToCheckValues.CurrentVessel.transform.Rotate(Vector3.up, 90); //rotates the ship
                    a = a + 1;
                    if(a == 4)
                    {
                        a = 0;
                    }
                    
                }
                if (BM.Confirm_Button.Status == true) //If Confirm button pressed
                {
                    Error.SetActive(false);
                    string path = Application.persistentDataPath + "/player_Ship.txt";
                    Debug.Log("Confirmed");
                    if (a == 0 || a == 2) //is rotate position even
                    {
                        Vector3 temp = ToCheckValues.CheckingPosition; //creates a temp file of the coordiantes
                        temp.z = temp.z + 1; //adds 1 to the x value (to show that the ship takes up more than one position) 
                        File.AppendAllText(path, ((Math.Round(temp.x - 0.4)) + "," + (temp.z - 0.5) + ("," + (ToCheckValues.CheckingName) + "\n"))); //writes the current x and z values to the text file
                        Vector3 temp2 = ToCheckValues.CheckingPosition; // creates another temp value
                        temp2.z = temp2.z - 1; //minus 1 to the x value (to show that the ship takes up more than two position) 
                        File.AppendAllText(path, ((Math.Round(temp2.x - 0.4)) + "," + (temp2.z - 0.5) + ("," + (ToCheckValues.CheckingName)) + "\n")); //writes to the text file
                        if (ToCheckValues.CheckingName == "Battleship1" || ToCheckValues.CheckingName == "Battleship2" || ToCheckValues.CheckingName == "carrier") // if Battleship1 or 2
                        {
                            if (a == 0) //if a facing south then 
                            {
                                temp2.z = temp2.z - 1; //remove one from the z value (vertical)
                                File.AppendAllText(path, (Math.Round(temp2.x - 0.4) + "," + (Math.Round(temp2.z - 0.5)) + ("," + (ToCheckValues.CheckingName)) + "\n")); //write to text file
                                if(ToCheckValues.CheckingName == "carrier")
                                {
                                    temp.z = temp.z + 1; //remove one from the z value (vertical)
                                    File.AppendAllText(path, (Math.Round(temp.x - 0.4) + "," + (Math.Round(temp.z - 0.5)) + ("," + (ToCheckValues.CheckingName)) + "\n")); //write to text file
                                }
                                a = 0;//Return a to 0
                            }
                            else
                            {
                                temp.z = temp.z + 1;
                                File.AppendAllText(path, (Math.Round(temp.x - 0.4) + "," + (Math.Round(temp.z - 0.5)) + ("," + (ToCheckValues.CheckingName)) + "\n"));
                                if (ToCheckValues.CheckingName == "carrier")
                                {
                                    temp2.z = temp2.z - 1; //remove one from the z value (vertical)
                                    File.AppendAllText(path, (Math.Round(temp2.x - 0.4) + "," + (Math.Round(temp2.z - 0.5)) + ("," + (ToCheckValues.CheckingName)) + "\n")); //write to text file
                                }
                                a = 0;
                            }
                        }


                    }
                    else
                    {

                        Vector3 temp = ToCheckValues.CheckingPosition; // add orignal           all same as before but for the vertical value.
                        temp.x = temp.x + 1;
                        File.AppendAllText(path, ((Math.Round(temp.x - 0.4)) + "," + (Math.Round(temp.z - 0.5)) + ("," + (ToCheckValues.CheckingName) + "\n")));
                        Vector3 temp2 = ToCheckValues.CheckingPosition;
                        temp2.x = temp2.x - 1;
                        File.AppendAllText(path, ((Math.Round(temp2.x - 0.4)) + "," + (Math.Round(temp2.z - 0.5)) + ("," + (ToCheckValues.CheckingName)) + "\n"));
                        if (ToCheckValues.CheckingName == "Battleship1" || ToCheckValues.CheckingName == "Battleship2" || ToCheckValues.CheckingName == "carrier")
                        {
                            if (a == 1) //east ///solve the issue
                            {
                                temp2.x = temp2.x - 1;
                                File.AppendAllText(path, ((Math.Round(temp2.x - 0.4)) + "," + (Math.Round(temp2.z - 0.5)) + ("," + (ToCheckValues.CheckingName)) + "\n"));
                                if(ToCheckValues.CheckingName == "carrier")
                                {
                                    temp.x = temp.x + 1;
                                    File.AppendAllText(path, ((Math.Round(temp.x - 0.4)) + "," + (Math.Round(temp.z - 0.5)) + ("," + (ToCheckValues.CheckingName)) + "\n"));
                                }
                                a = 0;
                            }
                            else
                            {
                                temp.x = temp.x + 1;
                                File.AppendAllText(path, ((Math.Round(temp.x - 0.4)) + "," + (Math.Round(temp.z - 0.5)) + ("," + (ToCheckValues.CheckingName)) + "\n"));
                                if (ToCheckValues.CheckingName == "carrier")
                                {
                                    temp2.x = temp2.x - 1;
                                    File.AppendAllText(path, ((Math.Round(temp2.x - 0.4)) + "," + (Math.Round(temp2.z - 0.5)) + ("," + (ToCheckValues.CheckingName)) + "\n"));
                                }
                                a = 0;
                            }
                        }
                    }
                    File.AppendAllText(path, ((Math.Round(ToCheckValues.CheckingPosition.x - 0.43))) + "," + (Math.Round(ToCheckValues.CheckingPosition.z - 0.5)) + ("," + (ToCheckValues.CheckingName)) + "\n");
                    Debug.Log(ToCheckValues.CheckingPosition);
                    if (ToCheckValues.CheckingName == "Battleship1")//these are all to stop mulitple ships from being used
                    {
                        PSD.Battleship1.NotBeenUsed = false;
                        UNS.fxbatscore("1");
                        PSD.AllShipsUsed = PSD.AllShipsUsed + 1;
                    }
                    if (ToCheckValues.CheckingName == "Battleship2")//these are all to stop mulitple ships from being used
                    {
                        PSD.Battleship2.NotBeenUsed = false;
                        UNS.fxbatscore("0");
                        PSD.AllShipsUsed = PSD.AllShipsUsed + 1;
                    }
                    if (ToCheckValues.CheckingName == "Submarine1")
                    {
                        PSD.Submarine1.NotBeenUsed = false;
                        UNS.fxsubscore("1");
                        PSD.AllShipsUsed = PSD.AllShipsUsed + 1;
                    }
                    if (ToCheckValues.CheckingName == "Submarine2")
                    {
                        PSD.Submarine2.NotBeenUsed = false;
                        UNS.fxsubscore("0");
                        PSD.AllShipsUsed = PSD.AllShipsUsed + 1;
                    }
                    if (ToCheckValues.CheckingName == "carrier")
                    {
                        PSD.Carrier.NotBeenUsed = false;
                        UNS.fxcarscore("0");
                        PSD.AllShipsUsed = PSD.AllShipsUsed + 1;
                    }
                    BM.Confirm_Button.Status = false;
                    a = 0;
                    yield return null;
                    PSD.ShipCurrentlyBeingUsed = false;
                    BM.Confirm_Button.UI_MODEL.SetActive(false);
                    StopAllCoroutines();

                }


            }
            yield return null;

        }



    }
}
