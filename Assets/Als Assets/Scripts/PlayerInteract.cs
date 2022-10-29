using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviourPunCallbacks
{

    public Transform grabTransform;

    public Transform holdTransform;

    public PlayerMovement playerMovement;

    public float grabRange = 2f;

    public PumpkinTrajectory pumpkinTrajectory;

    public Vector3 throwVelocity = new Vector3(0, 15f, 25f);

    public bool holdingPumpkin = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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
                            pumpkinTrajectory = hitInfo.transform.GetComponent<PumpkinTrajectory>();

                            photonView.RPC("PickUpPumpkinRPC", RpcTarget.All, PumpkinManager.Instance.GetPumpkinIndex(pumpkinTrajectory));

                            pumpkinTrajectory.transform.parent = holdTransform;
                            pumpkinTrajectory.transform.position = holdTransform.position;
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
                    if(pumpkinTrajectory != null)
                    {
                        //pumpkinTrajectory.LaunchPumpkin(throwVelocity.magnitude * playerMovement.movementDirection);
                        //pumpkinTrajectory = null;
                        //holdingPumpkin = false;

                        //photonView.RPC("ThrowPumpkinRPC", RpcTarget.All, throwVelocity.magnitude * playerMovement.movementDirection);

                        photonView.RPC("ThrowPumpkinRPC", RpcTarget.All);

                    }
                    
                }

            }
        }

    }

    [PunRPC]
    public void PickUpPumpkinRPC(int pumpkinNum)
    {
        pumpkinTrajectory = PumpkinManager.Instance.pumpkinList[pumpkinNum];

        //PumpkinTrajectory pumpkin = PumpkinManager.Instance.pumpkinList[pumpkinNum];

        if (!photonView.IsMine)
        {

            //Debug.Log("pick up the pumpkin");
            pumpkinTrajectory.transform.parent = holdTransform;
            pumpkinTrajectory.transform.position = holdTransform.position;
            holdingPumpkin = true;
        }

    }

    //[PunRPC]
    //public void ThrowPumpkinRPC(Vector3 flyVocity)
    //{

    //    if(pumpkinTrajectory)
    //    {
    //        pumpkinTrajectory.LaunchPumpkin(flyVocity);
            
    //        holdingPumpkin = false;
    //        pumpkinTrajectory = null;
    //    }

    //}

    [PunRPC]
    public void ThrowPumpkinRPC()
    {

        if (pumpkinTrajectory)
        {
            pumpkinTrajectory.LaunchPumpkin(throwVelocity.magnitude * playerMovement.transform.forward);

            holdingPumpkin = false;
            pumpkinTrajectory = null;
        }

    }
}
