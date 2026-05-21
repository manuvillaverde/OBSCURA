using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class DoorBlocked : MonoBehaviour
{
    public GameObject keyVisual;

    public GameObject door;

    public TextMeshProUGUI interactText;

    private bool _playerInRange = false;
    private bool _openingDoor = false;

    private PlayerInventory _inventory;

    void Start()
    {
        if (interactText != null)
            interactText.text = "";

        if (keyVisual != null)
            keyVisual.SetActive(false);
    }

    void Update()
    {
        if (_openingDoor) return;

        if (_playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (_inventory != null && _inventory.hasKey)
            {
                StartCoroutine(OpenDoorSequence());
            }
            else
            {
                interactText.text = "You need a key";
            }
        }
    }

    IEnumerator OpenDoorSequence()
    {
        _openingDoor = true;

        interactText.text = "";

       
        if (keyVisual != null)
            keyVisual.SetActive(true);

        
        yield return new WaitForSeconds(1.5f);

        Destroy(keyVisual);

      
        Destroy(door);

        yield return new WaitForSeconds(0.2f);

       
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;

            _inventory = other.GetComponent<PlayerInventory>();

            interactText.text = "Press E to open";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;

            interactText.text = "";
        }
    }
}