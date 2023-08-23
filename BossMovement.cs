using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : StateMachineBehaviour
{
    Vector3 moveDelta;
    Transform transform;
    Transform player;
    private RaycastHit2D hit;
    private BoxCollider2D boxCollider;
    private PlayerCombat pc;
    private bool isCollidingWithPlayer;
    public ContactFilter2D filter;
    private Collider2D[] hits = new Collider2D[10];
    private BossCombat combat;

    float xSpeed = 1.5f;
    float ySpeed = 1.0f;

    float attackCooldown = 2.5f;
    float lastSwing;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.GetComponent<Transform>();
        boxCollider = animator.GetComponent<BoxCollider2D>();
        combat = animator.GetComponent<BossCombat>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isCollidingWithPlayer)
        {
            UpdateMotor((player.position - transform.position).normalized);
        }

        isCollidingWithPlayer = false;

        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].name == "Protagonist_0")
            {
                isCollidingWithPlayer = true;
            }

            //Clean Array
            hits[i] = null;
        }

        if (Time.time - lastSwing > attackCooldown)
        {
            if (Vector2.Distance(player.position, transform.position) <= combat.attackRange)
            {
                animator.SetTrigger("MeleeAttack");
                lastSwing = Time.time;
            }
            else if (Vector2.Distance(player.position, transform.position) >= combat.attackRange + 2.5f)
            {
                combat.CastAttackHandler(animator);
            }
        }
    }

    private void UpdateMotor(Vector3 input)
    {
        // Reset MoveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //Swap sprite direction
        if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking", "Boss"));
        if (hit.collider == null && !pc.isAttacking)
        {
            //Make that thang move!
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking", "Boss"));
        if (hit.collider == null && !pc.isAttacking)
        {
            //Make that thang move!
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("MeleeAttack");
    }
}
