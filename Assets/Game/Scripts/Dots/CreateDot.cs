using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateDot : MonoBehaviour
{
    [SerializeField] private TMP_InputField xField;
    [SerializeField] private TMP_InputField yField;
    [SerializeField] private TMP_InputField zField;
    [SerializeField] private GameObject Dot;

    
    public void AddDot()
    {
        // Инициализируем значения по умолчанию
        float xValue = 0f;
        float yValue = 0f;
        float zValue = 0f;

        // Пытаемся получить значение X
        if (xField != null && !string.IsNullOrEmpty(xField.text))
        {
            if (float.TryParse(xField.text, out float parsedX))
            {
                xValue = parsedX / 10f;
            }
            else
            {
                Debug.LogWarning("Неверный формат в поле X. Используется значение по умолчанию: 0");
            }
        }

        // Пытаемся получить значение Y
        if (yField != null && !string.IsNullOrEmpty(yField.text))
        {
            if (float.TryParse(yField.text, out float parsedY))
            {
                yValue = parsedY / 10f;
            }
            else
            {
                Debug.LogWarning("Неверный формат в поле Y. Используется значение по умолчанию: 0");
            }
        }

        // Пытаемся получить значение Z
        if (zField != null && !string.IsNullOrEmpty(zField.text))
        {
            if (float.TryParse(zField.text, out float parsedZ))
            {
                zValue = parsedZ / 10f;
            }
            else
            {
                Debug.LogWarning("Неверный формат в поле Z. Используется значение по умолчанию: 0");
            }
        }

        // Создаем позицию точки
        Vector3 dotPosition = new Vector3(xValue, yValue, zValue);

        // Создаем экземпляр точки
        if (Dot != null)
        {
            Instantiate(Dot, dotPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Префаб точки не назначен!");
        }
    }
}
