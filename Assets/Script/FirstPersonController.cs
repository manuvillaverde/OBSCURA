using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonM : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensivity = 500f;
    public float gravity = -9.81f;

    public float lookSmooth = 15f;

    CharacterController controller;
    Vector3 velocity;
    float xRotation = 0f;

    public Transform playerCamera;

    // NEW INPUT SYSTEM
    PlayerMovement inputActions;
    InputAction moveAction;
    InputAction lookAction;

    Vector2 moveInput;
    Vector2 lookInput;

    Vector2 smoothLookInput;

    void Awake()
    {
        inputActions = new PlayerMovement();

        moveAction = inputActions.FindAction("Move", true);
        lookAction = inputActions.FindAction("Look", true);
    }

    void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerMovement();
            moveAction = inputActions.FindAction("Move", true);
            lookAction = inputActions.FindAction("Look", true);
        }

        inputActions.Enable();
    }

    void OnDisable()
    {
        if (inputActions != null)
            inputActions.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        lookInput = lookAction.ReadValue<Vector2>();

        smoothLookInput = Vector2.Lerp(smoothLookInput, lookInput, lookSmooth * Time.deltaTime);

        float mouseX = smoothLookInput.x * mouseSensivity * Time.deltaTime;
        float mouseY = smoothLookInput.y * mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        Vector3 move = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}