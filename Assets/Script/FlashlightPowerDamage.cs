using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightPowerDamage : MonoBehaviour
{
    [Header("References")]
    public Light flashlight;
    public Transform playerCamera;

    [Header("Power Mode")]
    public float normalIntensity = 30f;
    public float powerIntensity = 60f;

    public float damagePerSecond = 10f;
    public float range = 10f;

    private bool _powerMode = false;

    void Update()
    {
        if (flashlight == null || playerCamera == null) return;

        _powerMode = Mouse.current.rightButton.isPressed;

        if (!flashlight.enabled ) return;

        flashlight.intensity = _powerMode ? normalIntensity : powerIntensity;

        if (!_powerMode) return;

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
    

