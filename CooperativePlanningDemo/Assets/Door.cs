using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private PressurePlate pressurePlateScript1 = null;
    [SerializeField] private PressurePlate pressurePlateScript2 = null;
    private bool doorOpen;
    private Vector3 startPos;
    private Vector3 endPos;
    void Start()
    {
        doorOpen = false;
        startPos = transform.position;
        endPos = startPos;
        endPos.x = -3.24f;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorOpen == false && (pressurePlateScript1.depressed && pressurePlateScript2.depressed))
            doorOpen = true;

        if (doorOpen)
            transform.position = endPos;
        
    }
}
