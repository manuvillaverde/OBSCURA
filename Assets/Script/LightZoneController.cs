using UnityEngine;

public class LightZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>();

        if (enemy != null)
        {
            enemy.isInLight = true;
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

    private void OnDisable()
    {
        EnemyAI[] enemies = FindObjectsByType<EnemyAI>(
            FindObjectsSortMode.None
        );

        foreach (EnemyAI enemy in enemies)
        {
            enemy.isInLight = false;
        }
    }
}