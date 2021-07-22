using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("dit is de barrel");
        //if (collision.gameObject.tag == "Moveable")
        //{
        //    collision.transform.parent = transform;

        //}
    }

}
