using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using System;

public class move_ships : MonoBehaviour
{
    public GameObject AIBattleship1;
    public GameObject AIBattleship2;
    public GameObject AISubmarine1;
    public GameObject AISubmarine2;
    public GameObject AICarrier;


    public List<char> boatX = new List<char>();
    public List<char> boatY = new List<char>();

    public TextAsset textFile;

    private int valCharx;
    private int valChary;
    private int rotated;

    private int counter = 0;
    private string mystring;

    void Start()
    {
        string path = Application.dataPath + "/AI_Ships.txt";
        readTextFile(path);
    }

    void readTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            valCharx = (int)Char.GetNumericValue(inp_ln[0]);
            valChary = 30;
            valChary = valChary + (int)Char.GetNumericValue(inp_ln[3]);
            rotated = ((int)Char.GetNumericValue(inp_ln[5]));
            Debug.Log("rotate: "+rotated);
            Debug.Log(valCharx);
            Debug.Log(valChary);
            if (counter == 0)
            {
                AIBattleship1.transform.position = new Vector3(valCharx, 1, valChary);
                if (rotated == 1)
                {
                    AIBattleship1.transform.Rotate(Vector3.up, 90);
                }

            }
            if (counter == 4)
            {
                AIBattleship2.transform.position = new Vector3(valCharx, 1, valChary);
                if (rotated == 1)
                {
                    AIBattleship1.transform.Rotate(Vector3.up, 90);
                }
            }
            if (counter == 8)
            {
                AISubmarine1.transform.position = new Vector3(valCharx, 1, valChary);
                if (rotated == 1)
                {
                    AIBattleship1.transform.Rotate(Vector3.up, 90);
                }
            }
            if (counter == 11)
            {
                AISubmarine1.transform.position = new Vector3(valCharx, 1, valChary);
                if (rotated == 1)
                {
                    AIBattleship1.transform.Rotate(Vector3.up, 90);
                }
            }
            if (counter == 14)
            {
                AICarrier.transform.position = new Vector3(valCharx, 1, valChary);
                if (rotated == 1)
                {
                    AIBattleship1.transform.Rotate(Vector3.up, 90);
                }
            }
            counter = counter + 1;

        }


        inp_stm.Close();
        Debug.Log(boatX[1]);
    }
}
