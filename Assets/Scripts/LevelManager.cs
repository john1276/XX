using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializableDictionary.Scripts;

public class LevelManager : MonoBehaviour
{
    public SerializableDictionary<int, List<Platform>> plat_dict;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.levelManager != null) Debug.LogError("forget to remove previous level manager");
        GameManager.instance.levelManager = this;
    }
}
