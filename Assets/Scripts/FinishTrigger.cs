using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 判斷是否是玩家
        {
            Debug.Log("到達終點！");
            // 執行結束遊戲或切換關卡的邏輯
            //GameManager.Instance.LevelComplete(); 
        }
    }
}
