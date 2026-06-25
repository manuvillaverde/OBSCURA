using UnityEngine;
using UnityEngine.SceneManagement;

public class KeypadSceneLoader : MonoBehaviour
{
    public string nextSceneName;
    public float delay = 2f;

    public void LoadNextScene()
    {
        Invoke(nameof(ChangeScene), delay);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}