using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Threading.Tasks;

public class Gameplay_Script : MonoBehaviour
{

    public GameObject Player_Ui_Grid; //all our needed game object refrences

    public GameObject player_turn_text;
    public GameObject AI_Turn_text;

    public GameObject Player_game;
    public GameObject AI_play;
    public GameObject Win_Screen;
    public GameObject AI_Win;
    public GameObject Player_Win;


    public AI_PlayGame AI_Play; //script refrence to AI

    public int turn = 0;
    public bool PTurn = true;

    // Start is called before the first frame update
    void Start() //this generates a random value between 1 and 2 which determins who goes first.
    {
        int turn = Random.Range(1, 3);
        Player_Ui_Grid.SetActive(true);
        if(turn == 1)
        {
            Player();
        }
        else
        {
            AI();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Player() //when its the player turn, we set our variable for attack grid to true then turn the AI off
    {                          // finally the player class is set active.
        PTurn = true;
        AI_Turn_text.SetActive(false);
        player_turn_text.SetActive(true);
        Debug.Log("Player turn");
        AI_play.SetActive(false);
        Player_game.SetActive(true);


    }

    public async void AI() //we first stop the attack grid from being clicked, then we turn the player off and the AI off,
    {       
        PTurn = false;
        await Task.Delay(TimeSpan.FromSeconds(2)); //we added awaits to slow the game play down a bit, it also stops spamming. 
        player_turn_text.SetActive(false);
        AI_Turn_text.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(3));
        Debug.Log("AI Turn");
        Player_game.SetActive(false);
        AI_play.SetActive(true);
        AI_Play.AI_Logic(); //we then call the AI function.
        await Task.Delay(TimeSpan.FromSeconds(1));
        Player(); //then we turn the player back on 
    }
    public void Win(string Winner)    //this is called if a win happens
    {
        Player_game.SetActive(false); // we turn both the player and AI off 
        AI_play.SetActive(false);
        Win_Screen.SetActive(true); // then we activate the win screen
        if (Winner == "AI")         //if it's sent from the AI then we have the AI win appear 
        {
            AI_Win.SetActive(true);
        }
        else if(Winner == "Player") //else its the player.
        {
            Player_Win.SetActive(true);
        }
    }
}
