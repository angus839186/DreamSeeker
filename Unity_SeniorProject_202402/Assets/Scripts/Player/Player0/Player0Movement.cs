using Sirenix.OdinInspector;
using UnityEngine;

public class Player0Movement : SerializedMonoBehaviour
{
    public Camera mainCam;
    [SerializeField, Header("位移速度")] private float speed = 1.9f;
    [SerializeField, Header("限制範圍")] private float minX, maxX;
    private Rigidbody2D rb;
    private float MoveH, MoveV;
    [SerializeField] private float Movespeed;
    [SerializeField] private float jumpSpeed;

    [SerializeField] public bool isMoved;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isMoved) 
        {
            MoveH = Input.GetAxis("Horizontal") * Movespeed;
        }
        
        
        Flip();
        JumpUp();
    }

    private void LateUpdate()
    {
        Vector3 posA = mainCam.transform.position;
        Vector3 posB = transform.position;

        posB.z = -19.2f;

        posA = Vector3.Lerp(posA, posB, speed * Time.deltaTime);

        mainCam.transform.position = posA;
        Follow();
    }

    private void Follow()
    {
        mainCam.transform.position = new Vector3(Mathf.Clamp(mainCam.transform.position.x, minX, maxX), 6.6f, -19.2f);
    }

    private void FixedUpdate()
    {
        Walk();   
    }

    private void Walk()
    {
        if (isMoved == true)
        {
            rb.velocity = new Vector2(MoveH, rb.velocity.y);
            if (MoveH!= 0)
            {
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
            }
        }
        else
        {
            MoveH = 0;
            rb.velocity = new Vector2(0, 0);
            anim.SetBool("Walk", false);
        }
    }

    private void Flip()
    {
        if (isMoved == true)
        {
            //左右
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
            isMoved = false;
            rb.velocity = Vector2.zero;
            //rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed));
            rb.gravityScale = 1.5f;
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
    }

    public void Jump()
    {
        rb.gravityScale = 0.75f;
    }

    public void JumpDown()
    {
        Movespeed = 3f;
        rb.gravityScale = 0;
        isMoved = true;
    }
}
