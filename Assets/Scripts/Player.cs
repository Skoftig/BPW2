using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float slideSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public bool pushable;
    private Vector2 movement;
    private Collision2D coll;
    public bool slime;


    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");



        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SlimeTiles")
        {
            slime = true;
            //Debug.Log("Dit is slijmerig");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SlimeTiles")
        {
            slime = false;
            //Debug.Log("Dit is droog");
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
        if (slime == false)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        }

        else
        {
            rb.AddForce(rb.position + movement * slideSpeed * Time.fixedDeltaTime);
        }

    }


}
