using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;
    [SerializeField]
    float mouseSensitivity = 100;
    [SerializeField]
    float jumpHeight = 1.0f; //jump height
    [SerializeField]
    float gravity = -1.5f; //gravity strength
    [SerializeField]
    GameObject cam;
    [SerializeField]
    GameObject bulletSpawner;
    [SerializeField]
    GameObject bullet;

    Vector2 movement;
    Vector2 mouseMovement;
    CharacterController chara;
    float cameraUpRotation = 0;

    private Vector3 velocity; //Stores vertical movement
    private bool isGrounded;
    private float groundCheckTimer = 0f; //Timer to allow early jumps

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        chara = GetComponent<CharacterController>();
    }

    //Update is called once per frame
    void Update()
    {
        isGrounded = chara.isGrounded;

        //Ground check
        if (isGrounded)
        {
            groundCheckTimer = 0.2f; //Allow jumps within this short time after landing
        }
        else
        {
            groundCheckTimer -= Time.deltaTime;
        }

        //Prevent gravity from overriding jump immediately
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //Small downward force to keep the player grounded
        }

        //Camera
        float lookX = mouseMovement.x * Time.deltaTime * mouseSensitivity;
        float lookY = mouseMovement.y * Time.deltaTime * mouseSensitivity;
        cameraUpRotation -= lookY;
        cameraUpRotation = Mathf.Clamp(cameraUpRotation, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(cameraUpRotation,0,0);

        //Movement
        transform.Rotate(Vector3.up * lookX);
        float moveX = movement.x;
        float moveZ = movement.y;
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        chara.Move((move * speed + velocity) * Time.deltaTime);

        //Apply gravity
        velocity.y += gravity * Time.deltaTime;
        chara.Move(velocity * Time.deltaTime);
    }

    void OnMove(InputValue moveVal)
    {
        movement = moveVal.Get<Vector2>();
    }

    void OnLook(InputValue lookVal)
    {
        mouseMovement = lookVal.Get<Vector2>();
    }

    //jump mechanic
    void OnJump()
    {
        if (isGrounded || groundCheckTimer > 0f) //Allow jumping slightly after landing
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -1.0f * gravity);
            groundCheckTimer = 0f; //Reset buffer
        }
    }

    void OnAttack()
    {
        Instantiate(bullet, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
    }
}
