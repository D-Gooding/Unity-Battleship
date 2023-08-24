using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseB;


    public GameObject OptionMenu;
    public GameObject back;
    private bool menucount = false;

    private bool i = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Pause()
    {
        PauseMenu.SetActive(true);
        PauseB.SetActive(false);
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        PauseB.SetActive(true);
    }
    public void Settings()
    {
        OptionMenu.SetActive(true);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void Options_Menu()
    {
        if (menucount == false)
        {
            OptionMenu.SetActive(true);
            menucount = true;
            back.SetActive(true);
        }
        else
        {
            OptionMenu.SetActive(false);
            menucount = false;
            back.SetActive(false);
        }
    }
    public void Goback()
    {
        OptionMenu.SetActive(false);
    }
}
