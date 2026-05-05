using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PontoAtendimento
{
    public Transform posicao; // onde o cliente para
    public Transform olhar;   // para onde ele olha
}

public class Cliente : MonoBehaviour
{
    public PontoAtendimento[] pontos;

    private NavMeshAgent agent;
    private Transform alvo;
    private Transform direcaoBalcao;

    private bool chegou = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        int i = Random.Range(0, pontos.Length);

        alvo = pontos[i].posicao;
        direcaoBalcao = pontos[i].olhar;

        agent.stoppingDistance = 1.0f;
        agent.updateRotation = false;

        agent.SetDestination(alvo.position);
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        // ANDANDO
        if (!chegou && agent.desiredVelocity.sqrMagnitude > 0.01f)
        {
            Vector3 direcao = agent.desiredVelocity.normalized;
            direcao.y = 0;

            Quaternion rot = Quaternion.LookRotation(direcao);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10f);
        }

        // CHEGADA
        if (!chegou && agent.remainingDistance <= agent.stoppingDistance)
        {
            chegou = true;
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        // OLHAR PRO BALCÃO (suave)
        if (chegou && direcaoBalcao != null)
        {
            Vector3 direcao = direcaoBalcao.position - transform.position;
            direcao.y = 0;

            Quaternion rotacaoAlvo = Quaternion.LookRotation(direcao);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, Time.deltaTime * 5f);
        }
    }
}