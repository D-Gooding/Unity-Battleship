using System;
using System.Threading.Tasks;
using UnityEngine;

public class PlaceShipDown : MonoBehaviour
{
    public ButtonsManagement BM; // class refrences
    public Player_Verify Ve;
    public Grid grid;


    public GameObject carrierModel; //our game models
    public GameObject Battleship1Model;
    public GameObject Battleship2Model;
    public GameObject frigate1Model;
    public GameObject frigate2Model;
    public GameObject Submarine1Model;
    public GameObject Submarine2Model;

    public int AllShipsUsed = 0; 
    public class Vessel //Our boat class
    {

        public GameObject model;
        public string name;
        public bool NotBeenUsed = true;
        public Vector3 CurrentShipPostion;
        public Vector3 FinalShipPostion;


    }

    public bool ShipCurrentlyBeingUsed = false;

    public Vessel Carrier = new Vessel(); //creates new children 
    public Vessel Battleship1 = new Vessel();
    public Vessel Battleship2 = new Vessel();
    public Vessel Submarine1 = new Vessel();
    public Vessel Submarine2 = new Vessel();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        Carrier.model = carrierModel;   //Instantiate the classes
        Carrier.name = "carrier";
        Battleship1.model = Battleship1Model;
        Battleship1.name = "Battleship1";
        Battleship2.model = Battleship2Model;
        Battleship2.name = "Battleship2";
        Submarine1.model = Submarine1Model;
        Submarine1.name = "Submarine1";
        Submarine2.model = Submarine2Model;
        Submarine2.name = "Submarine2";
    }

    // Update is called once per frame
    void Update()
    {


    }

    public async void PlaceShipNearAsync(Vector3 clickPoint)  //where the click point calls 
    {
        //Debug.Log(clickPoint);
        Ve.ToCheckValues.CheckingPosition = grid.GetNearestPointOnGrid(clickPoint);  //rounds the point clicked on the grid to the nearest square within that grid
        Debug.Log(Ve.ToCheckValues.CheckingPosition);                                    // debug for the posistion clicked

        if (BM.OurButtons.ButtonValue == ("Carrier")) //if button pressed is the carrier button                                    
        {
            if (Carrier.NotBeenUsed == true)
            {
                Debug.Log("CS");
                (Carrier.model).transform.position = Ve.ToCheckValues.CheckingPosition; // using that final postion place the correct vessel on that point
                await Task.Delay(TimeSpan.FromSeconds(0.1));  //allows the collider check to catch up 
                Ve.ToCheckValues.CheckingName = Carrier.name;
                Ve.ToCheckValues.CurrentVessel = Carrier.model;
                Ve.BeginChecking();

            }
            else
            {
                Debug.Log("carrier used");
            }

        }
        if (BM.OurButtons.ButtonValue == ("Submarine"))//if button pressed is the submarine button  
        {
            if (Submarine1.NotBeenUsed == true)
            {
                //Debug.Log("CAR_SELECT"); 
                (Submarine1.model).transform.position = Ve.ToCheckValues.CheckingPosition; // using that final postion place the correct vessel on that point
                await Task.Delay(TimeSpan.FromSeconds(0.1));  //allows the collider check to catch up
                Ve.ToCheckValues.CheckingName = Submarine1.name; //sets our Verify values to the current ships ones
                Ve.ToCheckValues.CurrentVessel = Submarine1.model;
                Ve.BeginChecking();
            }
            else if ((Submarine1.NotBeenUsed == false) && (Submarine2.NotBeenUsed == true))
            {
                (Submarine2.model).transform.position = Ve.ToCheckValues.CheckingPosition; // using that final postion place the correct vessel on that point
                await Task.Delay(TimeSpan.FromSeconds(0.1));  //allows the collider check to catch up 
                Ve.ToCheckValues.CheckingName = Submarine2.name;
                Ve.ToCheckValues.CurrentVessel = Submarine2.model;
                Ve.BeginChecking();

            }
            else
            {
                Debug.Log("all Submarines used");
            }
        }
        if (BM.OurButtons.ButtonValue == ("BattleShip")) //if button pressed is the battleship button  
        {
            if (Battleship1.NotBeenUsed == true)
            {
                (Battleship1.model).transform.position = Ve.ToCheckValues.CheckingPosition; // using that final postion place the correct vessel on that point
                await Task.Delay(TimeSpan.FromSeconds(0.1));  //allows the collider check to catch up
                Ve.ToCheckValues.CheckingName = Battleship1.name;
                Ve.ToCheckValues.CurrentVessel = Battleship1.model;
                Ve.BeginChecking();

            }
            else if ((Battleship1.NotBeenUsed == false) && (Battleship2.NotBeenUsed == true))
            {
                (Battleship2.model).transform.position = Ve.ToCheckValues.CheckingPosition; // using that final postion place the correct vessel on that point
                await Task.Delay(TimeSpan.FromSeconds(0.1));  //allows the collider check to catch up
                Ve.ToCheckValues.CheckingName = Battleship2.name;
                Ve.ToCheckValues.CurrentVessel = Battleship2.model;
                Ve.BeginChecking();


            }
            else
            {
                Debug.Log("all battleships used");
            }
        }
        if (AllShipsUsed == 5) //if we have used all our ships then we reset.
        {
            BM.Confirm_Button.UI_MODEL.SetActive(false);
        }

    }
}
