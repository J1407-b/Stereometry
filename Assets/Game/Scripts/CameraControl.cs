using UnityEngine;

public class BlenderLikeCamera : MonoBehaviour
{
    // Скорости для разных режимов
    public float rotateSpeed = 5f;
    public float panSpeed = 0.5f;
    public float zoomSpeed = 50f;

    // Точка, вокруг которой происходит вращение
    private Vector3 targetPoint;

    // Внутренние переменные для отслеживания ввода
    private Vector3 lastMousePosition;

    // Устанавливается в Awake/Start
    private Camera cam;

    // Используется для инициализации
    void Awake()
    {
        // Получаем компонент Camera, прикрепленный к этому объекту
        cam = GetComponent<Camera>();

        // Инициализируем targetPoint перед первым использованием
        targetPoint = transform.position + transform.forward * 10f;
    }

    // Вызывается каждый кадр
    void Update()
    {
        // 1. Вращение (Орбитирование) - Средняя кнопка мыши (MMB)
        if (Input.GetMouseButton(2)) // 2 соответствует MMB
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                // Не вращаем, если нажата клавиша Shift (Панорамирование имеет приоритет)
            }
            else
            {
                // Если MMB нажата, но Shift нет - Вращение
                HandleRotation();
            }
        }

        // 2. Панорамирование - Shift + Средняя кнопка мыши (MMB)
        if (Input.GetMouseButton(2) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            // Если MMB и Shift нажаты - Панорамирование
            HandlePanning();
        }

        // 3. Масштабирование (Приближение/Отдаление) - Колесо мыши
        HandleZoom();

        // Обновляем lastMousePosition в конце кадра
        lastMousePosition = Input.mousePosition;
    }


    /// <summary>
    ///Обработка вращения (орбитирования) вокруг targetPoint.
    /// </summary>

    void HandleRotation()
    {
        // Если MMB была только что нажата, устанавливаем текущую позицию мыши, 
        // чтобы предотвратить скачок
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
            return;
        }

        // Вычисляем разницу в позиции мыши
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

        // Преобразуем разницу в углы поворота
        float rotX = -mouseDelta.y * rotateSpeed * Time.deltaTime; // Вращение по X (вверх/вниз)
        float rotY = mouseDelta.x * rotateSpeed * Time.deltaTime;  // Вращение по Y (влево/вправо)

        // Преобразуем вращение
        // Вращаем вокруг оси X (взгляд вверх/вниз)
        transform.RotateAround(targetPoint, transform.right, rotX);
        // Вращаем вокруг глобальной оси Y (взгляд влево/вправо)
        transform.RotateAround(targetPoint, Vector3.up, rotY);

        // Чтобы камера всегда смотрела на targetPoint после вращения (опционально)
        // transform.LookAt(targetPoint); 
    }

    /// <summary>
    /// Обработка панорамирования (перемещения) камеры в плоскости экрана.
    /// </summary>
    void HandlePanning()
    {
        // Если MMB была только что нажата, устанавливаем текущую позицию мыши, 
        // чтобы предотвратить скачок
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
            return;
        }

        // Вычисляем разницу в позиции мыши
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

        // Определяем направление движения в пространстве мира
        Vector3 moveX = transform.right * -mouseDelta.x * panSpeed * Time.deltaTime; // Движение влево/вправо
        Vector3 moveY = transform.up * -mouseDelta.y * panSpeed * Time.deltaTime;    // Движение вверх/вниз

        // Применяем панорамирование к позиции камеры и targetPoint
        transform.position += moveX;
        transform.position += moveY;

        targetPoint += moveX;
        targetPoint += moveY;
    }

    /// <summary>
    /// Обработка масштабирования (приближения/отдаления) с помощью колеса мыши.
    /// </summary>
    void HandleZoom()
    {
        // Получаем прокрутку колеса мыши
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Если была прокрутка
        if (scroll != 0f)
        {
            // Вычисляем расстояние для перемещения
            float moveDistance = scroll * zoomSpeed * Time.deltaTime;

            // Двигаем камеру вперед/назад вдоль ее направления
            transform.Translate(Vector3.forward * moveDistance, Space.Self);

            // Обновляем targetPoint, чтобы он оставался на определенном расстоянии 
            // от камеры (опционально, чтобы targetPoint не "уплывал" слишком далеко)
            // targetPoint = transform.position + transform.forward * Vector3.Distance(transform.position, targetPoint);
        }
    }
}