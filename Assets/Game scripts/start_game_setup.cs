using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Threading.Tasks;

public class start_game_setup : MonoBehaviour
{
    public GameObject AI;
    public GameObject AIgameparts;
    public GameObject sub_but;
    public GameObject bat_but;
    public GameObject carr_but;
    public GameObject attackgrid_but;
    public GameObject start_game_but;
    public GameObject rotate_but;
    public GameObject shiplacer;
    public GameObject VesselCount;
    public GameObject Main_GamePlay;
    public GameObject PLayer_text_Grid;
    public GameObject Ai_text_Grid;
    public GameObject BackPanel;
    public GameObject Back_NumCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async void start_game_button()
    {
        AI.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(0.3));
        AIgameparts.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(0.1));
        start_game_but.SetActive(false);
        rotate_but.SetActive(false);
        sub_but.SetActive(false);
        bat_but.SetActive(false);
        carr_but.SetActive(false);
        shiplacer.SetActive(false);
        VesselCount.SetActive(false);
        PLayer_text_Grid.SetActive(true);
        BackPanel.SetActive(true);
        Back_NumCount.SetActive(false);
        Ai_text_Grid.SetActive(true);


        //attackgrid_but.SetActive(true);
        Main_GamePlay.SetActive(true);



    }
}
