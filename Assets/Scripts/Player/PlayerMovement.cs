using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerSFXManager SFXManager;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;

    private Vector2 velocity = Vector2.zero;

    // Directional input from the player
    private Vector2 movementDirections;

    public float dashDuration = 0.8f;
    public float dashCooldown = 1.2f;

    // Speed during dash
    public float dashSpeed = 7f;

    // Regular movement speed
    public float speed = 4f;

    public bool rolling = false;

    // Current movement speed
    private float currentSpeed;

    private bool canDash = true;

    void Start()
    {
        SFXManager = GetComponent<PlayerSFXManager>();
        currentSpeed = speed;
        // Subscribe to the playerDied event
        PlayerStateTracker.playerDied += PlayOnDeath;
    }
    
    void Update()
    {
        if (!GameStateManager.gamePaused)
        {
            GetDirectionalInput();
            HandleAnimation();
            FlipToMouse();
            ManageDash();
        }
    }

    private void FixedUpdate()
    {
        if (!GameStateManager.gamePaused)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        // Calculate the velocity based on input and current speed
        velocity = movementDirections.normalized * currentSpeed;
        rb.velocity = velocity;
    }

    // Get horizontal and vertical input from the player
    private void GetDirectionalInput()
    {
        movementDirections.x = Input.GetAxisRaw("Horizontal");
        movementDirections.y = Input.GetAxisRaw("Vertical");
    }

    private IEnumerator Dash()
    {
        // Start dashing animation and disable vulnerability during dash
        rolling = true;
        PlayerStateTracker.playerVulnerable = false;
        SFXManager.PlayRollSFX();
        animator.SetBool("dashing", true);
        currentSpeed = dashSpeed;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        // Enable vulnerability after the dash, stop dashing animation, and reset speed
        PlayerStateTracker.playerVulnerable = true;
        rolling = false;
        animator.SetBool("dashing", false);
        currentSpeed = speed;

        // Start dash cooldown
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    private void ManageDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    // Manage player animations based on movement
    private void HandleAnimation()
    {
        animator.SetBool("running", velocity != Vector2.zero);
    }

    private void FlipToMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Flip the sprite if the mouse is to the left of the player
        sprite.flipX = mousePosition.x < transform.position.x;
    }

    // Handle actions to be taken when the player dies
    private void PlayOnDeath()
    {
        SFXManager.PlayGameOver();
        animator.SetTrigger("Died");
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
}

