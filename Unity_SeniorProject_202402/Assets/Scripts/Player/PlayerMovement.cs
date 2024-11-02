using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 10;

    [SerializeField]
    private Rigidbody2D rb;

    private Vector2 velocity;

    [SerializeField]
    private Vector2 inputMovement;
    public bool canMove;

    public Animator anim;

    public static PlayerMovement instance;

    public GameObject hand;
    public bool isPushing;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        velocity = new Vector2(speed, speed);
        rb = GetComponent<Rigidbody2D>();
        canMove = true;

    }

    void Update()
    {
        if ((canMove))
        {
            inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if(inputMovement.sqrMagnitude > 0.1f)
            {
                anim.SetBool("Walk", true);
                anim.SetFloat("Vertical", inputMovement.y);
                anim.SetFloat("Horizontal", inputMovement.x);

                hand.transform.localPosition = inputMovement.normalized * 0.1f;
            }
            else
            {
                inputMovement = new Vector2(0, 0);
                anim.SetBool("Walk", false);

                hand.transform.localPosition = Vector2.zero;
            }
        }
        else
        {
            inputMovement = new Vector2(0, 0);
            anim.SetBool("Walk", false);

            hand.transform.localPosition = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        Vector2 delta = inputMovement * velocity * Time.deltaTime;
        Vector2 newPosition = rb.position + delta;
        rb.MovePosition(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            if (!GameManager.Instance.IsMiniGameCompleted("Sokobanbox"))
            {
                isPushing = true;
                anim.SetBool("Push", true);
                Debug.Log(isPushing);
            }
            else
            {
                isPushing = false;
                anim.SetBool("Push", false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            isPushing = false;
            anim.SetBool("Push", false);
        }
    }

}
