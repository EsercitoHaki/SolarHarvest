using UnityEngine;
using UnityEngine.InputSystem; // Đảm bảo đã import namespace này

public class PlayerInput : MonoBehaviour
{
    private PlayerControls playerControls; // Tên của Input Actions Asset của bạn

    // Properties công khai để các script khác có thể đọc giá trị input
    public Vector2 MoveInput { get; private set; }
    public bool AttackPressed { get; private set; } // Ví dụ cho nút tấn công
    public bool InteractPressed { get; private set; } // Ví dụ cho nút tương tác

    private void Awake()
    {
        playerControls = new PlayerControls();

        // Gán các callback cho hành động di chuyển
        playerControls.Movement.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        playerControls.Movement.Move.canceled += ctx => MoveInput = Vector2.zero;

        // Ví dụ cho các hành động khác (đảm bảo đã định nghĩa trong Input Actions Asset của bạn)
        // Nếu bạn chưa có, hãy thêm các Action "Attack" và "Interact" kiểu Button vào PlayerControls.
        // playerControls.Actions.Attack.performed += ctx => AttackPressed = true;
        // playerControls.Actions.Attack.canceled += ctx => AttackPressed = false; // Reset sau khi nhả nút

        // playerControls.Actions.Interact.performed += ctx => InteractPressed = true;
        // playerControls.Actions.Interact.canceled += ctx => InteractPressed = false; // Reset sau khi nhả nút
    }

    private void OnEnable()
    {
        playerControls.Enable(); // Bật Input Action Map
    }

    private void OnDisable()
    {
        playerControls.Disable(); // Tắt Input Action Map
    }

    // Nếu bạn muốn các sự kiện nhấn nút chỉ xảy ra 1 lần cho mỗi lần nhấn
    // thì có thể reset cờ ở đây hoặc trong PlayerController sau khi xử lý.
    // public void ResetActionInputs()
    // {
    //     AttackPressed = false;
    //     InteractPressed = false;
    // }
}