using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public bool running = true;
    public float limit_time = 60;
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        if (running) {
            text.text = (Math.Ceiling(limit_time)).ToString();
            if (limit_time < 10){
                text.color = Color.red;
            }
            if (limit_time <= 0){
                running = false;
                GameManager.instance.timeOut();
            }
            limit_time -= Time.deltaTime;
        }
    }
}
