using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    // 移動相關變數
    public float moveSpeed = 5f; // 移動速度
    public float jumpForce = 10f; // 跳躍力度

    // 地面檢測
    public Transform groundCheck; // 檢查地面的位置
    public Transform spriteTransform;
    public float groundCheckRadius = 1.5f; // 檢查範圍
    public float groundCheckDistance = 1.5f;
    public LayerMask groundLayer; // 定義地面圖層

    private Rigidbody2D rb;
    private bool jumpRequest;
    const float Tolerance = 0.05f;//離地跳躍
    float m_CoyoteTime; // coyote time 的剩餘時間
    Animator animator;
    private int animationState = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 獲取剛體
        animator = GetComponent<Animator>();
    }

    bool press_plus = false;
    bool press_multi = false;
    public int counter = 1;
    public GameObject X, P;
    public Camera mainCamera;      // 主相機
    public float objectSpacing = 1f; // 生成物件之間的水平間隔
    public int objectsPerRow = 5;   // 每行生成的物件數量

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private void Update()
    {
        // 左右移動
        float moveInput = Input.GetAxis("Horizontal"); // 獲取鍵盤輸入（-1到1）
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // 設置速度
        if (moveInput > 0) {
            spriteTransform.localScale = new Vector3(1, 1, 1); // 恢復正常方向
        } else if (moveInput < 0) {
            spriteTransform.localScale = new Vector3(-1, 1, 1); // 水平翻轉
        }
        // 檢測是否在地面
        bool isGrounded = CheckGround();
        //Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded) {
            m_CoyoteTime = Tolerance; // 站在地面上就設定 coyote time
        } else {
            m_CoyoteTime -= Time.deltaTime; // 如果沒有站在地面上就開始倒數
        }
        //移動動畫

        if (isGrounded) {
            if (moveInput != 0) {
                animationState = 2;
            } else {
                animationState = rb.velocity.x >= 0 ? 0 : 1;
            }
        } else {
            if (moveInput != 0) {
                animationState = 4;
            }
        }
        // 跳躍
        if (Input.GetKeyDown(KeyCode.Space) && m_CoyoteTime > 0)
        {
            jumpRequest = true; // 標記跳躍請求
        }
        //如果按下z/x，觸發對應加或乘，並會在按下方向鍵後，確定移動方向
        //但就算長按也只會觸發一次
        if (press_multi){
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
                if(GameManager.instance.levelManager.plat_dict.ContainsKey(counter))
                {
                    animationState = 6;
                    SpawnObjectsAtPosition(X);
                }
                else
                {
                    animationState = 10;
                }
                //StartCoroutine(PlayAnimationForDuration(animationState, 3f));
                GameManager.instance.multi(counter++, true);
                press_multi = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
                if(GameManager.instance.levelManager.plat_dict.ContainsKey(counter))
                {
                        animationState = 6;
                        SpawnObjectsAtPosition(X);
                }
                else
                {
                        animationState = 10;
                }
                //StartCoroutine(PlayAnimationForDuration(animationState, 3f));
                GameManager.instance.multi(counter++, false);
                press_multi = false;
            }
        }
        else if(press_plus){
            
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
                if(GameManager.instance.levelManager.plat_dict.ContainsKey(counter))
                {
                    animationState = 8;
                    SpawnObjectsAtPosition(P);
                }
                else
                {
                    animationState = 10;
                }
                StartCoroutine(PlayAnimationForDuration(animationState, 3f));
                GameManager.instance.plus(counter++, true);
                
                press_plus = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
                if(GameManager.instance.levelManager.plat_dict.ContainsKey(counter))
                {
                    animationState = 8;
                    SpawnObjectsAtPosition(P);
                }
                else
                {
                    animationState = 10;
                }
                //StartCoroutine(PlayAnimationForDuration(animationState, 3f));
                GameManager.instance.plus(counter++, false);
                press_plus = false;
            }
            
        }
        if (!Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKeyDown(KeyCode.DownArrow) && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.D)){
            if (Input.GetKeyDown(KeyCode.Z)){
                print("press z");
                press_plus = true;
            }
            else if (Input.GetKeyDown(KeyCode.X)){
                print("press x");
                press_multi = true;
            }
        }
        animator.SetInteger("State", animationState);
    }
    private void FixedUpdate()
    {
        // 執行跳躍邏輯
        if (jumpRequest)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // 添加垂直速度
            jumpRequest = false; // 重置跳躍請求
        }
    }
    private bool CheckGround()
    {
        // 從角色腳部發射射線檢測地面
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null; // 如果射線碰到物體，則說明在地面上
    }
    private void OnDrawGizmosSelected()
    {
        // 在編輯器中顯示地面檢測範圍
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    private IEnumerator PlayAnimationForDuration(int state, float duration)
    {
    // 設置動畫狀態
    Debug.Log(state);
    // 暫停指定的時間
    yield return new WaitForSeconds(duration);
    animator.SetInteger("State", state);
    }
    public void SpawnObjectsAtPosition(GameObject objectPrefab)
    {
        // 獲取屏幕邊界
        Vector3 screenPosition;
        screenPosition = new Vector3(0, Screen.height, 0); // 左上角
        
        // 將屏幕坐標轉換為世界坐標
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0; // 確保生成物件在2D層

        // 根據行數和列數生成物件

        // 計算當前物件的生成位置
        Vector3 spawnPosition = worldPosition + new Vector3(spawnedObjects.Count * objectSpacing + 1, -1, 0);

        // 生成物件並設置為相機的子物件
        GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity, mainCamera.transform);
        //newObject.transform.localScale = Vector3.one;
        newObject.transform.localScale = new Vector3(1f/1.23198f, 1f/0.40629f, 1f);
        // 添加到生成物件列表
        spawnedObjects.Add(newObject);
    }
}