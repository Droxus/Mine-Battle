using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject map;
    public int panSpeed;
    public int zoomSpeed;
    private Vector3 panLimit;
    private bool isDragging = false;
    private Vector3 lastMousePosition;
    private int maxZoom = 20;
    private int xStartPosition = 0;
    // Start is called before the first frame update
    void Start() {
        panSpeed = 2;
        zoomSpeed = 5;
        map Map = map.GetComponent<map>();
        panLimit = new Vector3(Map.mapWidth-1, Map.mapHeight, Map.mapDepth-1);
        initCamera();
    }

    // Update is called once per frame
    void Update() {
        onCameraZoom();
        onCameraMove();
    }
    void initCamera() {
        Vector3 startPosition = new Vector3(panLimit.x/2, panLimit.y + 5, -panLimit.z/2);
        Quaternion startRotation = Quaternion.Euler(180/6, 0, 0);
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
    void onCameraMove() {
        if (Input.GetMouseButtonDown(1)) {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        } else if (Input.GetMouseButtonUp(1)) {
            isDragging = false;
        }
        if (isDragging) {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            Vector3 newPosition = transform.position;

            newPosition += new Vector3(-mouseDelta.x, 0, -mouseDelta.y) * panSpeed * Time.deltaTime;

            newPosition.x = Mathf.Clamp(newPosition.x, xStartPosition, panLimit.x);
            newPosition.z = Mathf.Clamp(newPosition.z, -panLimit.z, panLimit.z);

            transform.position = newPosition;
            lastMousePosition = Input.mousePosition;
        }
    }
    void onCameraZoom() {
        if (Input.mouseScrollDelta.y != 0) {
            float zoomAmount = Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
            Vector3 newPosition = transform.position + transform.forward * zoomAmount;
            // Vector3 newPosition = transform.position;
            // newPosition += new Vector3(0, 1, 0) * zoomAmount;
            newPosition.y = Mathf.Clamp(newPosition.y, panLimit.y, maxZoom);
            newPosition.x = Mathf.Clamp(newPosition.x, xStartPosition, panLimit.x);
            newPosition.z = Mathf.Clamp(newPosition.z, -panLimit.z, panLimit.z);
            transform.position = newPosition;
        }
    }
}
