using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity,yRotationLimit;
    private Transform player;
    float xRotation;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnEnable()
    {
        PlayerInput.OnCameraRotation += CameraMovementHandler;
    }
    private void CameraMovementHandler(float mouseX,float mouseY)
    {
      
        xRotation -= mouseY * Time.deltaTime*mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -yRotationLimit, yRotationLimit);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.Rotate(Vector3.up * mouseX * Time.deltaTime*mouseSensitivity);
    }

    private void OnDisable()
    {
        PlayerInput.OnCameraRotation -= CameraMovementHandler;
    }
}
