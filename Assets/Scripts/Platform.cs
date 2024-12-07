using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float plus_factor = 2;
    public float multi_factor = 2;
    public virtual void plus(){
        transform.localScale = new Vector3(transform.localScale.x / plus_factor, transform.localScale.y * plus_factor, transform.localScale.z);
    }

    public virtual void multi(){
        transform.localScale = new Vector3(transform.localScale.x * multi_factor, transform.localScale.y / multi_factor, transform.localScale.z);
    }
}
