using UnityEngine;
using UnityEngine.Video;

public class CameraInteraction : MonoBehaviour
{
    public GameObject interactionText;

    public GameObject groundCamera;
    public GameObject heldCamera;

    public VideoPlayer videoPlayer;

    public FirstPersonController player;

    bool canInteract = false;
    bool watchingVideo = false;

    void Start()
    {
        interactionText.SetActive(false);

        heldCamera.SetActive(false);

        videoPlayer.loopPointReached += VideoFinished;
    }

    void Update()
    {
        if (!canInteract)
            return;

        if (watchingVideo)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartVideo();
        }
    }

    void StartVideo()
    {
        watchingVideo = true;

        interactionText.SetActive(false);

        player.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        groundCamera.SetActive(false);

        heldCamera.SetActive(true);

        videoPlayer.Play();
    }

    void VideoFinished(VideoPlayer vp)
    {
        heldCamera.SetActive(false);

        player.enabled = true;

        watchingVideo = false;

        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            interactionText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (watchingVideo)
            return;

        if (other.CompareTag("Player"))
        {
            canInteract = false;
            interactionText.SetActive(false);
        }
    }
}