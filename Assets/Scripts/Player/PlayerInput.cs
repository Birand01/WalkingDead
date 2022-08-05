using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void OnPlayerBodyMovementHandler(Vector3 movement);
    public delegate void OnPlayerMovementAnimHandler(float x, float y);
    public delegate void OnCameraRotationHandler(float x, float y);
    public delegate void OnShootHandler(bool state);
    public delegate void OnShootAnimHandler(bool state);
  
    public static event OnShootAnimHandler OnShootAnim;
    public static event OnPlayerBodyMovementHandler OnPlayerMovement; 
    public static event OnCameraRotationHandler OnCameraRotation;  
    public static event OnShootHandler OnShoot;
    public static event OnPlayerMovementAnimHandler OnPlayerAnim;

    private void Update()
    {
        if (!PlayerHealth.Instance.isGameOver)
        {
            OnBodyMovement();
            OnCameraMovement();
            OnShootEvent();
        }
    }
    private void OnShootEvent()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnShootAnim?.Invoke(true);
            OnShoot?.Invoke(true);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            OnShootAnim?.Invoke(false);
            OnShoot?.Invoke(false);
        }
    }

   
    private void OnBodyMovement()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        OnPlayerMovement?.Invoke(movement);
        OnPlayerAnim(movement.x, movement.z);
    }

    private void OnCameraMovement()
    {
      float x = Input.GetAxisRaw("Mouse X");
      float y = Input.GetAxisRaw("Mouse Y");
        OnCameraRotation?.Invoke(x, y);
    }
}
