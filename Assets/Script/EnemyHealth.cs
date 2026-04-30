using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f;
    private float health;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Enemy HP: " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}