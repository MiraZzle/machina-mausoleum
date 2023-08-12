using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;

    public float dashCoolCounter = 0f;
    public float dashCounter = 0f;
    public float dashDuration = 0.8f;
    public float dashCooldown = 3f;

    public bool rolling = false;


    public float dashSpeed = 7f;
    public float speed = 4f;

    private float currentSpeed;

    private Vector2 velocity = Vector2.zero;
    private Vector2 movementDirections;

    void Start()
    {
        currentSpeed = speed;
    }
    
    void Update()
    {
        movementDirections.x = Input.GetAxisRaw("Horizontal");
        movementDirections.y = Input.GetAxisRaw("Vertical");

        velocity = movementDirections.normalized * currentSpeed;

        HandleAnimation();
        FlipToMouse();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                Dash();
            }
        }

        ManageDash();

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = velocity;
    }

    private void Dash()
    {
        animator.SetBool("dashing", true);

        currentSpeed = dashSpeed;
        dashCounter = dashDuration;
        rolling = true;
    }

    private void ManageDash()
    {
        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                currentSpeed = speed;
                dashCoolCounter = dashCooldown;

                animator.SetBool("dashing", false);
                rolling = false;

            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    private void HandleAnimation()
    {
        if (velocity == Vector2.zero)
        {
            animator.SetBool("running", false);
        }
        else
        {
            animator.SetBool("running", true);
        }
    }

    private void FlipToMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (mousePosition.x > transform.position.x)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }
}

