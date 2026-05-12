using UnityEngine;

public class MarsRoverCamera : MonoBehaviour
{
    [Header("Target (auto-find if empty)")]
    public Transform target;
    
    [Header("Camera Settings")]
    public float distance = 12f;
    public float height = 5f;
    public float smoothSpeed = 5f;
    
    [Header("Optional Offset")]
    public Vector3 lookOffset = new Vector3(0f, 1f, 0f);
    
    void LateUpdate()
    {
        // Если цель не назначена или уничтожена — ищем марсоход
        if (target == null)
        {
            GameObject rover = GameObject.FindGameObjectWithTag("Player");
            if (rover != null)
            {
                target = rover.transform;
                Debug.Log("Камера нашла марсоход и следит за ним!");
            }
            else
            {
                // Марсохода ещё нет на сцене (например, ещё не создан)
                return;
            }
        }
        
        // Расчёт желаемой позиции камеры
        Vector3 desiredPosition = target.position 
                                - target.forward * distance 
                                + Vector3.up * height;
        
        // Плавное движение
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        
        // Смотрим на марсоход
        transform.LookAt(target.position + lookOffset);
    }
}