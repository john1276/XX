using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 移動相關變數
    public float moveSpeed = 5f; // 移動速度
    public float jumpForce = 10f; // 跳躍力度

    // 地面檢測
    public Transform groundCheck; // 檢查地面的位置
    public float groundCheckRadius = 1.5f; // 檢查範圍
    public float groundCheckDistance = 1.5f;
    public LayerMask groundLayer; // 定義地面圖層

    private Rigidbody2D rb;
    private bool jumpRequest;
    const float Tolerance = 0.05f;//離地跳躍
    float m_CoyoteTime; // coyote time 的剩餘時間

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 獲取剛體
    }

    private void Update()
    {
        // 左右移動
        float moveInput = Input.GetAxis("Horizontal"); // 獲取鍵盤輸入（-1到1）
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // 設置速度
        // 檢測是否在地面
        bool isGrounded = CheckGround();
        //Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded) {
            m_CoyoteTime = Tolerance; // 站在地面上就設定 coyote time
        } else {
            m_CoyoteTime -= Time.deltaTime; // 如果沒有站在地面上就開始倒數
        }
        // 跳躍
        if (Input.GetKeyDown(KeyCode.Space) && m_CoyoteTime > 0)
        {
            jumpRequest = true; // 標記跳躍請求
        }
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
}