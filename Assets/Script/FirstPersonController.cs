using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensivity = 500f;
    public float gravity = -9.81f;

    public float lookSmooth = 15f;

    CharacterController _controller;
    Vector3 _velocity;
    float _xRotation = 0f;

    public Transform playerCamera;

    // NEW INPUT SYSTEM
    PlayerMovement _inputActions;
    InputAction _moveAction;
    InputAction _lookAction;

    Vector2 _moveInput;
    Vector2 _lookInput;

    Vector2 _smoothLookInput;

    void Awake()
    {
        _inputActions = new PlayerMovement();

        _moveAction = _inputActions.FindAction("Move", true);
        _lookAction = _inputActions.FindAction("Look", true);
    }

    void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerMovement();
            _moveAction = _inputActions.FindAction("Move", true);
            _lookAction = _inputActions.FindAction("Look", true);
        }

        _inputActions.Enable();
    }

    void OnDisable()
    {
        if (_inputActions != null)
            _inputActions.Disable();
    }

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        _lookInput = _lookAction.ReadValue<Vector2>();

        _smoothLookInput = Vector2.Lerp(_smoothLookInput, _lookInput, lookSmooth * Time.deltaTime);

        float mouseX = _smoothLookInput.x * mouseSensivity * Time.deltaTime;
        float mouseY = _smoothLookInput.y * mouseSensivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

        playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        Vector3 move = (transform.right * _moveInput.x + transform.forward * _moveInput.y).normalized;
        _controller.Move(move * speed * Time.deltaTime);

        if (_controller.isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}