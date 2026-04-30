using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float _currentHealth;
    public Slider healthSlider;

    void Start()
    {
        _currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = _currentHealth;
        }
    }
    void Update()
    {
        if (healthSlider != null)
            healthSlider.value = _currentHealth;
    }
    public void TakeDamage(float damage)

    {
        _currentHealth -= damage;  
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

        Debug.Log("Vida actual:" +  _currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El jugador murió");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
