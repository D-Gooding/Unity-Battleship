using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateNumberofShips : MonoBehaviour
{
    public GameObject Battletxt;
    public GameObject Carriertxt;
    public GameObject Submarinetxt;

    private int carscore;
    private string batscore;
    private string subscore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Battletxt.GetComponent<UnityEngine.UI.Text>().text = batscore.ToString();
        Carriertxt.GetComponent<UnityEngine.UI.Text>().text = carscore.ToString();
        Submarinetxt.GetComponent<UnityEngine.UI.Text>().text = subscore.ToString();
    }
    public void fxbatscore(string a)
    {
        batscore = a;
    }
    public void fxcarscore()
    {
        carscore = 0;
    }
    public void fxsubscore(string c)
    {
        subscore = c;
    }
}
