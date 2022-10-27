using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviourPunCallbacks
{

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    public TMP_Text text;

    public CharacterController cc;

    private Vector3 playerVelocity;


    public float playerSpeed = 10f;

    //velocity and speed calcs
    public Vector3 currentVelocity = new Vector3();
    public float currentSpeed = 0;
    public Vector3 previousPos = new Vector3();

    public Vector3 movementDirection = new Vector3();

    public float jumpHeight = 10f;

    private float gravityVal = -9.81f;

    private bool groundedPlayer;

    [Tooltip("Rotation angle per second")]
    public float playerRotSpeed = 300f;

    public PlayerCamFollow ourCam;

    public Animator playerAnim;


    private void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        //DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        previousPos = transform.position;
        playerAnim = this.GetComponent<Animator>();

        //Do my things
        if(photonView.IsMine)
        {
            ourCam.gameObject.SetActive(true);
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            ourCam.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        groundedPlayer = cc.isGrounded;

        currentVelocity = (transform.position - previousPos) / Time.deltaTime;
        currentSpeed = currentVelocity.magnitude;

        previousPos = transform.position;

        //if this is our guy then process inputs and movement
        if(photonView.IsMine)
        {
            Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (inputVector.magnitude > 1)
            {
                inputVector = inputVector / inputVector.magnitude;
            }


            playerVelocity = inputVector * playerSpeed * Time.deltaTime;

            playerVelocity.y += gravityVal * Time.deltaTime;

            cc.Move(playerVelocity);

            Vector3 moveDirection = inputVector;
            moveDirection.Normalize();

            movementDirection = moveDirection;

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerRotSpeed * Time.deltaTime);
            }
        }

        playerAnim.SetFloat("TotalSpeed", currentSpeed);

    }
}
