using UnityEngine;
using TMPro;

public class FloatingNote : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text messageText;

    private bool isOpen = false;

    void Update()
    {
        if (isOpen &&
            (Input.GetKeyDown(KeyCode.E) ||
             Input.GetKeyDown(KeyCode.Escape)))
        {
            HideMessage();
        }
    }

    public void ShowMessage(string message)
    {
        panel.SetActive(true);
        messageText.text = message;

        isOpen = true;
        Time.timeScale = 0f;
    }

    public void HideMessage()
    {
        panel.SetActive(false);

        isOpen = false;
        Time.timeScale = 1f;
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}