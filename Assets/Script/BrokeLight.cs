using UnityEngine;

public class LanternFlicker : MonoBehaviour
{
    Light _light;

    void Start()
    {
        _light = GetComponent<Light>();
    }

    void Update()
    {
        _light.intensity = Random.Range(7f, 100f);
    }
}