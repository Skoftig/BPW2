using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public UnityEvent onActivate;
    public bool Activated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("barrel"))
        {
            Rigidbody2D Temp = collision.gameObject.GetComponent<Rigidbody2D>();
            Temp.constraints = RigidbodyConstraints2D.FreezeAll;
            collision.gameObject.transform.position = transform.position;
            Activated = true;
        }
    }
}
