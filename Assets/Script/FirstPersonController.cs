 
using UnityEngine;
using UnityEngine.Rendering;

public class FirstPersonM : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensivity = 2f;
    public float gravity = -9.81f;

    CharacterController controller;
    Vector3 velocity;
    float xRotation = 0f;

    public Transform playerCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity;

        xRotation = xRotation - mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        //Teclado
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //Gravedad 
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y = velocity.y + (gravity * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
}
