using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerBaseState currentState;

    [HideInInspector]
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkState walkState = new PlayerWalkState();
    [HideInInspector]
    public PlayerSneakState sneakState = new PlayerSneakState();
    [HideInInspector]
    public Vector2 movement;

    Vector2 mouseMovement;

    CharacterController controller;
    public float default_speed = 5;

    float cameraUpRotation = 0;

    public bool isSneaking = false;

    private Vector3 velocity; //Stores vertical movement
    [SerializeField]
    GameObject cam;
    [SerializeField]
    float mouseSensitivity = 100;
    [SerializeField]
    GameObject bulletSpawner;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float jumpHeight = 25.0f; //jump height
    [SerializeField]
    float gravity = -100.0f; //gravity strength
    private bool isGrounded;
    float lookX = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();

        SwitchState(idleState);

        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this); 
    }

    // Handle Input //
    void OnMove(InputValue moveVal)
    {
        movement = moveVal.Get<Vector2>();
    }

    void OnLook(InputValue lookVal)
    {
        mouseMovement = lookVal.Get<Vector2>();
    }

    void OnSprint(InputValue sprintVal) //its actually sneaking and this is being used for sneaking for this game
    {
        if (sprintVal.isPressed)
        {
            isSneaking = true;
        }
        else
        {
            isSneaking = false;
        }
    }

    // Helper Function //
    public void MovePlayer(float speed)
    {
        //Movement
        float moveX = movement.x;
        float moveZ = movement.y;
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        Vector3 moveDirection = (move * speed * Time.deltaTime);
        controller.Move(moveDirection + velocity * Time.deltaTime);
    }

    public void moveCamera()
    {
        //Camera
        lookX = mouseMovement.x * Time.deltaTime * mouseSensitivity;
        float lookY = mouseMovement.y * Time.deltaTime * mouseSensitivity;
        cameraUpRotation -= lookY;
        cameraUpRotation = Mathf.Clamp(cameraUpRotation, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(cameraUpRotation,0,0);
        transform.Rotate(Vector3.up * lookX);
    }

    public void Gravity()
    {
        //Ground Check
        isGrounded = controller.isGrounded;

        //Prevent gravity from overriding jump immediately
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f; //Small downward force to keep the player grounded
        }
        velocity.y += gravity * Time.deltaTime;
        Vector3 gravity_movement = new Vector3(0, velocity.y, 0);
        controller.Move(gravity_movement);
    }

    public void SwitchState(PlayerBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    //jump mechanic
    void OnJump()
    {
        if (isGrounded) //Only jump if on the ground
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity); 
        }
    }

    void OnAttack()
    {
        Instantiate(bullet, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
    }

}
