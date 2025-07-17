using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Tốc độ di chuyển của nhân vật

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        // Nếu Rigidbody2D được đặt trên GameObject con, bạn cần GetComponentInChildren
        // Ví dụ: rb = GetComponentInChildren<Rigidbody2D>();
    }

    /// <summary>
    /// Thực hiện di chuyển nhân vật dựa trên hướng và tốc độ.
    /// Hàm này được gọi từ PlayerController trong FixedUpdate.
    /// </summary>
    /// <param name="moveDirection">Vector2 chỉ hướng di chuyển.</param>
    public void Move(Vector2 moveDirection)
    {
        // Sử dụng MovePosition để di chuyển Rigidbody2D, tốt cho vật lý
        rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }
}