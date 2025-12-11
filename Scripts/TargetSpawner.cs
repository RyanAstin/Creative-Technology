using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;       
    public float spawnInterval = 2f;      
    public Vector3 spawnArea = new Vector3(10, 3, 10);
    // Defines the radius of spawn positions around the spawner.

    bool spawning = false;

    public void StartSpawning()
    {
        spawning = true;

        // Repeatedly call SpawnTarget on a timer
        InvokeRepeating(nameof(SpawnTarget), 0f, spawnInterval);
    }

    public void StopSpawning()
    {
        spawning = false;

        // Stop the repeating function
        CancelInvoke(nameof(SpawnTarget));
    }

    void SpawnTarget()
    {
        if (!spawning) return;

        // Random position within a 3D box region
        Vector3 pos = transform.position +
            new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                Random.Range(0, spawnArea.y),
                Random.Range(-spawnArea.z, spawnArea.z)
            );

        // Spawn the target
        Instantiate(targetPrefab, pos, Quaternion.identity);
    }
}

