using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float detectionRadius;

    public EnemyAI enemyAI;

    public void checkForPlayer()
    {
        if (enemyAI != null && enemyAI.isInLight)
        {
            Debug.Log("Ataque cancelado por luz");
            return;
        }

        Debug.Log("Checkear for player");

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            detectionRadius
        );

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Debug.Log("Jugador detectado");

                PlayerHealth health =
                    hit.GetComponent<PlayerHealth>();

                if (health != null)
                {
                    health.TakeDamage(9999f);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}