using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform spawnPoint;

    public float tempoEntreSpawns = 3f;
    public int maxNPCs = 5;

    public int npcAtivos = 0;

    void Start()
    {
        InvokeRepeating("SpawnNPC", 0f, tempoEntreSpawns);
    }

    void SpawnNPC()
    {
        if (npcAtivos >= maxNPCs) return;

        GameObject npc = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);

        Cliente cliente = npc.GetComponent<Cliente>();
        cliente.spawner = this;

        npcAtivos++;
    }

    public void RemoverNPC()
    {
        npcAtivos--;
    }
}