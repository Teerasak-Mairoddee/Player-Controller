using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackControls : MonoBehaviour
{
    private Animator animator;
    private bool isRunning = false;
    private bool isMoveKeyPressed = false;

    [Tooltip("Adjust this value to control the jump height")]
    [Range(0.0f, 50.0f)]
    public float jumpForce = 5.0f;

    [Tooltip("How long the hitbox is triggered for")]
    [Range(0.0f, 2.0f)]
    public float timing = 0.4f; 

    [Tooltip("How long the parry window is triggered for")]
    [Range(0.0f, 2.0f)]
    public float blockTiming = 0.2f; 

    [Tooltip("Adjust this value to control movement speed")]
    [Range(0.0f, 50.0f)]
    public float moveSpeed = 5f;

    [Tooltip("Adjust this value to check if player is still")]
    [Range(0.0f, 1.0f)]
    public float isMovingThreshold = 0.01f;

    [Tooltip("Adjust this value for horizontal force")]
    [Range(0.0f, 20.0f)]
    public float horizontalForce = 10f; 

    [Tooltip("Adjust how long the roll movement speed increases")]
    [Range(0.0f, 2.0f)]
    public float rollTime = 0.5f; 

    [Tooltip("How Long before player can roll again")]
    [Range(0.0f, 2.0f)]
    public float rollDelay = 0.4f;

    [Tooltip("Movement speed when rolling")]
    [Range(0.0f, 50.0f)]
    public float rollSpeed = 10f;

    [Tooltip("Delay For Animation to take effect for pain")]
    [Range(0.0f, 2.0f)]
    public float hurtDelay = 0.2f;

    private Rigidbody2D rb;

    public string groundTag = "Ground";
    private bool isGrounded;
    private bool canJump;
    private bool isBlocking = false;
    private bool isFrozen = false;
    private bool isAttackLocked = false;
    private bool isRolling = false;

    private bool isFacingRight = true; // Track the direction the sprite is facing

    private KeyCode lastKeyPressed;

    public string[] triggerNames; // Array of trigger names
    private int currentTriggerIndex = 0;

    private bool isMoving = false;

    private HitboxTimings HitboxTimingsReference;
    private ParryTimings ParryTimingsReference;
    private DodgeTimings DodgeTimingsReference;
    private JumpTimings JumpTimingsReference;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to the GameObject
        triggerNames = new string[] { "Sword", "Sword2", "Sword3" };
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        HitboxTimingsReference = GetComponentInChildren<HitboxTimings>();
        ParryTimingsReference = GetComponentInChildren<ParryTimings>();
        DodgeTimingsReference = GetComponentInChildren<DodgeTimings>();
        JumpTimingsReference = GetComponentInChildren<JumpTimings>();

        if (HitboxTimingsReference != null)
        {
            // Call the function from HitboxTimings
            HitboxTimingsReference.FunctionToCall();
        }
        else
        {
            Debug.LogWarning("HitboxTimingsReference reference not found in child GameObject.");
        }

        if (ParryTimingsReference != null)
        {
            // Call the function from ParryTimings
            ParryTimingsReference.FunctionToCall();
        }
        else
        {
            Debug.LogWarning("ParryTimingsReference reference not found in child GameObject.");
        }
        if (DodgeTimingsReference != null)
        {
            // Call the function from HitboxTimings
            DodgeTimingsReference.FunctionToCall();
        }
        else
        {
            Debug.LogWarning("DodgeTimingsReference reference not found in child GameObject.");
        }
        if (JumpTimingsReference != null)
        {
            // Call the function from HitboxTimings
            JumpTimingsReference.FunctionToCall();
        }
        else
        {
            Debug.LogWarning("JumpTimingsReference reference not found in child GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //RigidBody MOVE
        if (!isFrozen) 
        {
            // Get input for horizontal movement
            float moveX = Input.GetAxis("Horizontal");

            // Calculate the velocity for horizontal movement
            Vector2 movementSpeed = new Vector2(moveX * moveSpeed, rb.velocity.y);

            // Apply the velocity to move the sprite
            rb.velocity = movementSpeed;

            // Check if the player is moving
            isMoving = Mathf.Abs(moveX) > isMovingThreshold; // You can adjust the threshold as needed

            // Apply movement based on the player's input
            if (isMoving)
            {
                // Calculate the velocity for horizontal movement
                Vector2 movement = new Vector2(moveX * moveSpeed, rb.velocity.y);
                rb.velocity = movement;

                // Flip the sprite if necessary
                FlipSprite(moveX);
            }
         
        }




        // ATTACK
        if (Input.GetKeyDown(KeyCode.G) && !isBlocking && isGrounded &&!isFrozen)
        {
            
            // Stop moving by setting the horizontal velocity to 0
            StillForAnimation(timing);
            
            // Check if it's the same key as the last one
            if (Input.GetKeyDown(lastKeyPressed))
            {
                // The player pressed the same key twice
                Debug.Log("Same key pressed twice: " + lastKeyPressed);
                
                // Increment the trigger index
                currentTriggerIndex++;
                Debug.Log("Index: " + currentTriggerIndex);
                // If the index is out of bounds, wrap it around
                if (currentTriggerIndex >= triggerNames.Length)
                {
                    currentTriggerIndex = 0;
                }

                // Set the trigger based on the current index
                string currentTriggerName = triggerNames[currentTriggerIndex];
                animator.SetTrigger(currentTriggerName);
                StartCoroutine(WaitForAnimation(currentTriggerName));
                HitboxTimingsReference.EnableHitbox(timing);

                Debug.Log("Trigger: " + currentTriggerName);

            }
            else
            {
                // Update the last key pressed
                lastKeyPressed = KeyCode.G;
                animator.SetTrigger("Sword");
                StartCoroutine(WaitForAnimation("Sword"));
                HitboxTimingsReference.EnableHitbox(timing);

            }
        }
       

        // ROLL
        if ((Input.GetKeyDown(KeyCode.S))&&(isMoving) && (isGrounded==true) &&!isRolling)
        {
            animator.SetTrigger("Roll");
            isRolling = true;
            StartCoroutine(RollTiming(rollTime));
            lastKeyPressed = KeyCode.S;
        }

        // Jump  
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && canJump)
        {
            animator.SetBool("isJumping", true);
            Jump();
            DodgeTimingsReference.DisableHitBoxSwitch();
            JumpTimingsReference.EnableHitBoxSwitch();
            lastKeyPressed = KeyCode.W;
        }

        // Block  
        if (Input.GetKeyDown(KeyCode.F) && isGrounded && canJump && !isFrozen)
        {
            ParryTimingsReference.EnableHitbox(blockTiming);
            animator.SetBool("isBlocking", true);
            lastKeyPressed = KeyCode.F;
            isBlocking = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            animator.SetBool("isBlocking", false);
            isBlocking = false;
        }

        //RUN
        if (Input.GetKeyDown(KeyCode.A)) 
        {
        
        }
        
        if (isMoving)
        {
            animator.SetBool("isRunning", true);
            lastKeyPressed = KeyCode.D;
        }
        if (!isMoving) 
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void Hurt()
    {
        animator.SetTrigger("Hurt");
        StillForAnimation(hurtDelay);
    }

    public void Parry()
    {
        animator.SetTrigger("Parry");
    }

    void FlipSprite(float moveX)
    {
        // Check the direction of movement and flip the sprite accordingly
        if ((moveX > 0 && !isFacingRight) || (moveX < 0 && isFacingRight))
        {
            // Flip the sprite by reversing its local scale on the X-axis
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;

            // Update the facing direction
            isFacingRight = !isFacingRight;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with an object tagged as ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // The player is grounded, you can add additional logic here
            isGrounded = true;
            animator.SetBool("isJumping", false);
            canJump = true;
            DodgeTimingsReference.EnableHitBoxSwitch();
            JumpTimingsReference.DisableHitBoxSwitch();
        }
    }
    void Jump()
    {
        // Apply an upward force to make the player jump
        
        isGrounded = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        canJump = false;
    }


    IEnumerator RollTiming(float timing)
    {
        // Code before the delay
        DodgeTimingsReference.DisableHitbox(timing);
        moveSpeed = rollSpeed;
        Debug.Log("Icrease Roll Speed!");
        yield return new WaitForSeconds(timing); // Wait for 2 seconds
        moveSpeed = 5; //standard move speed
        yield return new WaitForSeconds(rollDelay);
        isRolling = false;      
        // Code after the delay
    }

    public void StillForAnimation(float timing) 
    {
        StartCoroutine(TriggerTiming(timing));
    }

    IEnumerator TriggerTiming(float timing)
    {
        // Code before the delay
        rb.velocity = new Vector2(0f, rb.velocity.y);
        isMoving = false;
        isFrozen = true;
        Debug.Log("isFrozen: " + isFrozen);
        yield return new WaitForSeconds(timing); // Wait for 2 seconds
        isFrozen = false;
        // Code after the delay
    }

    IEnumerator WaitForAnimation(string animationName)
    {
        // Wait for the specified animation to finish
        while (!IsAnimationFinished(animationName))
        {
            yield return null;
        }

        // Animation has finished; you can now perform post-animation actions
        Debug.Log("Animation finished!");

        // Add your post-animation logic here
    }

    bool IsAnimationFinished(string animationName)
    {
        // Check if the specified animation is not playing
        return !animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    //test to call from another script
    public void FunctionToCall()
    {
        Debug.Log("Function in AttackControls called.");
    }

}
