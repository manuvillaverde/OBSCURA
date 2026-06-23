using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float chaseRange = 15f;
    public float attackRange = 2f;

    public bool isInLight;

    private NavMeshAgent agent;
    private Animator animator;

    private float attackCooldown = 1.5f;
    private float lastAttackTime;

    private bool attackLocked;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        agent.stoppingDistance = 0.5f;
    }

    void Update()
    {
        if (!player || !agent || !animator) return;

        float distance = Vector3.Distance(transform.position, player.position);

        animator.SetFloat("velocity", agent.velocity.magnitude);

        // 💡 LIGHT
        if (isInLight)
        {
            StopMovement();
            return;
        }

        // ⚔️ ATTACK
        if (distance <= attackRange)
        {
            Attack();
            return;
        }

        // reset lock si salió del rango
        attackLocked = false;

        // 🟡 CHASE
        if (distance <= chaseRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            StopMovement();
        }
    }

    void Attack()
    {
        StopMovement();

        if (attackLocked)
            return;

        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;
        attackLocked = true;

        animator.SetTrigger("attack");
    }

    void StopMovement()
    {
        if (agent == null) return;

        agent.isStopped = true;
        agent.ResetPath();
    }

    // 💀 LLAMADO DESDE ANIMATION EVENT (ÚLTIMO FRAME DEL ATTACK)
    public void EndAttack()
    {
        attackLocked = false;
    }
}