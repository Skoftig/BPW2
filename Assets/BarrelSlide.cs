using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSlide : MonoBehaviour
{
    public Rigidbody2D myRB;
    public Vector2 velocity;
    public float speed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            velocity = Vector2.zero;
        }
    }
    void Start()
    {
        velocity = Vector2.zero;
    }
    void Update()
    {
        if(velocity != Vector2.zero)
        {
            myRB.AddForce(velocity * Time.deltaTime * speed,ForceMode2D.Force);
        }
    }
}
