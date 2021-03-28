using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool depressed;
    [SerializeField] private Collider agent1Collider = null;
    [SerializeField] private Collider agent2Collider = null;
    void Start()
    {
        depressed = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        Collider otherCollider = other.gameObject.GetComponent<Collider>();
        
        if (otherCollider == agent1Collider || otherCollider == agent2Collider)
            depressed = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.GetComponent<Collider>() == agent1Collider || other.gameObject.GetComponent<Collider>() == agent2Collider)
            depressed = false;
    }
}
