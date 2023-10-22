using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // This is just a simple player controller that I am planning on working on more later. Just the basics to get us ready to test stuff

    // Components & Children
    private Rigidbody playerRigidBody;
    private CharacterController playerController;
    private Transform playerHead;

    // Input Variables
    private float horizontalInput;
    private float verticalInput;
    private float mouseXInput;
    private float mouseYInput;
    private float camXRotation;
    private bool invertRotation = true;

    // Movement & physics

    [SerializeField] private float movementSpeed = 5f;
    private float moveMultiplier;
    [SerializeField] private float sprintMultiplier = 3f;
    [SerializeField] private float turnSpeed = 3f;
    [SerializeField] private float gravity = -10f;
    private Vector3 playerVelocity;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerController = GetComponent<CharacterController>();
        playerHead = GameObject.Find("PlayerHead").transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        GatherInput();
        Move();
        RotatePlayer();
        ApplyGravity();
    }

    private void GatherInput()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");
        mouseXInput = Input.GetAxis("Mouse X");
        mouseYInput = Input.GetAxis("Mouse Y");

        moveMultiplier = Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1;
    }

    private void Move()
    {
        // Forward movement
        if(Input.GetKey(KeyCode.W))
        {
            playerController.Move(transform.forward * movementSpeed * moveMultiplier * Time.deltaTime);
        }

        // Backwards movement
        if(Input.GetKey(KeyCode.S))
        {
            playerController.Move(-transform.forward * movementSpeed * moveMultiplier * Time.deltaTime);
        }

        // Strafe left
        if (Input.GetKey(KeyCode.A))
        {
            playerController.Move(-transform.right * movementSpeed * moveMultiplier * Time.deltaTime);
        }

        // Strafe right
        if (Input.GetKey(KeyCode.D))
        {
            playerController.Move(transform.right * movementSpeed * moveMultiplier * Time.deltaTime);
        }
    }
    private void RotatePlayer()
    {
        // Rotate the player
        transform.Rotate(Vector3.up * mouseXInput * Time.deltaTime * turnSpeed);

        // Rotate the head
        camXRotation += mouseYInput * turnSpeed * Time.deltaTime * (invertRotation ? -1 : 1);
        camXRotation = Mathf.Clamp(camXRotation, -85f, 85f);

        playerHead.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }

    private void ApplyGravity()
    {
        playerVelocity.y += gravity * Time.deltaTime;
        playerController.Move(playerVelocity * Time.deltaTime);
    }
}
