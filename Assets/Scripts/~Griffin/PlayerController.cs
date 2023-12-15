using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // This is just a simple player controller that I am planning on working on more later. Just the basics to get us ready to test stuff

    // Components & Children
    private Rigidbody playerRigidBody;
    private CharacterController playerController;
    private Transform playerHead;
    private Interactor interactor;

    // Input Variables
    private float horizontalInput;
    private float verticalInput;
    private float mouseXInput;
    private float mouseYInput;
    private float camXRotation;
    private bool invertRotation = true;

    [Header("Movement & physics")]
    [SerializeField] private float movementSpeed = 5f;
    private float moveMultiplier;
    [SerializeField] private float sprintMultiplier = 3f;
    [SerializeField] private float turnSpeed = 3f;
    [SerializeField] private float gravity = -10f;
    private Vector3 playerVelocity;
    [SerializeField] private float jumpVelocity = 20f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    private float groundCheckDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerController = GetComponent<CharacterController>();
        playerHead = GameObject.Find("PlayerHead").transform;
        interactor = GetComponent<Interactor>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        GatherInput();
        Move();
        RotatePlayer();
        ApplyGravity();
        Jump();
        Interact();
        PauseGame();
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
        Vector3 baseDirection = new Vector3(0, 0, 0);
        Vector3 moveDirection = new Vector3(0, 0, 0);
        // Forward movement
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward;
        }

        // Backwards movement
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += -transform.forward;
        }

        // Strafe left
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += -transform.right;
        }

        // Strafe right
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right;
        }

        baseDirection.x = Mathf.Clamp(baseDirection.x + moveDirection.x, -1, 1);
        baseDirection.y = Mathf.Clamp(baseDirection.y + moveDirection.y, -1, 1);
        baseDirection.z = Mathf.Clamp(baseDirection.z + moveDirection.z, -1, 1);

        playerController.Move(baseDirection * movementSpeed * moveMultiplier * Time.deltaTime);
        //playerController.Move((baseDirection * movementSpeed * moveMultiplier * Time.deltaTime).normalized);
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

    private bool GroundCheck()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            playerVelocity.y = jumpVelocity;
        }
    }

    public void Interact()
    {
        if(interactor.isHitting)
        {
            IInteractable interactable = interactor.hit.collider.GetComponent<IInteractable>();

            if(interactable != null)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (NewUIManager.Instance.isPaused == false)
            {
                NewUIManager.Instance.PauseGame();
            }
            else
            {
                NewUIManager.Instance.ResumeGame();
            }
        }
    }
}
