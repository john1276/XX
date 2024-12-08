using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> back_list;
    public float back_timer = 0;
    int cur_back=0;
    public GameObject startHint;
    public float blink_speed = 1;
    public float blink_timer = 0;
    private bool cur_blink_status = false;

    void Start(){
        for (int i = 1; i < back_list.Count; i++){
            back_list[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        back_timer += Time.deltaTime;
        if (back_timer > 1f / back_list.Count){
            // next background
            back_list[cur_back++].SetActive(false);
            cur_back %= back_list.Count;
            back_list[cur_back].SetActive(true);
            back_timer = 0;
        }
        blink_timer += Time.deltaTime;
        if (blink_timer > blink_speed){
            startHint.SetActive(cur_blink_status);
            cur_blink_status ^= true;
            blink_timer = 0;
        }
        // press enter to start
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)){
            GameManager.instance.loadNextLevel();
        }
    }
}
