using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;
    public Transform spawnPoint;
    public float respawnTime = 2f;

    private GameObject currentFruit;
    private bool isRespawning;

    void Start()
    {
        SpawnFruit();
    }

    void Update()
    {
        if (currentFruit == null && !isRespawning)
        {
            isRespawning = true;
            Invoke(nameof(SpawnFruit), respawnTime);
        }
    }

    private void SpawnFruit()
    {
        currentFruit = Instantiate(
            fruitPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        isRespawning = false;
    }
}