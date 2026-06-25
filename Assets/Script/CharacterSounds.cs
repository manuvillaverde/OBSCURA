using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    public AudioSource footstepSource;

    public AudioClip woodStep;
    public AudioClip grassStep;
    public AudioClip concreteStep;

    public AudioClip currentFootstep;

    public float stepInterval = 0.5f;

    private CharacterController controller;
    private float stepTimer;

    private Vector3 inputMove;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentFootstep = concreteStep;
    }

    void Update()
    {
        if (controller == null) return;

        
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        inputMove = new Vector3(x, 0, z);

        bool isMoving = inputMove.magnitude > 0.1f;

        if (controller.isGrounded && isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                footstepSource.clip = currentFootstep;
                footstepSource.Play();

                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;

          
            if (footstepSource.isPlaying)
                footstepSource.Stop();
        }
    }

    public void SetGrass() => currentFootstep = grassStep;
    public void SetConcrete() => currentFootstep = concreteStep;
    public void SetWood() => currentFootstep = woodStep;
}