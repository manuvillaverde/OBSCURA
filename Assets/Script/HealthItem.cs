using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class HealItem : MonoBehaviour
{
    public float healAmount = 25f;
    public TextMeshProUGUI healText;

    private bool _playerInRange = false;
    private PlayerHealth _playerHealth;

    void Start()
    {
        if (healText != null)
            healText.alpha = 0;
    }

    void Update()
    {
        if (_playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (_playerHealth != null)
            {
                _playerHealth.Heal(healAmount);

                if (healText != null)
                    healText.alpha = 0;

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
            _playerHealth = other.GetComponent<PlayerHealth>();

            if (healText != null)
                healText.alpha = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;

            if (healText != null)
                healText.alpha = 0;
        }
    }
}