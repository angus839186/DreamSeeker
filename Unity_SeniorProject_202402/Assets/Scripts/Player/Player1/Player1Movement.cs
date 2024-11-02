using MUI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player1Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float MoveH, MoveV;
    [SerializeField] private float Movespeed;
    [SerializeField] private float jumpSpeed;
    private Player1Controller player1Controller;

    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player1Controller = GetComponent<Player1Controller>();
    }

    private void Update()
    {
        MoveH = Input.GetAxis("Horizontal") * Movespeed;
        MoveV = Input.GetAxis("Vertical") * Movespeed;

        Flip();
        JumpUp();
        SquatDown();
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
    {
        if (player1Controller.isMoved == true)
        {
            if (MoveH != 0 || MoveV != 0)
            {
                rb.velocity = new Vector2(MoveH, MoveV);
                anim.SetBool("Walk", true);
                anim.SetFloat("Horizontal", MoveH);
                anim.SetFloat("Vertical", MoveV);
            }
            else
            {
                MoveH = 0; MoveV = 0;
                anim.SetBool("Walk", false);
            }
        }
    }

    private void Flip()
    { 
        if (player1Controller.isMoved == true)
        {
            //екеk
            if (MoveH > 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (MoveH < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    private void JumpUp()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player1Controller.isMoved = false;
            rb.velocity = Vector2.zero;
            //rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed));
            rb.gravityScale = 1.5f;
            rb.AddForce(Vector2.up * jumpSpeed ,ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
    }

    public void Jump()
    {
        rb.gravityScale = 0.5f;
    }

    public void JumpDown()
    {
        Movespeed = 3f;
        rb.gravityScale = 0;
        player1Controller.isMoved = true;
    }

    private void SquatDown()
    {
        if (Input.GetKey(KeyCode.V))
        {
            anim.SetBool("Squatdown",true);
            rb.velocity = Vector2.zero;
            player1Controller.isMoved = false;
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            anim.SetBool("Squatdown", false);
            player1Controller.isMoved = true;
        }
    }
}
