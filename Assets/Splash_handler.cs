using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash_handler : MonoBehaviour
{
    private Vector3 NewVector;
    private AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Splash(float x, float y) //takes in coordinates, plays audio for splash
    {
        audioData.Play(0);
        x = x + 0.4f;
        y = y + 0.3f;
        NewVector = new Vector3(x, 0.5f, y);
        transform.position = NewVector;
        var exp = GetComponent<ParticleSystem>();
        exp.Play(); //then starts the splash animation
    }
}
