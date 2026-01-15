using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 move;
    public float speed;
    public float jumpForce;
    PlayerInput input;
    public bool isGrounded;
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Quieto();
        move = input.actions["Move"].ReadValue<Vector2>();
        Flip();
        animator.SetFloat("Horizontal", Mathf.Abs(rb.linearVelocityX));
        animator.SetBool("IsGround", isGrounded);
    }
    private void FixedUpdate()
    {
        rb.linearVelocityX = move.x * speed;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }
    }
    public void Flip()
    {
        if (rb.linearVelocityX < 0)
        {
            transform.localScale = new Vector3(-6, 6, 6);
        }
        if (rb.linearVelocityX > 0)
        {
            transform.localScale = new Vector3(6, 6, 6);

        }
    }
    public void Quieto()
    {
        if (rb.linearVelocityX != 0)
        {
            GetComponent<Timer>().OnPlayerMoved();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Boost"))
        {
            rb.AddForce(Vector2.down * 30, ForceMode2D.Impulse);
        }
        if (other.CompareTag("BoostUp"))
        {
            rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }
}