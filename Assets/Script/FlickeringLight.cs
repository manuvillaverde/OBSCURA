using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour
{
    [Header("Referencias")]
    public Light spotLight;
    public GameObject lightZone;

    [Header("Tiempos")]
    public float onTime = 4f;
    public float offTime = 3f;

    [Header("Desfase Inicial")]
    public float startDelay = 0f;

    private void Start()
    {
        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            // Encender
            spotLight.enabled = true;

            if (lightZone != null)
                lightZone.SetActive(true);

            yield return new WaitForSeconds(onTime);

            // Apagar
            spotLight.enabled = false;

            if (lightZone != null)
                lightZone.SetActive(false);

            yield return new WaitForSeconds(offTime);
        }
    }
}
