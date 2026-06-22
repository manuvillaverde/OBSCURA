using UnityEngine;

public class NoteInteractable : MonoBehaviour
{
    [Header("UI")]
    public GameObject interactionText; 
    public GameObject panel;           

    private bool playerNear = false;


    void Start()
    {
        if (interactionText != null)
            interactionText.SetActive(false);

        if (panel != null)
            panel.SetActive(false);
    }

    void Update()
    {
        if (!playerNear) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (panel == null) return;

            bool newState = !panel.activeSelf;
            panel.SetActive(newState);

            if (interactionText != null)
                interactionText.SetActive(!newState);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerNear = true;

        if (interactionText != null)
            interactionText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerNear = false;

        if (interactionText != null)
            interactionText.SetActive(false);

        if (panel != null)
            panel.SetActive(false);
    }
}