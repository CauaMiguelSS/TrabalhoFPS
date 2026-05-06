using UnityEngine;
using UnityEngine.AI;

public class Cliente : MonoBehaviour
{
    public GerenciadorFila fila;
    public Spawner spawner;

    [Header("Espera")]
    public float tempoMaxEspera = 10f;

    [Header("Ataque")]
    public float distanciaAtaque = 2f;
    public float velocidadeIrritado = 6f;
    public int dano = 10;

    private NavMeshAgent agent;
    private Transform player;

    private PontoAtendimento meuPonto;
    private Transform alvo;
    private Transform direcaoBalcao;

    private bool chegou = false;
    private bool irritado = false;

    private float tempoAtual = 0f;
    private float tempoEntreAtaques = 1.5f;
    private float proximoAtaque = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        agent.stoppingDistance = 1f;
        agent.updateRotation = false;

        EscolherPonto();

        if (alvo != null)
            agent.SetDestination(alvo.position);
    }

    void EscolherPonto()
    {
        meuPonto = fila.PegarPontoLivre();

        if (meuPonto != null)
        {
            alvo = meuPonto.posicao;
            direcaoBalcao = meuPonto.olhar;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!irritado)
            ComportamentoFila();
        else
            ComportamentoAtaque();
    }

    void ComportamentoFila()
    {
        if (!chegou && agent.desiredVelocity.sqrMagnitude > 0.01f)
        {
            Vector3 dir = agent.desiredVelocity.normalized;
            dir.y = 0;

            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10f);
        }

        if (!chegou && agent.remainingDistance <= agent.stoppingDistance)
        {
            chegou = true;
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        if (chegou && direcaoBalcao != null)
        {
            Vector3 dir = direcaoBalcao.position - transform.position;
            dir.y = 0;

            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);

            tempoAtual += Time.deltaTime;

            if (tempoAtual >= tempoMaxEspera)
                FicarIrritado();
        }
    }

    void FicarIrritado()
    {
        irritado = true;

        if (meuPonto != null)
            fila.LiberarPonto(meuPonto);

        agent.isStopped = false;
        agent.speed = velocidadeIrritado;
        agent.stoppingDistance = distanciaAtaque;
    }

    void ComportamentoAtaque()
    {
        if (player == null) return;

        agent.SetDestination(player.position);

        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;

        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10f);

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= distanciaAtaque)
            Atacar();
    }

    void Atacar()
    {
        if (Time.time >= proximoAtaque)
        {
            proximoAtaque = Time.time + tempoEntreAtaques;

            Debug.Log("NPC atacando player");
        }
    }

    public void Sair()
    {
        if (meuPonto != null)
            fila.LiberarPonto(meuPonto);

        if (spawner != null)
            spawner.RemoverNPC();

        Destroy(gameObject);
    }
}