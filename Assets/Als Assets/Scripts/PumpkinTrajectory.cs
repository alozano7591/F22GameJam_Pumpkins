using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinTrajectory : MonoBehaviourPunCallbacks
{

    public float gravityVal = -9.81f;

    public bool pumpkinInLaunch = false;

    public Vector3 currentVelocity = new Vector3();

    public Rigidbody rb;

    public int pointValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //    if(pumpkinInLaunch)
    //    {
    //        rb.MovePosition((transform.position + (currentVelocity * Time.deltaTime)));

    //        currentVelocity += Vector3.up * gravityVal * Time.deltaTime;
    //    }

    //}

    private void FixedUpdate()
    {
        if (pumpkinInLaunch)
        {
            rb.MovePosition((transform.position + (currentVelocity * Time.deltaTime)));

            currentVelocity += Vector3.up * gravityVal * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hit something with trigger");

        if (other.transform.tag == "ground")
        {
            Debug.Log("trigger Hit the ground");
            pumpkinInLaunch = false;
            rb.velocity = Vector3.zero;
        }

        //if (other.transform.tag == "PumpkinCrate")
        //{
        //    ScoreManager.Instance.AddToPlayerScore(other.GetComponent<PumpkinCrate>().playerNumber, pointValue);
        //}
    }

    /// <summary>
    /// Call this to launch the pumpkin
    /// </summary>
    /// <param name="startVelocity"></param>
    /// <param name="launchDirection"></param>
    public void LaunchPumpkin(Vector3 startVelocity)
    {
        transform.parent = null;
        currentVelocity = startVelocity;
        pumpkinInLaunch = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="startVelocity"></param>
    [PunRPC]
    public void LaunchPumpkinRPC(Vector3 startVelocity)
    {

        transform.parent = null;
        currentVelocity = startVelocity;
        pumpkinInLaunch = true;

    }

   


}
