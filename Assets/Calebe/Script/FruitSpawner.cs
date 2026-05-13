using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [Header("Configuração")]
    public GameObject fruitPrefab;
    public Transform spawnPoint;
    public float respawnTime = 2f;

    private GameObject currentFruit;

    void Start()
    {
        SpawnFruit();
    }

    void Update()
    {
        if (currentFruit == null)
        {
            Invoke(nameof(SpawnFruit), respawnTime);
        }
    }

    private void SpawnFruit()
    {
        if (currentFruit == null)
        {
            currentFruit = Instantiate(
                fruitPrefab,
                spawnPoint.position,
                spawnPoint.rotation
            );
        }
    }
}