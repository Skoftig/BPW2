using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public bool pushable;
    private Vector2 movement;
    private Direction lastInput;
    private Collision2D coll;
    private Direction directionStatus;


    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement);
        if(hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Moveable")
        {
            collision.transform.parent = transform;
            lastInput = directionStatus;
            coll = collision;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "SlimeTiles")
        {
            Debug.Log("Dit is slijmerig");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SlimeTiles")
        {
            Debug.Log("Dit is droog");
        }
    }

    /// <summary>
    /// FixedUpdate is more reliable when working with Physics, as the normal Update can vary. 
    /// To ensure movement speed stays the same no matter how many times the update is called, it is multiplied with 
    /// Time.fixedDeltaTime
    /// </summary>
    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        directionStatus = GetDirection(movement);

        if (lastInput != directionStatus && coll != null)
        {
            coll.transform.parent = null;
            
            
        }

        //Debug.Log(lastInput + "<lastInput  directionstatus>" + directionStatus);

    }

    public enum Direction
    {
        Up,
        Down,
        Right,
        Left,
        None
    }

    public Direction GetDirection(Vector2 playerDirection)
    {
        if(playerDirection == Vector2.up)
        {
            Debug.Log("Up");
            return Direction.Up;
        }

        else if(playerDirection == Vector2.down)
        {
            Debug.Log("Down");
            return Direction.Down;
        }

        else if (playerDirection == Vector2.right)
        {
            Debug.Log("Right");
            return Direction.Right;

        }

        else if (playerDirection == Vector2.left)
        {
            Debug.Log("Left");
            return Direction.Left;

        }

        else
        {
            Debug.Log("None");
            return Direction.None;

        }
    }
}
