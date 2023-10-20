using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableFloor : MonoBehaviour
{
    [Header("Timer")] 
    [Tooltip("The time to break the floor from the time it is touched by the player, in seconds")] public float timeToBreak = 4f;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(BreakFloor());
        }
    }

    IEnumerator BreakFloor()
    {
        Debug.Log(name + " floor is breaking in " + timeToBreak + " seconds!");
        
        yield return new WaitForSeconds(timeToBreak);
        
        Destroy(gameObject);
    }
}