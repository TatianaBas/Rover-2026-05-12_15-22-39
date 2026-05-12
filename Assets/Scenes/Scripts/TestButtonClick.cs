using UnityEngine;

public class TestButtonClick : MonoBehaviour
{
    public void OnButtonPress()
    {
        Debug.Log(" КНОПКА СРАБОТАЛА!");
        // Меняем цвет кнопки для наглядности
        GetComponent<UnityEngine.UI.Image>().color = Color.green;
    }
}