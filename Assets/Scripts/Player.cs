using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public States currentState;
    public float moveSpeed = 5f;
    public float slideSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public bool pushable;
    private Vector2 movement;
    private Collision2D coll;
    [SerializeField] private bool slime;
    public Vector2 currentDirection;
    public Vector2[] directions = new Vector2[4] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    public Axis currentAxis;
    private int RaycastLayers;
    public float RaycastLength;


    private void Start()
    {
        ChangeState(States.Walking);
    }
    // Update is called once per frame
    private void Update()
    {
        animator.SetFloat("Horizontal", currentDirection.x);
        animator.SetFloat("Vertical", currentDirection.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void ShootRaycast(Vector2[] directions)
    {
        RaycastLayers = LayerMask.GetMask("Details");
        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], RaycastLength, LayerMask.GetMask("Details"));
            Debug.DrawRay(transform.position, directions[i] * RaycastLength, Color.red);
            if (hit)
            {
                BarrelSlide slide = hit.collider.gameObject.GetComponent<BarrelSlide>();
                if (slide)
                {
                    Debug.Log("Barrel be slidin" + directions[i]);
                    slide.velocity = directions[i];
                }
                rb.velocity = Vector2.zero;
                slime = false;
                ChangeState(States.Changing);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SlimeTiles")
        {
            ChangeState(States.Sliding);
            slime = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SlimeTiles")
        {
            ChangeState(States.Walking);
            slime = false;
        }
    }

    /// <summary>
    /// FixedUpdate is more reliable when working with Physics, as the normal Update can vary. 
    /// To ensure movement speed stays the same no matter how many times the update is called, it is multiplied with 
    /// Time.fixedDeltaTime
    /// I want the player to slide on the slime tiles, I use an if and else statement. When on dry land, the player moves
    /// with movePosition for accurate movement, but when on slime, I use AddForce to give the player more fluid movement.
    /// </summary>
    private void FixedUpdate()
    {
        movement = Vector2.zero;

        switch (currentState)
        {
            case States.Walking:
                Walking();
                break;
            case States.Sliding:
                Sliding();
                break;
            case States.Changing:
                Changing();
                break;
            default:
                break;
        }
    }

    private void Sliding()
    {
        if (slime)
        {
            rb.AddForce((Vector2)transform.position + currentDirection * slideSpeed * Time.fixedDeltaTime, ForceMode2D.Force);
            switch (currentAxis)
            {
                case Axis.Horizontal:
                    ShootRaycast(new Vector2[2] { directions[2], directions[3] });
                    break;
                case Axis.Vertical:
                    ShootRaycast(new Vector2[2] { directions[0], directions[1] });
                    break;
                default:
                    break;
            }
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                movement.x = Input.GetAxisRaw("Horizontal");
                slime = true;
                currentAxis = Axis.Horizontal;
            }

            else
            {
                if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                    movement.y = Input.GetAxisRaw("Vertical");
                    slime = true;
                    currentAxis = Axis.Vertical;
                }
            }
            currentDirection = movement;
        }
    }
    /// <summary>
    /// https://answers.unity.com/questions/238887/can-you-unfreeze-a-rigidbodyconstraint-position-as.html
    /// used for unfreezing constraints
    /// weird bug solved by reassigning currentDirection to be identical to movement solving the issue of the player moving in the wrong direction.
    /// </summary>
    private void Walking()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            movement.x = Input.GetAxisRaw("Horizontal");
            currentAxis = Axis.Horizontal;
        }

        else
        {
            if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                movement.y = Input.GetAxisRaw("Vertical");
                currentAxis = Axis.Vertical;
            }

        }
        currentDirection = movement;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void Changing()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        currentDirection = Vector2.zero;
        if (horizontal > 0 || horizontal < 0)
        {
            currentDirection = new Vector2(horizontal, 0);
        }

        else if (vertical > 0 || vertical < 0)
        {
            currentDirection = new Vector2(0, vertical);
        }

        ChangeState(States.Sliding);
    }

    private void ChangeState(States nextState)
    {
        currentState = nextState;
    }

}

public enum Axis
{
    Horizontal,
    Vertical
}

public enum States
{
    Walking,
    Sliding,
    Changing
}
