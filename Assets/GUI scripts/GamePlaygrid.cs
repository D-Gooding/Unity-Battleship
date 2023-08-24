using UnityEngine;

public class GamePlaygrid : MonoBehaviour
{
    public Player_PlayGame P_PG;
    public Gameplay_Script GPS;
    private char[] toremove = {' ','(',')'}; // what to remove from the name 
    private int result;
    public bool Button_A_Grid_Recive(string buttonname) //takes the name of the button where it came from
    {                                                   //removes the brackets and then turns it into an interger.
        string resulttemp = buttonname.Trim(toremove);
        int.TryParse(resulttemp, out result);
        Debug.Log(result);
        int x = result % 10; //we then convert the number into a x and y value.
        int y = result / 10;
        if (P_PG.PlayerGuess(x, y)) //then parses this to the Player play game script, if it was successful then 
        {                           // will return true, else false.
            return true;
        }
        else
        {
            return false;
        }
    }
}
