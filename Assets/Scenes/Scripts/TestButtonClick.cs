using UnityEngine;

public class TestButtonClick : MonoBehaviour
{
    public void OnButtonPress()
    {
        Debug.Log(" КНОПКА СРАБОТАЛА!");
        GetComponent<UnityEngine.UI.Image>().color = Color.green;
    }
}