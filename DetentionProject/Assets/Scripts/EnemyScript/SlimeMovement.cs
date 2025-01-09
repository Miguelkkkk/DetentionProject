using UnityEngine;
using UnityEngine.AI;

public class SlimeMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform playerTarget;   

    void Awake()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent não encontrado! Adicione o componente ao inimigo.");
        }
    }

    void Update()
    {
        if (playerTarget != null)
        {
            navMeshAgent.SetDestination(playerTarget.position);
        }
    }
}

