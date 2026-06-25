using UnityEngine;

public class FactoryEntrance : MonoBehaviour
{
    public Transform door;          
    public Vector3 closedRotation;  
    public float speed = 2f;

    public AudioSource audioSource;
    public AudioClip closeSound;

    private bool activated = false;
    private bool closing = false;

    private Quaternion startRot;
    private Quaternion targetRot;

    private void Start()
    {
        if (door != null)
            startRot = door.rotation;

        targetRot = Quaternion.Euler(closedRotation);
    }

    private void Update()
    {
        if (closing && door != null)
        {
            door.rotation = Quaternion.Slerp(
                door.rotation,
                targetRot,
                Time.deltaTime * speed
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;
            closing = true;

            if (audioSource != null && closeSound != null)
                audioSource.PlayOneShot(closeSound);
        }
    }
}