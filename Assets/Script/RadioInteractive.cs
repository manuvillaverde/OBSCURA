using UnityEngine;
using TMPro;

public class RadioInteractable : MonoBehaviour
{
    public AudioSource audioSource;
    public Light radioLight;
    public TextMeshProUGUI pressText;

    private bool playerInRange = false;
    private bool hasPlayed = false;
    private bool hasFinished = false;

    void Start()
    {
        radioLight.color = Color.red;
        pressText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (hasFinished) return;

        if (playerInRange && !hasPlayed)
        {
            pressText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayRadio();
            }
        }

        if (hasPlayed && !audioSource.isPlaying)
        {
            FinishRadio();
        }
    }

    void PlayRadio()
    {
        audioSource.Play();
        hasPlayed = true;

        pressText.gameObject.SetActive(false);
        radioLight.color = Color.green;
    }

    void FinishRadio()
    {
        hasFinished = true;
        pressText.gameObject.SetActive(false);
        radioLight.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasFinished)
        {
            playerInRange = true;

  
            FirstPersonController controller =
                other.GetComponent<FirstPersonController>();

            if (controller != null)
            {
                controller.LockLookForFrame();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            pressText.gameObject.SetActive(false);
        }
    }
}