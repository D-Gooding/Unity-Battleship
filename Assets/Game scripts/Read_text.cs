using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;


public class Read_text : MonoBehaviour
{
    public List<char> boatX = new List<char>();

    public TextAsset textFile;

    private string mystring;

    // Start is called before the first frame update    https://www.youtube.com/watch?v=eFOdcQMaz2M
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
            Debug.Log(inp_ln[0]);
            Debug.Log(inp_ln[1]);
            boatX.Add(inp_ln[0]);
        }

        inp_stm.Close();
        Debug.Log(boatX[1]);
    }
}