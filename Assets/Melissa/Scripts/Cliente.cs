using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PontoAtendimento
{
    public Transform posicao;
    public Transform olhar;

    [HideInInspector] public bool ocupado;
}

public class Cliente : MonoBehaviour
{
    public PontoAtendimento[] pontos;
    public Spawner spawner;

    private NavMeshAgent agent;
    private Transform alvo;
    private Transform direcaoBalcao;
    private PontoAtendimento meuPonto;

    private bool chegou = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = 1.0f;
        agent.updateRotation = false;

        EscolherPonto();

        if (alvo != null)
        {
            agent.SetDestination(alvo.position);
        }

        transform.rotation = Quaternion.identity;
    }

    void EscolherPonto()
    {
        foreach (var p in pontos)
        {
            if (!p.ocupado)
            {
                p.ocupado = true;
                meuPonto = p;

                alvo = p.posicao;
                direcaoBalcao = p.olhar;
                return;
            }
        }

        // Se não tem ponto livre, o NPC só desiste da vida
        Destroy(gameObject);
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

            // Simula atendimento e saída depois de um tempo
            Invoke(nameof(Sair), Random.Range(3f, 6f));
        }

        // OLHAR PRO BALCÃO
        if (chegou && direcaoBalcao != null)
        {
            Vector3 direcao = direcaoBalcao.position - transform.position;
            direcao.y = 0;

            Quaternion rotacaoAlvo = Quaternion.LookRotation(direcao);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, Time.deltaTime * 5f);
        }
    }

    void Sair()
    {
        if (meuPonto != null)
        {
            meuPonto.ocupado = false;
        }

        if (spawner != null)
        {
            spawner.RemoverNPC();
        }

        Destroy(gameObject);
    }
}