using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviourPunCallbacks
{

    public Transform grabTransform;

    public Transform holdTransform;

    public float grabRange = 2f;

    public PumpkinTrajectory pumpkinTrajectory;

    public Vector3 throwVelocity = new Vector3(0, 15f, 25f);

    public bool holdingPumpkin = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        //if this is our guy, then do the inputs
        if(photonView.IsMine)
        {
            if (Input.GetKeyDown("e"))
            {

                if (!holdingPumpkin)
                {
                    if (Physics.Raycast(grabTransform.position, grabTransform.forward, out RaycastHit hitInfo, grabRange))
                    {


                        if (hitInfo.transform.tag == "pumpkin")
                        {
                            Debug.Log("We hit a pumpkin");
                            Transform ourNewPumpkin = hitInfo.transform;
                            pumpkinTrajectory = ourNewPumpkin.GetComponent<PumpkinTrajectory>();
                            ourNewPumpkin.parent = holdTransform;
                            ourNewPumpkin.transform.position = holdTransform.position;
                            holdingPumpkin = true;

                        }
                        else
                        {
                            Debug.Log("We hit something else");
                        }
                    }
                }
                else
                {
                    pumpkinTrajectory.LaunchPumpkin(throwVelocity);
                    pumpkinTrajectory = null;
                    holdingPumpkin = false;
                }

            }
        }
        

    }
}
