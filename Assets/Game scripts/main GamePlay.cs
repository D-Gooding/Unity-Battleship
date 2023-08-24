using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class NewBehaviourScript : MonoBehaviour
{
    private int valCharx;
    private int valChary;

    private int turn;

    public List<string> quessed = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //add ui stuff
        //input a x guess
        //input a y guess
    }

    private bool Search_values_for(int g,int h)
    {
        string path = Application.dataPath + "/AI_Ships.txt";
        StreamReader inp_stm = new StreamReader(path);
        Debug.Log("---------------------search---------------------------");
        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            Debug.Log(inp_ln);
            valCharx = (int)Char.GetNumericValue(inp_ln[0]);
            valChary = ((int)Char.GetNumericValue(inp_ln[2]) * 10);
            valChary = valChary + (int)Char.GetNumericValue(inp_ln[3]);
            //Debug.Log(valCharx);
            //Debug.Log(valChary);
            if (valCharx == g && valChary == h) //if a current y 
            {

                quessed.Add(g+","+h);
                inp_stm.Close();
                return true;
            }
        }
        inp_stm.Close();
        return false;
    }
}
