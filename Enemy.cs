using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    private Animator enemyAnim;

    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    private float ySpeed = 0.5f;
    private float xSpeed = 0.75f;

    public Transform playerTransform;
    private Transform selfTransform;
    private Vector3 startingPosition;
    private bool isChasing;
    private bool isCollidingWithPlayer;
    public float triggerLength = 1.0f;
    public float chaseLength = 5.0f;

    //hitbox
    public ContactFilter2D filter;
    private Collider2D[] hits = new Collider2D[10];

    //Combat
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    float attackCooldown = 1.0f;
    float lastSwing;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemyAnim = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startingPosition = transform.position;

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        enemyAnim.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Death");

        enemyAnim.SetBool("isDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        Destroy(gameObject);
    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null && colInfo.tag == "Player")
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(10);
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyAnim.SetBool("isMoving", false);

        if (Time.time - lastSwing > attackCooldown)
        {
            if (Vector2.Distance(playerTransform.position, transform.position) <= attackRange)
            {
                enemyAnim.SetTrigger("Attack");
                lastSwing = Time.time;
            }
        }

        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
            {
                isChasing = true;
            }

            if (isChasing)
            {
                if (!isCollidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            isChasing = false;
        }

        //Check for overlap
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
    }

    public void Rebirth()
    {
        GetComponent<Collider2D>().enabled = true;
        this.enabled = true;
    }

    private void UpdateMotor(Vector3 input)
    {
        // Reset MoveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //Swap sprite direction
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //Make that thang move!
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
            enemyAnim.SetBool("isMoving", true);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //Make that thang move!
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
            enemyAnim.SetBool("isMoving", true);
        }
    }

}
