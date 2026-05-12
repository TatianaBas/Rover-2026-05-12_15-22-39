using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Простая сцена загружена!");
    }

    void Update()
    {
        // Простая логика: вращение объекта
        transform.Rotate(Vector3.up * Time.deltaTime * 50f);
    }
}
