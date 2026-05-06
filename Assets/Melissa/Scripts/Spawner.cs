using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] npcPrefabs;
    public Transform spawnPoint;
    public GerenciadorFila fila;

    public float tempoEntreSpawns = 3f;
    public int maxNPCs = 5;

    private int npcAtivos = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnNPC), 0f, tempoEntreSpawns);
    }

    void SpawnNPC()
    {
        if (npcAtivos >= maxNPCs) return;

        int index = Random.Range(0, npcPrefabs.Length);

        GameObject npc = Instantiate(
            npcPrefabs[index],
            spawnPoint.position,
            Quaternion.identity
        );

        Cliente cliente = npc.GetComponent<Cliente>();

        cliente.spawner = this;
        cliente.fila = fila;

        npcAtivos++;
    }

    public void RemoverNPC()
    {
        npcAtivos--;
    }
}