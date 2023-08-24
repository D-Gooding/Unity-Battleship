using UnityEngine;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;


public class ShipPlacer : MonoBehaviour
{
    public class Vessel : MonoBehaviour
    {
        public GameObject model;
        public string name;
        public bool NotBeenUsed = true;
        public Vector3 CurrentShipPostion;
        public Vector3 FinalShipPostion;

    }
    public class verify : MonoBehaviour
    {
        public Vector3 finalPosition;
        public string name;
        public GameObject CurrentVessel;

    }
    private Grid grid;                    //Variables

    public static UPNUM UNS;

    public GameObject carrierModel;
    public GameObject Battleship1Model;
    public GameObject Battleship2Model;
    public GameObject frigate1Model;
    public GameObject frigate2Model;
    public GameObject Submarine1Model;
    public GameObject Submarine2Model;

    public GameObject CONFIRMERROR;

    private static bool CBS = false;
    private static int a = 0;

    public GameObject Confirm_button;
    public GameObject Start_Game_Button;
    public GameObject Rotate_Button;

    private static GameObject Current_Ship;

    private static bool button_rotate_value = false;

    public start_game_setup SGS;

    private static int AllShipsUsed = 0;

    private static bool OutsideZone;

    private static bool ShipCurrentlyBeingUsed = false;

    public string btype;                       //variables

