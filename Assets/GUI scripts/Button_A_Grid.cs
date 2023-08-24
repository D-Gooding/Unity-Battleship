using UnityEngine;
using UnityEngine.UI;

public class Button_A_Grid : MonoBehaviour
{
    public GamePlaygrid GPG;
    public Gameplay_Script GPS;
    public void Clicked() //on click if it's the players turn then send the name of the gameobject to the GameplayGrid script 
    {
        if(GPS.PTurn == true)
        {
            if (GPG.Button_A_Grid_Recive(gameObject.name)) //if it was a successful hit the change its color to red 
            {
                GetComponent<Image>().color = Color.red;
            }
            else
            {
                GetComponent<Image>().color = Color.white; // else set the color to white (unsuccessful)
            }
            gameObject.GetComponent<Button>().interactable = false; //deactivate the button 
            GPS.AI();//run the gameplay script with the AI function.
        }
    }
}
