using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializableDictionary.Scripts;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int cur_level = 0;
    public LevelManager levelManager = null;
    void Start()
    {
        if (GameManager.instance == null){
            GameManager.instance = this;
        }
        else{
            Destroy(this.gameObject);
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
        if (levelManager == null){
            Debug.LogError("level manager missing.");
        }
        if (levelManager.plat_dict.ContainsKey(num)){
            foreach (Platform plat in levelManager.plat_dict.Get(num)){
                plat.plus(vertical);
            }
        }
    }

    public void multi(int num, bool vertical){
        if (levelManager == null){
            Debug.LogError("level manager missing.");
        }
        if (levelManager.plat_dict.ContainsKey(num)){
            foreach (Platform plat in levelManager.plat_dict.Get(num)){
                plat.multi(vertical);
            }
        }
    }

    public void loadNextLevel(){
        SceneManager.LoadScene(++cur_level);
    }

    public void levelReload(){
        SceneManager.LoadScene(cur_level);
    }
}
