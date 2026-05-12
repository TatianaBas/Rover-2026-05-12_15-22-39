using UnityEngine;
using UnityEngine.InputSystem;

public class RoverInput : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;
    public float turnSpeed = 100f;
    
    private Vector2 moveInput;
    
    
    void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.isGameActive)
        {
            // Если игра не активна — не реагируем на клавиши
            moveInput = Vector2.zero;
            return;
        }
        float horizontal = Keyboard.current.aKey.isPressed ? -1f : Keyboard.current.dKey.isPressed ? 1f : 0f;
        horizontal = horizontal == 0f && Keyboard.current.leftArrowKey.isPressed ? -1f : Keyboard.current.rightArrowKey.isPressed ? 1f : horizontal;
        
        float vertical = Keyboard.current.wKey.isPressed ? 1f : Keyboard.current.sKey.isPressed ? -1f : 0f;
        vertical = vertical == 0f && Keyboard.current.upArrowKey.isPressed ? 1f : Keyboard.current.downArrowKey.isPressed ? -1f : vertical;
        
        moveInput = new Vector2(horizontal, vertical);
    }
    
    void FixedUpdate()
    {
        Vector3 movement = transform.forward * moveInput.y * speed * Time.fixedDeltaTime;
        transform.Translate(movement, Space.World);
        
        float turn = moveInput.x * turnSpeed * Time.fixedDeltaTime;
        transform.Rotate(0f, turn, 0f, Space.World);
    }
}