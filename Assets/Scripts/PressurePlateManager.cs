using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateManager : MonoBehaviour
{
    public PressurePlate[] pressurePlates;
    private bool CanOpenFire;
    public UnityEvent plateEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PressurePlate pressurePlate in pressurePlates)
        {
            if (pressurePlate.Activated)
            {
                CanOpenFire = true;
            }
            else
            {
                CanOpenFire = false;
                break;
            }
        }
        if(CanOpenFire)
        {
            plateEvent.Invoke();
        }
    }
}
