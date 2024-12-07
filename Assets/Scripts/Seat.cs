using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public GameObject platform;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Log the name of the object that entered the trigger
        Debug.Log("Triggered by: " + other.gameObject.name);

        // You can add custom logic, for example:
        if (other.CompareTag("Player"))
        {
            // Do something when the player enters the trigger area
            other.gameObject.transform.parent = platform.transform;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        // Log the name of the object that entered the trigger
        Debug.Log("Triggered by: " + other.gameObject.name);

        // You can add custom logic, for example:
        if (other.CompareTag("Player"))
        {
            // Do something when the player enters the trigger area
            other.gameObject.transform.parent = null;
        }
    }
}