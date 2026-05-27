using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public bool hasHeardRadio = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}