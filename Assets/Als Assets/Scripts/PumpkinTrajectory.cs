using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinTrajectory : MonoBehaviour
{

    public float gravityVal = -9.81f;

    public bool pumpkinInLaunch = false;

    public Vector3 currentVelocity = new Vector3();

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
        if(pumpkinInLaunch)
        {
            rb.MovePosition(transform.position + (currentVelocity * Time.deltaTime));

            currentVelocity += Vector3.up * gravityVal * Time.deltaTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit something with trigger");

        if (other.transform.tag == "ground")
        {
            Debug.Log("trigger Hit the ground");
            pumpkinInLaunch = false;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Hit something");

        if (collision.transform.tag == "ground")
        {
            Debug.Log("Hit the ground");
            pumpkinInLaunch = false;
            rb.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Call this to launch the pumpkin
    /// </summary>
    /// <param name="startVelocity"></param>
    public void LaunchPumpkin(Vector3 startVelocity)
    {
        transform.parent = null;
        currentVelocity = startVelocity;
        pumpkinInLaunch = true;
    }


}