using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode_Handler : MonoBehaviour
{
    private Vector3 NewVector;
    public GameObject FirePrefab;
    private AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>(); //assigns the audio component
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Explode(float x,float y) //this plays the audio then moves the explsion to the called coordinates
    {
        audioData.Play(0);
        x = x + 0.4f;
        y = y + 0.3f;
        NewVector = new Vector3(x, 1, y); 
        transform.position = NewVector;
        var exp = GetComponent<ParticleSystem>(); //explodes
        exp.Play(); //plays once
        Instantiate(FirePrefab, NewVector, Quaternion.identity); //then adds a fire prefab in that postion.
        //Handheld.Vibrate();   //this is activated when we build for mobile.
    }

}
