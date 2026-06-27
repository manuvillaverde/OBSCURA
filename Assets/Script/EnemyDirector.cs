using UnityEngine;
using System.Collections;

public class EnemyDirector : MonoBehaviour
{
    [Header("Referencias")]
    public EnemyAI enemy;
    public Transform[] spawnPoints;
    public Camera playerCamera;

    [Header("Tiempos")]
    public float minDisappearTime = 25f;
    public float maxDisappearTime = 40f;

    public float respawnDelay = 2f;

    [Header("Distancia mínima")]
    public float minSpawnDistance = 12f;

    void Start()
    {
        StartCoroutine(DirectorRoutine());
    }

    IEnumerator DirectorRoutine()
    {
        while (!EnemyAI.tutorialCompleted)
            yield return null;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDisappearTime, maxDisappearTime));

            while (IsVisible(enemy.transform))
                yield return null;

            enemy.gameObject.SetActive(false);

            Transform spawn = GetRandomSpawn();

            if (spawn != null)
                enemy.transform.position = spawn.position;

            yield return new WaitForSeconds(respawnDelay);

            enemy.gameObject.SetActive(true);
        }
    }

    Transform GetRandomSpawn()
    {
        Transform[] shuffled = (Transform[])spawnPoints.Clone();

        for (int i = 0; i < shuffled.Length; i++)
        {
            int rnd = Random.Range(i, shuffled.Length);

            Transform tmp = shuffled[i];
            shuffled[i] = shuffled[rnd];
            shuffled[rnd] = tmp;
        }

        foreach (Transform point in shuffled)
        {
            float dist = Vector3.Distance(playerCamera.transform.position, point.position);

            if (dist < minSpawnDistance)
                continue;

            if (!IsVisible(point))
                return point;
        }

        return null;
    }

    bool IsVisible(Transform target)
    {
        Vector3 viewport = playerCamera.WorldToViewportPoint(target.position);

        if (viewport.z < 0)
            return false;

        if (viewport.x < 0 || viewport.x > 1)
            return false;

        if (viewport.y < 0 || viewport.y > 1)
            return false;

        Vector3 dir = target.position - playerCamera.transform.position;

        if (Physics.Raycast(playerCamera.transform.position, dir.normalized, out RaycastHit hit, dir.magnitude))
        {
            return hit.transform == target;
        }

        return false;
    }
}