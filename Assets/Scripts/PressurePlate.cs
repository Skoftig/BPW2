using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public UnityEvent unityEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Barrel")
        {
            Rigidbody2D Temp = collision.gameObject.GetComponent<Rigidbody2D>();
            Temp.constraints = RigidbodyConstraints2D.FreezeAll;
            unityEvent?.Invoke();
        }
    }
}
