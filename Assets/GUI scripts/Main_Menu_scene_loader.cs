using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_scene_loader : MonoBehaviour
{
    public GameObject OptionMenu;
    public GameObject play;
    public GameObject back;
    private bool menucount = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play_button()
    {
        SceneManager.LoadScene("main_player_game");
    }
    public void Options_Menu()
    {
        if(menucount == false)
        {
            OptionMenu.SetActive(true);
            menucount = true;
            play.SetActive(false);
            back.SetActive(true);
        }
        else
        {
            OptionMenu.SetActive(false);
            menucount = false;
            play.SetActive(true);
            back.SetActive(false);
        }    
    }
    public void Exit()
    {
        Application.Quit();
    }
}
