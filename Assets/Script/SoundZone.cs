using UnityEngine;
using System.Collections;

public class AudioZoneFade : MonoBehaviour
{
    public AudioSource ambienteAudio;

    [Range(0f, 1f)]
    public float volumenDentro = 0.2f;

    [Range(0f, 1f)]
    public float volumenFuera = 1f;

    public float duracionFade = 2f;

    private Coroutine fadeCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IniciarFade(volumenDentro);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IniciarFade(volumenFuera);
        }
    }

    void IniciarFade(float volumenObjetivo)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeVolumen(volumenObjetivo));
    }

    IEnumerator FadeVolumen(float objetivo)
    {
        float volumenInicial = ambienteAudio.volume;
        float tiempo = 0f;

        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;

            ambienteAudio.volume = Mathf.Lerp(
                volumenInicial,
                objetivo,
                tiempo / duracionFade
            );

            yield return null;
        }

        ambienteAudio.volume = objetivo;
    }
}
