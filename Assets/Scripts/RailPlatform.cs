using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailPlatform : Platform
{
    public float speed = 1;
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject rail;

    public override void plus(){
        rail.transform.localScale = new Vector3(rail.transform.localScale.x * plus_factor, rail.transform.localScale.y, rail.transform.localScale.z);
    }
    public override void multi(){
        speed *= multi_factor;
    }

    bool left = true;

    void Update(){
        if (left){
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else{
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (endPoint.transform.position.x < transform.position.x){
            left = false;
        }
        else if (transform.position.x < startPoint.transform.position.x){
            left = true;
        }
    }

    void OnCollisionEnter(Collision collision){
        print(collision.gameObject);
    }
}
