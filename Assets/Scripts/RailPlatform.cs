using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RailPlatform : Platform
{
    public float speed = 1;
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject rail;

    public override void plus(bool vertical){
        rail.transform.localScale = new Vector3(transform.localScale.x + plus_factor, transform.localScale.y, transform.localScale.z);
        //rail.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * plus_factor, transform.localScale.z);
    }
    public override void multi(bool vertical){
        speed *= multi_factor;
    }

    bool left = true;
    double height;
    double width;
    double length;

    void Update(){
        if (left){
            transform.position += new Vector3((float)(speed * Time.deltaTime * width / length), (float)(speed * Time.deltaTime * height / length), 0);
        }
        else{
            transform.position -= new Vector3((float)(speed * Time.deltaTime * width / length), (float)(speed * Time.deltaTime * height / length), 0);
        }
        //double startDots = (transform.position.x - startPoint.transform.position.x) * width + (transform.position.y - startPoint.transform.position.y) * height;
        //double endDots = (transform.position.x - endPoint.transform.position.x) * width + (transform.position.y - endPoint.transform.position.y) * height;
        if (left){
            double endDots = (transform.position.x - endPoint.transform.position.x) * width + (transform.position.y - endPoint.transform.position.y) * height;
            if (endDots > 0){
                left = false;
            }
        }
        else{
            double startDots = (transform.position.x - startPoint.transform.position.x) * width + (transform.position.y - startPoint.transform.position.y) * height;
            if (startDots < 0){
                left = true;
            }
        }
    }

    void Start(){
        height = endPoint.transform.position.y - startPoint.transform.position.y;
        width = endPoint.transform.position.x - startPoint.transform.position.x;
        length = Math.Sqrt(Math.Pow(height, 2) + Math.Pow(width, 2));
    }

    void OnCollisionEnter(Collision collision){
        print(collision.gameObject);
    }
}
