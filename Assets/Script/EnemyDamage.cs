using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float detectionRadius = 2f;

    public void checkForPlayer()
    {
        Debug.Log("Checkear for player");

        Collider[] hits =
            Physics.OverlapSphere(
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

        Gizmos.DrawWireSphere(
            transform.position,
            detectionRadius
        );
    }
}