using UnityEngine;
using NavKeypad;

public class KeypadInteractionFPV : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (!Cursor.visible)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Toque: " + hit.collider.name);

                KeypadButton button =
                    hit.collider.GetComponent<KeypadButton>();

                if (button != null)
                {
                    Debug.Log("BOTON DETECTADO");
                    button.PressButton();
                }
            }
        }
    }
}