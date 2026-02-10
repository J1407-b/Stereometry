using System.Collections.Generic;
using UnityEngine;

public class SphereCollector : MonoBehaviour
{
    // Храним ссылки на сами GameObject сфер (максимум 2)
    private List<GameObject> selectedSphereObjects = new List<GameObject>();
    private LineRenderer lineRenderer;

    public void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // Убедимся, что линия будет рисоваться между двумя точками
        lineRenderer.positionCount = 2;
    }

    public void Update()
    {
        // Проверяем нажатие левой кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                SelectableSphere sphereScript = hit.collider.GetComponent<SelectableSphere>();

                if (sphereScript != null)
                {
                    GameObject sphereObject = hit.collider.gameObject;

                    // Если сфера еще не выбрана и у нас меньше 2 сфер - добавляем её
                    if (!selectedSphereObjects.Contains(sphereObject) && selectedSphereObjects.Count < 2)
                    {
                        selectedSphereObjects.Add(sphereObject);
                        Debug.Log("Сфера добавлена: " + sphereObject.name);
                    }
                }
            }
        }

        if (selectedSphereObjects.Count == 2)
            UpdateLineRenderer();

        // Очищаем линию, если выбрано меньше 2 сфер
        if (selectedSphereObjects.Count < 2 && Input.GetMouseButtonDown(1))
        {
            Destroy(lineRenderer.gameObject);
            return;
        }
    }



    private void UpdateLineRenderer()
    {
        
        // Проверяем, что обе сферы существуют
        if (selectedSphereObjects[0] == null || selectedSphereObjects[1] == null)
        {
            lineRenderer.positionCount = 0;
            // Удаляем уничтоженные сферы из списка
            selectedSphereObjects.RemoveAll(sphere => sphere == null);
            return;
        }

        // Устанавливаем линию между двумя выбранными сферами
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, selectedSphereObjects[0].transform.position);
        lineRenderer.SetPosition(1, selectedSphereObjects[1].transform.position);
    }


}