using UnityEngine;
using System.Threading.Tasks;
using System;

public class AI_Missile : MonoBehaviour
{
    public GameObject Smoke;
    public GameObject Fire;
    public Splash_handler SP;
    public float Y_thrust;
    public Explode_Handler EH;
    public Rigidbody rb;
    private Vector3 NewVector;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * Y_thrust);
        Smoke.SetActive(true); //adds all the smoke and fire to make the missile look more realistic
        Fire.SetActive(true);
    }

    // Update is called once per frame
    public async void HIT(float x, float y, bool Hit) // will move the missile over the ship let it fall under gravity.
    {
        x = x + 0.4f; //adjustments
        y = y + 0.3f;
        NewVector = new Vector3(x, 10, y); //moves it above the ship
        transform.position = NewVector;
        await Task.Delay(TimeSpan.FromSeconds(0.75f)); //lets the missile fall before exploding it
        if(Hit == true) //if we HIT the ship
        {
            EH.Explode(x, y); 
        }
        else
        {
            SP.Splash(x, y);
        }
    }
}
