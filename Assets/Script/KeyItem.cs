using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class KeyItem : MonoBehaviour
{
    public TextMeshProUGUI keyText;

    private bool _playerInRange = false;
    private PlayerInventory _inventory;

    void Start()
    {
        if (keyText != null)
            keyText.alpha = 0;
    }

    void Update()
    {
        if (_playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (_inventory != null)
            {
                _inventory.hasKey = true;

                Debug.Log("Llave obtenida!");

                if (keyText != null)
                    keyText.alpha = 0;

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;

            _inventory = other.GetComponent<PlayerInventory>();

            if (keyText != null)
                keyText.alpha = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;

            if (keyText != null)
                keyText.alpha = 0;
        }
    }
}