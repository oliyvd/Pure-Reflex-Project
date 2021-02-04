using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Coordinates")]
    public Vector3 CameraCoords;
    public float Ycoord = 7.5f;

    [Header("Camera")]
    public bool isPushable;
    private Camera cam;
    public int panBorderThickness = 1;
    public bool edgeTouch;
    public int speed = 20;
    private float cameraSpeed;
    public bool isDragging;
    public float zoomSpeed;
    private float mouseScrollInput;
    private float camFOV;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, Ycoord, 0);
        Cursor.lockState = CursorLockMode.Confined;
        cam = GetComponent<Camera>();
        camFOV = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
        // Update camera coordinates
        CameraCoords = transform.position;
        EdgeDetect();
        EdgeMovement();
        MouseDrag();
        Zooming();
    }

    /// <summary>
    /// <Author> Vubi GameDev </Author>  
    /// <URL> https://www.youtube.com/channel/UCE76fmvkZK5nVVoi6BtyVFA </URL>
    /// </summary>
    void Zooming()
    {
        camFOV -= mouseScrollInput * zoomSpeed;
        camFOV = Mathf.Clamp(camFOV, 30, 60);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camFOV, zoomSpeed * Time.deltaTime);
    }

    void EdgeMovement()
    {   
        // Obtain inital speed
        cameraSpeed = speed;

        // Alter speed if touching multiple edges
        if (noEdge > 1)
            cameraSpeed = cameraSpeed / 1.5f;

        // Check if not dragging
        if (!isDragging && isPushable)
        {   
            // Obtain coordinates 
            Vector3 pos = transform.position;
    
            // Up
            if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                pos.z += cameraSpeed * Time.deltaTime;
            }

            // Down
            if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
            {
                pos.z -= cameraSpeed * Time.deltaTime;
            }

            // Right
            if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                pos.x += cameraSpeed * Time.deltaTime;
            }

            // Left
            if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
            {
                pos.x -= cameraSpeed * Time.deltaTime;
            }

            transform.position = pos;
        }
    }

    public int noEdge;
    void EdgeDetect()
    {
        Vector3 pos = transform.position;
        noEdge = 0;

        
        // Up
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            noEdge += 1;
        }

        // Down
        if (Input.mousePosition.y <= panBorderThickness)
        {
            noEdge += 1;
        }

        // Right
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            noEdge += 1;
        }

        // Left
        if (Input.mousePosition.x <= panBorderThickness)
        {
            noEdge += 1;
        }
    }

    Vector3 oldPos;
    Vector3 panOrigin;
    void MouseDrag()
    {
        if (Input.GetMouseButtonDown(2))
        {
            isDragging = true;
            //Vector2 mousePos = Input.mousePosition;
            //dragOrigin = Input.mousePosition;

            oldPos = transform.position;
            panOrigin = cam.ScreenToViewportPoint(Input.mousePosition);

            return;
        }

        if (Input.GetMouseButton(2))
        {
            isDragging = true;
            float x2 = speed * 2;
            
            if (noEdge <= 0)
            {
                Vector3 pos = transform.position;
                pos.x -= Input.GetAxis("Mouse X") * x2 * Time.deltaTime;
                pos.z -= Input.GetAxis("Mouse Y") * x2 * Time.deltaTime;
                transform.position = pos;
            }
        }

        else if (Input.GetMouseButtonUp(2))
        {
            isDragging = false;
            //cursorAttach.Attach();
            return;
        }

        //Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        //Vector3 move = new Vector3(pos.x * dragSpeed * Time.deltaTime, 0, pos.y * dragSpeed * Time.deltaTime);


        //transform.Translate(move, Space.World);
    }
}
