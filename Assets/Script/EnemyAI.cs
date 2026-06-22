using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float chaseRange = 15f;
    public float attackRange = 2f;

    public bool isInLight = false;

    private NavMeshAgent agent;

    [SerializeField] private Animator _animator;

    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
    }

    void Update()
    {
        if (player == null) return;

        if (!agent.isOnNavMesh) return;

        _animator.SetFloat("velocity", agent.velocity.magnitude);

        if (isInLight)
        {
            agent.isStopped = true;
            agent.ResetPath();

            isAttacking = false;

            _animator.ResetTrigger("Attack");

            return;
        }

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance <= attackRange)
        {
            agent.isStopped = true;

            if (!isAttacking)
            {
                isAttacking = true;
                _animator.SetTrigger("Attack");
            }

            return;
        }

        isAttacking = false;
        agent.isStopped = false;

        if (distance <= chaseRange)
        {
            agent.SetDestination(player.position);
        }
    }
}