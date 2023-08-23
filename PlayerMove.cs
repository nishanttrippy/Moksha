using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected Vector3 dashDirection;
    protected RaycastHit2D hit;
    protected RaycastHit2D angelHit;
    protected float ySpeed = 1.5f;
    protected float xSpeed = 1.75f;
    private Animator playerAnim;
    private PlayerCombat pc;

    //Attack Params
    private float dashCooldown = 0.75f;
    private float lastDash;
    public bool isDashing = false;
    private float dashFactor = 1.0f;

    private int isFacingRight = 1;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        pc = GetComponent<PlayerCombat>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));

        if(x != 0 || y != 0)
        {
            playerAnim.SetBool("isMoving", true);
        }
        else
        {
            playerAnim.SetBool("isMoving", false);
        }
    }
    private void Update()
    {
        if(Time.time - lastDash > dashCooldown)
        {
            isDashing = false;
            dashFactor = 1.0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(Time.time - lastDash > dashCooldown)
            {
                playerAnim.SetTrigger("Dash");
                dashFactor = 3.0f;
                isDashing = true;
                lastDash = Time.time;
                Physics2D.IgnoreLayerCollision(6, 9);
            }
        }
    }

    private void UpdateMotor(Vector3 input)
    {
        // Reset MoveDelta
        moveDelta = new Vector3(input.x * xSpeed * dashFactor, input.y * ySpeed * dashFactor, 0);

        //Swap sprite direction
        if (moveDelta.x > 0 && isFacingRight == -1)
        {
            transform.Rotate(0f, 180f, 0f);
            isFacingRight *= -1;
        }
        else if (moveDelta.x < 0 && isFacingRight == 1)
        {
            transform.Rotate(0f, 180f, 0f);
            isFacingRight *= -1;
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
            transform.Translate(moveDelta.x * Time.deltaTime * isFacingRight, 0, 0);
        }
    }
}
