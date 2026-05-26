using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightPowerDamage : MonoBehaviour
{
    [Header("References")]
    public Light flashlight;
    public Transform playerCamera;

    [Header("Power Mode")]
    public float normalIntensity = 30f;
    public float powerIntensity = 80f;

    [Header("Damage")]
    public float damagePerSecond = 10f;
    public float range = 10f;

    private bool _powerMode = false;

    void Update()
    {
        if (flashlight == null || playerCamera == null) return;

        _powerMode = Mouse.current.rightButton.isPressed;

        if (!flashlight.enabled) return;

        // Intensidad segun modo
        flashlight.intensity = _powerMode
            ? powerIntensity
            : normalIntensity;

        // Daño SOLO en modo power
        if (!_powerMode) return;

        Ray ray = new Ray(
            playerCamera.position,
            playerCamera.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            EnemyHealth enemy =
                hit.collider.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(
                    damagePerSecond * Time.deltaTime
                );
            }
        }
    }

    public bool IsPowerMode()
    {
        return _powerMode;
    }

}