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
                    else if (selectedSphereObjects.Count >= 2)
                    {
                        Debug.Log("Уже выбрано максимальное количество сфер (2).");
                        // Можно добавить логику замены, если нужно:
                        // Например, заменить первую сферу: selectedSphereObjects[0] = sphereObject;
                    }
                }
            }
        }

        // Очищаем уничтоженные сферы из списка
        CleanDestroyedSpheres();

        // Обновляем линию
        UpdateLineRenderer();
    }

    private void CleanDestroyedSpheres()
    {
        // Создаем временный список для сфер, которые нужно удалить
        List<GameObject> spheresToRemove = new List<GameObject>();

        // Проверяем все сферы в списке
        for (int i = 0; i < selectedSphereObjects.Count; i++)
        {
            if (selectedSphereObjects[i] == null)
            {
                // Если сфера уничтожена (null), добавляем в список на удаление
                spheresToRemove.Add(selectedSphereObjects[i]);
                Debug.Log("Обнаружена уничтоженная сфера, удаляю из списка...");
            }
        }

        // Удаляем все отмеченные сферы
        foreach (GameObject sphere in spheresToRemove)
        {
            selectedSphereObjects.Remove(sphere);
        }
    }

    private void UpdateLineRenderer()
    {
        // Очищаем линию, если выбрано меньше 2 сфер
        if (selectedSphereObjects.Count < 2)
        {
            lineRenderer.positionCount = 0;
            return;
        }

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

    // Метод для получения выбранных сфер (опционально)
    public List<GameObject> GetSelectedSpheres()
    {
        return new List<GameObject>(selectedSphereObjects);
    }

    // Метод для очистки всех выбранных сфер (опционально)
    public void ClearSelection()
    {
        selectedSphereObjects.Clear();
        lineRenderer.positionCount = 0;
    }
}