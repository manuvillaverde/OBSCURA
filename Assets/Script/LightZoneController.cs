using UnityEngine;

public class LightZoneController : MonoBehaviour
{
    public bool lightEnabled = true;

    private void OnTriggerStay(Collider other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>();

        if (enemy != null)
        {
            enemy.isInLight = lightEnabled;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>();

        if (enemy != null)
        {
            enemy.isInLight = false;
        }
    }
}