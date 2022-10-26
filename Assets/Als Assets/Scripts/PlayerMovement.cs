using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    public TMP_Text text;

    public CharacterController cc;

    private Vector3 playerVelocity;


    public float playerSpeed = 10f;

    //velocity and speed calcs
    public Vector3 currentVelocity = new Vector3();
    public float currentSpeed = 0;
    public Vector3 previousPos = new Vector3();

    public float jumpHeight = 10f;

    private float gravityVal = -9.81f;

    private bool groundedPlayer;

    [Tooltip("Rotation angle per second")]
    public float playerRotSpeed = 300f;

    public Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        previousPos = transform.position;
        playerAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        groundedPlayer = cc.isGrounded;

        currentVelocity = (transform.position - previousPos) / Time.deltaTime;
        currentSpeed = currentVelocity.magnitude;

        previousPos = transform.position;
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        if(inputVector.magnitude > 1)
        {
            inputVector = inputVector / inputVector.magnitude;
        }


        playerVelocity = inputVector * playerSpeed * Time.deltaTime;

        playerVelocity.y += gravityVal * Time.deltaTime;

        cc.Move(playerVelocity);

        Vector3 movementDirection = inputVector;
        movementDirection.Normalize();

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerRotSpeed * Time.deltaTime);
        }

        playerAnim.SetFloat("TotalSpeed", currentSpeed);

    }
}
