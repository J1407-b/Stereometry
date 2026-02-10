using UnityEngine;
using System.Collections.Generic;

public class CreateLineRenderer : MonoBehaviour
{
    [SerializeField] GameObject LineRender;
    private List<Vector3> selectedSpheres = new List<Vector3>();
    private Vector3[] positions;
    public void CreateLineRender()
    {
        GameObject.Instantiate(LineRender);
        // Проверяем нажатие левой кнопки мыши (0 - левая кнопка)
    }
}
