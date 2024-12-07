using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializableDictionary.Scripts;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public SerializableDictionary<int, List<Platform>> plat_dict;
    void Start()
    {
        if (GameManager.instance == null){
            GameManager.instance = this;
        }
        else{
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    float timer = 0;
    bool first = true;
    // Update is called once per frame
    void Update()
    {
        if (timer > 1 && first){
            //plus(1);
            first = false;
        }
        timer += Time.deltaTime;
    }

    public void plus(int num, bool vertical){
        if (plat_dict.ContainsKey(num)){
            foreach (Platform plat in plat_dict.Get(num)){
                plat.plus(vertical);
            }
        }
    }

    public void multi(int num, bool vertical){
        if (plat_dict.ContainsKey(num)){
            foreach (Platform plat in plat_dict.Get(num)){
                plat.multi(vertical);
            }
        }
    }
    
}
