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
        if (radioLight != null)
            radioLight.color = Color.red;

        if (pressText != null)
            pressText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (hasFinished) return;

        if (playerInRange && !hasPlayed)
        {
            if (pressText != null)
                pressText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayRadio();
            }
        }

        if (hasPlayed && audioSource != null && !audioSource.isPlaying)
        {
            FinishRadio();
        }
    }

    void PlayRadio()
    {
        if (audioSource == null) return;

        audioSource.Play();
        hasPlayed = true;

        if (pressText != null)
            pressText.gameObject.SetActive(false);

        if (radioLight != null)
            radioLight.color = Color.green;
    }

    void FinishRadio()
    {
        hasFinished = true;

        if (pressText != null)
            pressText.gameObject.SetActive(false);

        if (radioLight != null)
            radioLight.color = Color.red;

        if (GameState.Instance != null)
        {
            GameState.Instance.hasHeardRadio = true;
            Debug.Log("RADIO COMPLETADA");
        }
        else
        {
            Debug.LogError("GameState NO existe en la escena");
        }
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

            if (pressText != null)
                pressText.gameObject.SetActive(false);
        }
    }
}