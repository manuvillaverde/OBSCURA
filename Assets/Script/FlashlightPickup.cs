using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    [Header("UI")]
    public GameObject interactionText;
    public GameObject chargeSlider;

    [Header("Flashlights")]
    public GameObject groundFlashlight;
    public GameObject heldFlashlight;

    private bool canPickup = false;

    void Start()
    {
        interactionText.SetActive(false);

        
        heldFlashlight.SetActive(false);

       
        chargeSlider.SetActive(false);
    }

    void Update()
    {
        if (!canPickup)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            
            heldFlashlight.SetActive(true);

            
            chargeSlider.SetActive(true);

            
            groundFlashlight.SetActive(false);

            
            interactionText.SetActive(false);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        canPickup = true;
        interactionText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        canPickup = false;
        interactionText.SetActive(false);
    }
}