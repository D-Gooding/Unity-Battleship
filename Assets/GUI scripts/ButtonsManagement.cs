using UnityEngine;

public class ButtonsManagement : MonoBehaviour
{
    public GameObject Confirm_Button_Model;  //variables
    public GameObject BattleShip_Model;
    public GameObject Carrier_Model;
    public GameObject Submarine_Model;
    public GameObject StartGame_Model;
    public GameObject Rotate_Model;
    public bool Confirm_Button_Status;
    public bool BattleShip_Status;
    public bool Carrier_Status;
    public bool submarine_Status;
    public bool StartGame_Status;
    public bool Rotate_Status;  //variables


    public start_game_setup SGS; //refrence to start game set up




    public class button   //creates a button class 
    {
        public GameObject UI_MODEL;  // Loctates a model for it
        public bool Status = false;  // sets its Status to False by Defualt

    }


    public class buttons : button
    {
        public string ButtonValue;
    }
    // Start is called before the first frame update

    public button Confirm_Button = new button();  //create children of that class
    public button Rotate_Button = new button();
    public button Start_Button = new button();
    public button BattleShip_Button = new button();
    public button Carrier_Button = new button();
    public button Submarine_Button = new button();

    public buttons OurButtons = new buttons(); //creates a class for our current button value (used for the place ship down script to know what ship to use

    public PlaceShipDown PSD;// our connection to the place ship down script
    public GameObject Error; // for our error if a ship is already in use
    

    void Start()
    {
        Confirm_Button.UI_MODEL = Confirm_Button_Model;
        Rotate_Button.UI_MODEL = Rotate_Model;
        Start_Button.UI_MODEL = StartGame_Model;
        BattleShip_Button.UI_MODEL = BattleShip_Model;
        Carrier_Button.UI_MODEL = Carrier_Model;
        Submarine_Button.UI_MODEL = Submarine_Model;



    }
    public void ConfirmFX()
    {
        Confirm_Button.Status = true;
    }
    public void BattleshipFX() //acivates when the battleship button is clicked
    {
        if (PSD.ShipCurrentlyBeingUsed == true) // only allows a new ship to be selected if the current one has finsihed beging used
        {
            Error.SetActive(true);
        }
        else
        {
            BattleShip_Button.Status = true;
            OurButtons.ButtonValue = "BattleShip";
        }

    }
    public void CarrierFX() //acivates when the carrier button is clicked
    {
        if (PSD.ShipCurrentlyBeingUsed == true)
        {
            Error.SetActive(true);
        }
        else
        {
            Carrier_Button.Status = true;
            OurButtons.ButtonValue = "Carrier";
        }
    }
    public void SubmarineFX() //acivates when the submarine button is clicked
    {
        if (PSD.ShipCurrentlyBeingUsed == true)
        {
            Error.SetActive(true);
        }
        else
        {
            Submarine_Button.Status = true;
            OurButtons.ButtonValue = "Submarine";
        }
    }
    public void StartGameFX()//acivates when the start game button is clicked
    {
        Start_Button.Status = true;
        SGS.start_game_button();
    }
    public void RotateFX()//acivates when the rotate button is clicked
    {
        Rotate_Button.Status = true;
    }
}
