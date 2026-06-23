using UnityEngine;

public class LightZoneController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>();

        if (enemy != null)
            enemy.isInLight = true;
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>();

        if (enemy != null)
            enemy.isInLight = false;
    }
}