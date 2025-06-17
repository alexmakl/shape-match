using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContainer : MonoBehaviour
{
    [SerializeField] private Transform wallLeft;
    [SerializeField] private Transform wallRight;
    
    void Start()
    {
        Camera camera = Camera.main;
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;
        
        wallLeft.position = new Vector3(camera.transform.position.x - cameraWidth / 2f - 0.4f, camera.transform.position.y, 0);
        wallRight.position = new Vector3(camera.transform.position.x + cameraWidth / 2f + 0.4f, camera.transform.position.y, 0);
    }
}
