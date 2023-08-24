using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliders : MonoBehaviour
{
    // Start is called before the first frame update
    public bool enter = true;
    public Player_Verify CP;
    private bool IN = false;
    private bool OUT = false;
    void Awake()
    {
        var boxCollider = gameObject.GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (OUT == true)
        {
            OUT = false; //Stops crash 
            Debug.Log("out");
            CP.outside_box();//runs the outside box script
        }
        else if (IN == true)
        {
            
            IN = false;
            Debug.Log("in");
            CP.InTheBox();
        }
    }
    private void OnTriggerStay(Collider boxCollider) //if boat in box collider
    {
        OUT = true; //set Out to true
    }
    private void OnTriggerExit(Collider boxCollider)//if boat exits collider
    {
        IN = true;//set IN to true
    }

}
