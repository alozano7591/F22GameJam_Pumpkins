using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class PumpkinCrate : MonoBehaviourPunCallbacks
{

    public int playerNumber;



    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "pumpkin")
        {
            if(PhotonNetwork.IsMasterClient)
            {
                ScoreManager.Instance.AddToPlayerScore(playerNumber, 1);
            }
            
        }


    }

}
