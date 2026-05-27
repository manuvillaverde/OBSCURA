using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DoorInteractable : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI pressText;

    [Header("Scene")]
    public string sceneToLoad;

    [Header("Look Target")]
    public Transform radioLookTarget;
    public float lookTime = 2f;

    private bool playerInRange = false;
    private bool isBusy = false;

    void Start()
    {
        if (pressText != null)
            pressText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isBusy) return;

        if (playerInRange)
        {
            pressText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!GameState.Instance.hasHeardRadio)
                {
                    StartCoroutine(LookAtRadioEvent());
                }
                else
                {
                    OpenDoor();
                }
            }
        }
    }

    System.Collections.IEnumerator LookAtRadioEvent()
    {
        isBusy = true;
        pressText.gameObject.SetActive(false);

        FirstPersonController player =
            FindObjectOfType<FirstPersonController>();

        if (player != null && radioLookTarget != null)
        {
            player.LockLookForFrame();

            Vector3 dir = radioLookTarget.position - player.transform.position;
            dir.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(dir);

            float t = 0f;
            Quaternion startRot = player.transform.rotation;

            while (t < lookTime)
            {
                t += Time.deltaTime;

                player.transform.rotation =
                    Quaternion.Slerp(startRot, targetRotation, t / lookTime);

                yield return null;
            }
        }

        isBusy = false;
    }

    void OpenDoor()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
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