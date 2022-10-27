using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamFollow : MonoBehaviour
{
    //public static CameraFollow _instance;

    public Camera cameraRef;

    public Transform playerTransform;

    private Vector3 _cameraOffset;

    private Vector3 newPos;

    private Vector3 velocity = Vector3.zero;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;

    Transform thisTransformCache;               //should make a small savings in performance


    //other variables
    public bool followJustPlayer = false;

    private void Awake()
    {
        //_instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        thisTransformCache = this.transform;        //supposed to save computing costs since this.Transform is equivalent to getComponent

        transform.parent = null;                    //set our camera free so that it's position isn't restricted to player's

        if (playerTransform)
        {
            _cameraOffset = thisTransformCache.position - playerTransform.position;
        }

        if(PlayerMovement.LocalPlayerInstance)
        {
            playerTransform = PlayerMovement.LocalPlayerInstance.transform;
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (playerTransform)
        {
            if (followJustPlayer)
            {

                newPos = playerTransform.position + _cameraOffset;                                              //if player is not moving then make new follow position just the player  

                //thisTransformCache.position = Vector3.Slerp(thisTransformCache.position, newPos, SmoothFactor);

                //thisTransformCache.position = Vector3.Lerp(thisTransformCache.position, newPos, SmoothFactor);

                this.thisTransformCache.position = Vector3.SmoothDamp(thisTransformCache.position, newPos, ref velocity, SmoothFactor);

                if (LookAtPlayer)
                {
                    thisTransformCache.LookAt(playerTransform);
                }

            }


        }
    }
}
