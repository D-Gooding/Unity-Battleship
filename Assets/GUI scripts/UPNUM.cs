using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UPNUM : MonoBehaviour
{
    public TextMeshProUGUI Battletxt;
    public TextMeshProUGUI Carriertxt;
    public TextMeshProUGUI Submarinetxt;

    private string carscore;
    private string batscore;
    private string subscore;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void fxbatscore(string a)
    {
        batscore = a;
        Battletxt.text = batscore.ToString();
    }
    public void fxcarscore(string b)
    {
        carscore = b;
        Carriertxt.text = carscore.ToString();
    }
    public void fxsubscore(string c)
    {
        subscore = c;
        Submarinetxt.text = subscore.ToString();
    }
}
