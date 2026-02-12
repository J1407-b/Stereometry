using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotateSpeed = 5f;
    public float panSpeed = 0.5f;
    public float zoomSpeed = 50f;
    private Vector3 targetPoint;
    private Vector3 lastMousePosition;
    private Camera cam;
    void Awake()
    {
        cam = GetComponent<Camera>();
        targetPoint = transform.position + transform.forward * 10f;
    }
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.LeftShift) && Input.GetMouseButton(2))
        {
            HandleRotation();
        }
        if (Input.GetMouseButton(2) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            HandlePanning();
        }
        HandleZoom();
        lastMousePosition = Input.mousePosition;
    }

    void HandleRotation()
    {
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
            return;
        }
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
        float rotX = -mouseDelta.y * rotateSpeed * Time.deltaTime;
        float rotY = mouseDelta.x * rotateSpeed * Time.deltaTime;
        transform.RotateAround(targetPoint, transform.right, rotX);
        transform.RotateAround(targetPoint, Vector3.up, rotY);
    }
    void HandlePanning()
    {
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
            return;
        }

        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
        Vector3 moveX = transform.right * -mouseDelta.x * panSpeed * Time.deltaTime;
        Vector3 moveY = transform.up * -mouseDelta.y * panSpeed * Time.deltaTime;
        transform.position += moveX;
        transform.position += moveY;

        targetPoint += moveX;
        targetPoint += moveY;
    }
    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float moveDistance = scroll * zoomSpeed * Time.deltaTime;
            transform.Translate(Vector3.forward * moveDistance, Space.Self);
        }
    }
}