using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] float speed;
    private Vector3 move;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        PlayerInput.OnPlayerMovement += HandleBodyMovement;
    }

    private void HandleBodyMovement(Vector3 movementVector)
    {
       
        move = transform.right * movementVector.x + transform.forward * movementVector.z;
        characterController.Move(move * speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        PlayerInput.OnPlayerMovement -= HandleBodyMovement;
    }
}
