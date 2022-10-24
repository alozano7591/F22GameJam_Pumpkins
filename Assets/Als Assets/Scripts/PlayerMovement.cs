using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController cc;

    private Vector3 playerVelocity;


    public float playerSpeed = 10f;
    
    public float jumpHeight = 10f;

    private float gravity = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (cc.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        cc.Move(move * Time.deltaTime * playerSpeed);

        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        //// Changes the height position of the player..
        //if (Input.GetButtonDown("Jump") && cc.isGrounded)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        //}

        //playerVelocity.y += gravity * Time.deltaTime;
        //cc.Move(playerVelocity * Time.deltaTime);

    }
}
