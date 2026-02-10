using UnityEngine;

public class DeleteObject : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(1)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
