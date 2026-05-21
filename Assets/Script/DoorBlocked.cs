using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DoorBlocked : MonoBehaviour
{
    public GameObject door;
    public TextMeshProUGUI interactText;

    private bool _playerInRange = false;
    private PlayerInventory _inventory;

    void Start()
    {
        if (interactText != null)
            interactText.text = "";
    }

    void Update()
    {
        if (_playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (_inventory != null && _inventory.hasKey)
            {
                Debug.Log("Puerta abierta!");

                Destroy(door);

                if (interactText != null)
                    interactText.text = "";

                Destroy(gameObject);
            }
            else
            {
                if (interactText != null)
                    interactText.text = "You need a key";

                Debug.Log("Necesitas una llave");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;

            _inventory = other.GetComponent<PlayerInventory>();

            if (interactText != null)
                interactText.text = "Press E to open";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;

            if (interactText != null)
                interactText.text = "";
        }
    }
}