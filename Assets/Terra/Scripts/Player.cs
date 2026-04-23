using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
//    private Camera myCam;
    public Vector3 movementInput = Vector3.zero;
    public float movementSpeed;

    LayerMask layerMask;

    void Start()
    {
        layerMask = LayerMask.GetMask("Interactable");
        // myCam = Camera.main;

        // Cursor.lockState = CursorLockMode.Confined;
        // Cursor.visible = false;
    }

    
    void Update()
    {
        // Vector3 target = myCam.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        // myCam.transform.LookAt(target, Vector3.up);

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y).normalized * movementSpeed;
        transform.position = transform.position + move;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
         if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
        }
        else
        {
            movementInput = Vector3.zero;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (context.performed)
        {
            if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
                Debug.Log("Test");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * Mathf.Infinity, Color.red);
            }
        }
    }

}
