using UnityEngine;

public class Missile_handler : MonoBehaviour
{
    private Vector3 NewVector;
    public GameObject Smoke;//attaches to the smoke and fire effects
    public GameObject Fire;

    public AI_PlayGame APG;

    private AudioSource audioData;

    public float Z_thrust; //the thrust to give the missile
    public float Y_thrust;
    public Rigidbody rb; //gets the ridged body component

    public GameObject B1; //used for the position of all the ships
    public GameObject B2;
    public GameObject S1;
    public GameObject S2;
    public GameObject C;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>(); //gets the correct audio source 
        rb.AddRelativeForce(Vector3.forward * Z_thrust); //adds the thrust
        rb.AddRelativeForce(Vector3.down * Y_thrust);
    }

    public void Fireing()
    {
        audioData.Play(0); //plays the missile firing sound
        Smoke.SetActive(false); //stops the smoke from the last missile being sent
        Fire.SetActive(false);
        Jump: //will loop till it find a ship that hasn't been destroyed, then launch a missile from it.
        int j = Random.Range(1, 6); //selects a random ship

        if(j == 1)
        {
            if (APG.SLL.B1L == 0) //if the ships has been destroyed 
            {
                goto Jump;//loop
            }
            transform.position = B1.transform.position;//or move the missile to that ship and fire
        }
        else if(j== 2)
        {
            if (APG.SLL.B2L == 0)
            {
                goto Jump;
            }
            transform.position = B2.transform.position;
        }
        else if(j== 3) 
        {
            if (APG.SLL.S1L == 0)
            {
                goto Jump;
            }
            transform.position = S1.transform.position;
        }
        else if(j == 4)
        {
            if (APG.SLL.S2L == 0)
            {
                goto Jump;
            }
            transform.position = S2.transform.position;
        }
        else
        {
            if (APG.SLL.CL == 0)
            {
                goto Jump;
            }
            transform.position = C.transform.position;
        };
        Smoke.SetActive(true);//turns the effects on 
        Fire.SetActive(true);
    }
}
