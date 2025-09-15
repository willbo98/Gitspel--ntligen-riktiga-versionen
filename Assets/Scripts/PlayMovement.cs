using UnityEngine;

public class PlayMovement : MonoBehaviour
{
    public float runSpeed = 2;
    public float jumpSpeed = 3;
    public float doubleJumpSpeed = 2.5f;

    private bool canDoubleJump;

    Rigidbody2D rd2D;

    private bool betterJump = false;
   
    public float fallMultipiler = 0.5f;
    public float lowJumpMultiplier = 1f;

    public SpriteRenderer spriteRenderer;

    public Animator animator;

    private void Start()
    {
        rd2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (GroundCheck.isGrounded)
            {
                canDoubleJump = true;
                rd2D.velocity = new Vector2(rd2D.velocity.x, jumpSpeed);
            }
            else
            {
                if (canDoubleJump)
                {
                    animator.SetBool("DoubleJump", true);
                    rd2D.velocity = new Vector2(rd2D.velocity.x, jumpSpeed);
                    canDoubleJump = false; 
                }
            }
        }

        if (GroundCheck.isGrounded == false)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }
        if (GroundCheck.isGrounded == true)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Falling", false);
        }

        if (rd2D.velocity.y < 0)
        {
            animator.SetBool("Falling", true);
        }
        else if(rd2D.velocity.y > 0)
        {
            animator.SetBool("Falling", false);
        }
    }

    void FixedUpdate()
    {
        if(Input.GetKey("d") || Input.GetKey("right"))
        {
            rd2D.velocity = new Vector2(runSpeed, rd2D.velocity.y);
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rd2D.velocity = new Vector2(-runSpeed, rd2D.velocity.y);
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);
        }
        else
        {
            rd2D.velocity = new Vector2(0, rd2D.velocity.y);
            animator.SetBool("Run", false);
        }
   
        if (betterJump)
        {
            if (rd2D.velocity.y < 0)
            {
                rd2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultipiler) * Time.deltaTime;
            }

            if (rd2D.velocity.y > 0 && !Input.GetKey("space"))
            {
                rd2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }
    }
}
