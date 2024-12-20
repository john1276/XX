using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Platform : MonoBehaviour
{
    public float plus_factor = 1;
    public float multi_factor = 2;
    public int time = 0;
    public virtual void plus(bool vertical){
        if(!vertical)
        {
            transform.localScale = new Vector3(transform.localScale.x + plus_factor, Math.Max(transform.localScale.y - plus_factor, 0.1f), transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Math.Max(transform.localScale.x - plus_factor, 0.1f), transform.localScale.y + plus_factor, transform.localScale.z);
        }
    }

    public virtual void multi(bool vertical){
        if (vertical)
        {
            transform.localScale = new Vector3(transform.localScale.x / multi_factor, transform.localScale.y * multi_factor, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x * multi_factor, transform.localScale.y / multi_factor, transform.localScale.z);
        }
        }
}