    public static Vessel Carrier = new Vessel();
    public static Vessel Battleship1 = new Vessel();
    public static Vessel Battleship2 = new Vessel();
    public static Vessel Submarine1 = new Vessel();
    public static Vessel Submarine2 = new Vessel();
    public static verify OurCurrentShip = new verify();

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);


        Carrier.name = "carrier";
        Carrier.model = carrierModel;

        Battleship1.name = "Battleship1";
        Battleship1.model = Battleship1Model;

        Battleship2.name = "Battleship2";
        Battleship2.model = Battleship2Model;

        Submarine1.name = "Submarine1";
        Submarine1.model = Submarine1Model;

        Submarine2.name = "Submarine2";
        Submarine2.model = Submarine2Model;
        create_text_player();//create / opens the text file path

    }

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();//attaches the grid to the variable grid
        create_text_player();
    }

    private void Update()
    {
        if (AllShipsUsed == 5)
        {
            Confirm_button.SetActive(false);
            Start_Game_Button.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0)) //waits for a left mouse click                  
        {
            RaycastHit hitInfo;          //sets a Raycast hit to hitinfo
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //calaculates from the camera posistion and where you clicked on the screen to the correct place on the screen

            if (Physics.Raycast(ray, out hitInfo))   //if the raycast hits a point on the grid then
            {
                if (hitInfo.point.y <= 0.4)
                {
                    PlaceShipNearAsync(hitInfo.point);        //start the ship placement fumnction on the correct position
                }
            }
        }
    }


    private async void PlaceShipNearAsync(Vector3 clickPoint)  //function 
    {
        Debug.Log(clickPoint);
        OurCurrentShip.finalPosition = grid.GetNearestPointOnGrid(clickPoint);  //rounds the point clicked on the grid to the nearest square within that grid
        Debug.Log(OurCurrentShip.finalPosition);                                    // debug for the posistion clicked

        if (btype == ("Carrier")) //if button pressed is the carrier button                                    
        {
            if(Carrier.NotBeenUsed == true)
            {
                Debug.Log("CS");
                (Carrier.model).transform.position = OurCurrentShip.finalPosition; // using that final postion place the correct vessel on that point
                await Task.Delay(TimeSpan.FromSeconds(0.1));  //allows the collider check to catch up 
                OurCurrentShip.name = Carrier.name;
                OurCurrentShip.CurrentVessel = Carrier.model;
                StartCoroutine(wait_r());

            }
            else
            {
                Debug.Log("carrier used");
            }

        }
        if (btype == ("Submarine"))//if button pressed is the submarine button  
        {
            if (Submarine1.NotBeenUsed == true)
            {
                //Debug.Log("CAR_SELECT"); 
                (Submarine1.model).transform.position = OurCurrentShip.finalPosition; // using that final postion place the correct vessel on that point
                await Task.Delay(TimeSpan.FromSeconds(0.1));  //allows the collider check to catch up
                OurCurrentShip.name = Submarine1.name;
                OurCurrentShip.CurrentVessel = Submarine1.model;
                StartCoroutine(wait_r());
            }
            else if ((Submarine1.NotBeenUsed == false) && (Submarine2.NotBeenUsed == true))
            {
                (Submarine2.model).transform.position = OurCurrentShip.finalPosition; // using that final postion place the correct vessel on that point
                await Task.Delay(TimeSpan.FromSeconds(0.1));  //allows the collider check to catch up 
                OurCurrentShip.name = Submarine2.name;
                OurCurrentShip.CurrentVessel = Submarine2.model;
                VerifyFX.Testing();
                StartCoroutine(wait_r());

            }
            else
            {
                Debug.Log("all Submarines used");
            }
        }
        if (btype == ("BattleShip")) //if button pressed is the battleship button  
        {
            if (Battleship1.NotBeenUsed == true)
            {
                (Battleship1.model).transform.position = OurCurrentShip.finalPosition; // using that final postion place the correct vessel on that point
                await Task.Delay(TimeSpan.FromSeconds(0.1));  //allows the collider check to catch up
                OurCurrentShip.name = Battleship1.name;
                OurCurrentShip.CurrentVessel = Battleship1.model;
                StartCoroutine(wait_r());
                
            }
            else if((Battleship1.NotBeenUsed == false) && (Battleship2.NotBeenUsed == true))
            {
                (Battleship2.model).transform.position = OurCurrentShip.finalPosition; // using that final postion place the correct vessel on that point
                await Task.Delay(TimeSpan.FromSeconds(0.1));  //allows the collider check to catch up
                OurCurrentShip.name = Battleship2.name;
                OurCurrentShip.CurrentVessel = Battleship2.model;
                StartCoroutine(wait_r());
                

            }
            else
            {
                Debug.Log("all battleships used");
            }
        }
        if(AllShipsUsed == 5)
        {
            Confirm_button.SetActive(false);
            Start_Game_Button.SetActive(true);
        }

    }

    void create_text_player()
    {
        string path = Application.persistentDataPath + "/player_Ship.txt";
        FileStream stream = new FileStream(path, FileMode.Create);
        stream.Close();
        using (stream = new FileStream(path, FileMode.Truncate))
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.Write("");
            }
        }
        stream.Close();
    }

    public void boat_type(string type) //recives string values from the Type_control script
    {
        if(ShipCurrentlyBeingUsed == false)
        {
            btype = type; //This assigns it to an internal variable 
            Debug.Log(type);
            return;
        }
        else
        {
            CONFIRMERROR.SetActive(true);
        }
        return;
    }

    public void outside_box()//this finds out if the ship is in the collider from the collider script 
    {
        if (OutsideZone == false) //
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

    public void confirm_button()
    {
        CBS = true; //checks if button is clicked
        CONFIRMERROR.SetActive(false);
    }

    public void rotate_button()
    {
        button_rotate_value = true;
    }
    public void Start_Game_function()
    {
        SGS.start_game_button();
    }

    public class VerifyFX
    {
        public static void Testing()
            {
            Debug.Log(OurCurrentShip.name);
            }

    }

    public IEnumerator wait_r() //This is the Single thread task function (allows while loops withput crashing)
    {
        ShipCurrentlyBeingUsed = true;
        Confirm_button.SetActive(false); //Stops the confirm button from being active
        while (true)
        {
            if (OutsideZone == true) //if boat outside zone
            {
                Debug.Log("shipCOnOUt");
                Confirm_button.SetActive(false); //sets the button to deactive
                yield return null;
            }
            else
            {
                Confirm_button.SetActive(true); //the boat is not in the collider therfore is in the playa area so you can confirm the position
                Debug.Log("SHIPCONIN");
                Debug.Log(OurCurrentShip.finalPosition);
                
                if (Input.GetKeyDown(KeyCode.R) || button_rotate_value == true) //if R key is pressed
                {
                    Debug.Log("2");
                    button_rotate_value = false;
                    OurCurrentShip.CurrentVessel.transform.Rotate(Vector3.up, 90); //rotates the ship
                    a = a + 1; //so the program can keep a record of the rotate position 
                    if (a == 4) //once it gets to 4 it resets (done a full 360)
                    {
                        a = 0;
                    }
                }
                
                

                if (CBS == true) //If Confirm button pressed
                {
                    string path = Application.persistentDataPath + "/player_Ship.txt";
                    Debug.Log("Confirmed");
                    if (a % 2 == 1) //is rotate position odd
                    {
                        Vector3 temp = OurCurrentShip.finalPosition; //creates a temp file of the coordiantes
                        temp.z = temp.z + 1; //adds 1 to the x value (to show that the ship takes up more than one position) 
                        File.AppendAllText(path, ((temp.x) + "," + (temp.z) + ("," + (OurCurrentShip.name) + "\n"))); //writes the current x and z values to the text file
                        Vector3 temp2 = OurCurrentShip.finalPosition; // creates another temp value
                        temp2.z = temp2.z - 1; //minus 1 to the x value (to show that the ship takes up more than two position) 
                        File.AppendAllText(path, ((temp2.x) + "," + (temp2.z) + ("," + (OurCurrentShip.name)) + "\n")); //writes to the text file
                        if(name == "Battleship1" || name == "Battleship2") // if Battleship1 or 2
                        {
                            if(a == 1) //if a facing east then 
                            {
                                temp2.z = temp2.z - 1; //remove one from the z value (horizontal)
                                File.AppendAllText(path, ((temp2.x)+ ","+ (temp2.z) + ("," + (OurCurrentShip.name)) + "\n")); //write to text file
                                temp2.z = temp2.z - 1; //remove one from the z value (horizontal)
                                File.AppendAllText(path, ((temp2.x) + "," + (temp2.z) + ("," + (OurCurrentShip.name)) + "\n")); //write to text file
                                a = 0;//Return a to 0
                            }
                            else
                            {
                                temp.z = temp.z + 1;
                                File.AppendAllText(path, ((temp.x) + "," + (temp.z) + ("," + (OurCurrentShip.name)) + "\n"));
                                temp.z = temp.z + 1;
                                File.AppendAllText(path, ((temp.x) + "," + (temp.z) + ("," + (OurCurrentShip.name)) + "\n"));
                                a = 0;
                            }
                        }


                    }
                    else
                    {
                        
                        Vector3 temp = OurCurrentShip.finalPosition; // add orignal           all same as before but for the vertical value.
                        temp.x = temp.x + 1;
                        File.AppendAllText(path, ((temp.x) + "," + (temp.z) + ("," + (OurCurrentShip.name) + "\n")));
                        Vector3 temp2 = OurCurrentShip.finalPosition;
                        temp2.x = temp2.x - 1;
                        File.AppendAllText(path, ((temp2.x) + "," + (temp2.z) + ("," + (OurCurrentShip.name)) + "\n"));
                        if (name == "Battleship1" || name == "Battleship2")
                        {
                            if (a == 2)
                            {
                                temp2.x = temp2.x - 1;
                                File.AppendAllText(path, ((temp2.x)+","+ (temp2.z) + ("," + (OurCurrentShip.name)) + "\n"));
                                temp2.x = temp2.x - 1;
                                File.AppendAllText(path, ((temp2.x) + "," + (temp2.z) + ("," + (OurCurrentShip.name)) + "\n"));
                                a = 0;
                            }
                            else
                            {
                                temp.z = temp.x + 1;
                                File.AppendAllText(path, ((temp2.x) + "," + (temp2.z) + ("," + (OurCurrentShip.name)) + "\n"));
                                temp.z = temp.x + 1;
                                File.AppendAllText(path, ((temp2.x) + "," + (temp2.z) + ("," + (OurCurrentShip.name)) + "\n"));
                                a = 0;
                            }
                        }
                    }
                    File.AppendAllText(path, (OurCurrentShip.finalPosition.x)+","+ (OurCurrentShip.finalPosition.z) + ("," + (OurCurrentShip.name)) + "\n");
                    Debug.Log(OurCurrentShip.finalPosition);
                    if (OurCurrentShip.name == "Battleship1")//these are all to stop mulitple ships from being used
                    {
                        Battleship1.NotBeenUsed = false;
                        UNS.fxbatscore("1");
                        AllShipsUsed = AllShipsUsed + 1;
                    }
                    if (OurCurrentShip.name == "Battleship2")//these are all to stop mulitple ships from being used
                    {
                        Battleship2.NotBeenUsed = false;
                        UNS.fxbatscore("0");
                        AllShipsUsed = AllShipsUsed + 1;
                    }
                    if (OurCurrentShip.name == "Submarine1")
                    {
                        Submarine1.NotBeenUsed = false;
                        UNS.fxsubscore("1");
                        AllShipsUsed = AllShipsUsed + 1;
                    }
                    if (OurCurrentShip.name == "Submarine2")
                    {
                        Submarine2.NotBeenUsed = false;
                        UNS.fxsubscore("0");
                        AllShipsUsed = AllShipsUsed + 1;
                    }
                    if (OurCurrentShip.name == "carrier")
                    {
                        Carrier.NotBeenUsed = false;
                        UNS.fxcarscore("0");
                        AllShipsUsed = AllShipsUsed + 1;
                    }
                    CBS = false;
                    a = 0;
                    yield return null;
                    ShipCurrentlyBeingUsed = false;
                    Confirm_button.SetActive(false);
                    StopAllCoroutines();
                    
                }
                

            }
            yield return null;

        }
        


    }

}