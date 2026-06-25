using UnityEngine;
using System.Collections;

public class TutorialEnemyScream : MonoBehaviour
{
    public AudioSource audioSource;
    public float delay = 5f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);

        audioSource.Play();
    }
}
