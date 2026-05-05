using UnityEngine;
using UnityEngine.AI;

public class Cliente : MonoBehaviour
{
    public Transform[] pontosAtendimento;
    public Transform direcaoBalcao;

    private NavMeshAgent agent;
    private Transform alvo;
    private bool chegou = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        int i = Random.Range(0, pontosAtendimento.Length);
        alvo = pontosAtendimento[i];

        agent.stoppingDistance = 1.0f; // aumenta pra não invadir
        agent.updateRotation = false;

        agent.SetDestination(alvo.position);
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        // SÓ gira com o movimento se ainda não chegou
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

            // trava posição (evita deslize)
            agent.velocity = Vector3.zero;

            // gira pro balcão (AGORA sim)
            if (direcaoBalcao != null)
            {
                transform.rotation = direcaoBalcao.rotation;
            }
        }
    }
}