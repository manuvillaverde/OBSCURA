using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5f;

    public float mouseSensivity = 500f;
    public float gamepadSensivity = 30f;

    public float gravity = -9.81f;
    public float lookSmooth = 10f;

    public Slider batterySlider;

    CharacterController _controller;
    Vector3 _velocity;
    float _xRotation = 0f;

    [Header("Flashlight")]
    [SerializeField] private Light _flashlight;
    public float maxBattery = 100f;
    public float batteryConsumedPerSecond = 1f;
    public float batteryRechargePerSecond = 10f;

    private float _currentBattery;
    private bool _isRecharging;

    public Transform playerCamera;

    [SerializeField] private TextMeshProUGUI _chargeText;
    private bool _inChargeZone = false;
    private bool _charging = false;


    // New Input System
    PlayerMovement _inputActions;
    InputAction _moveAction;
    InputAction _lookAction;
    InputAction _flashlightAction;

    Vector2 _moveInput;
    Vector2 _lookInput;
    Vector2 _smoothLookInput;

    void Awake()
    {
        _inputActions = new PlayerMovement();

        _moveAction = _inputActions.FindAction("Move", true);
        _lookAction = _inputActions.FindAction("Look", true);
        _flashlightAction = _inputActions.FindAction("Flashlight", true);
    }

    void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerMovement();

            _moveAction = _inputActions.FindAction("Move", true);
            _lookAction = _inputActions.FindAction("Look", true);
            _flashlightAction = _inputActions.FindAction("Flashlight", true);
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

        _chargeText.alpha = 0;

        _currentBattery = maxBattery;

        batterySlider.maxValue = maxBattery;

        // Linterna apagada al inicio
        if (_flashlight != null)
            _flashlight.enabled = false;
    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        _lookInput = _lookAction.ReadValue<Vector2>();

        if (_inChargeZone && Keyboard.current.eKey.wasPressedThisFrame)
        {
            _charging = true;
        }

        if (_inChargeZone == false)
        {
            _charging = false;
        }

        batterySlider.value = _currentBattery;

        FlashlightAction();
        FlashlightBatterySystem();

        LookActions();
        MoveActions();
    }

    private void MoveActions()
    {
        Vector3 move = (transform.right * _moveInput.x + transform.forward * _moveInput.y).normalized;
        _controller.Move(move * speed * Time.deltaTime);

        if (_controller.isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void LookActions()
    {
        _smoothLookInput = Vector2.Lerp(_smoothLookInput, _lookInput, lookSmooth * Time.deltaTime);

        float sens = (Gamepad.current != null) ? gamepadSensivity : mouseSensivity;

        float lookX = _smoothLookInput.x * sens * Time.deltaTime;
        float lookY = _smoothLookInput.y * sens * Time.deltaTime;

        _xRotation -= lookY;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

        playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * lookX);
    }

    private void FlashlightAction()
    {
        if (_flashlightAction.WasPressedThisFrame())
        {
            if (_flashlight != null)
            {
                // Solo se puede prender si hay bateria
                if (!_flashlight.enabled && _currentBattery > 0)
                {
                    _flashlight.enabled = true;
                }
                else
                {
                    _flashlight.enabled = false;
                }
            }
        }
    }

    private void FlashlightBatterySystem()
    {
        if (_flashlight == null) return;

        // Consume bateria si esta prendida
        if (_flashlight.enabled)
        {
            _currentBattery -= batteryConsumedPerSecond * Time.deltaTime;
        }

        // Recarga bateria si estas en zona de recarga
        if (_charging)
        {
            _currentBattery += batteryRechargePerSecond * Time.deltaTime;
        }

        // Limites
        _currentBattery = Mathf.Clamp(_currentBattery, 0, maxBattery);

        // Si se queda sin bateria, se apaga sola
        if (_currentBattery <= 0)
        {
            _flashlight.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bateria"))
        {
            _inChargeZone = true;
            _chargeText.alpha = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bateria"))
        {
            _inChargeZone = false;
            _charging = false;
            _chargeText.alpha = 0;
        }
    }
}