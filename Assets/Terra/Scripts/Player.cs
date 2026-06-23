using NUnit.Framework;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    private Camera myCam;
    public Vector3 movementInput = Vector3.zero;
    public float movementSpeed;
    private Rigidbody rb;
    [SerializeField]
    private bool handIsFull = false;
    [SerializeField]
    private GameObject handObj;

    [SerializeField]
    private GameObject itemLookedAt;
    [SerializeField]
    private GameObject heldItem;
    [SerializeField] private GameObject glass;
    [SerializeField] private Canvas confirmUI;
    [SerializeField] private GameObject shiftManager;
    [SerializeField] private Canvas crosshairUI;
    [SerializeField] private Canvas pauseUI;

    /// <summary>
    /// Sets the camera to the "myCam" variable and adds the necessary layers to the LayerMask
    /// </summary>
    void Start()
    {  
        myCam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Creates a new Vector3 called move and calculates the speed at wich it moves and then moves the player
    /// Shoots out a raycist that checks wich item the player looks at and sets a bool to true or false in the script on that item
    /// </summary>
    void FixedUpdate()
    {
        Vector3 move = rb.position + transform.TransformDirection(movementInput.x, 0, movementInput.y).normalized * movementSpeed;
        rb.MovePosition(move);

        RaycastHit hit;
        LayerMask look = LayerMask.GetMask("Holdable", "Ingredient");
        if(Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit, Mathf.Infinity, look))
        {
            itemLookedAt = hit.collider.gameObject;
            itemLookedAt.GetComponent<ItemWobble>().isBeingLookedAt = true;
        }
        else
        {
            itemLookedAt.GetComponent<ItemWobble>().isBeingLookedAt = false;
        }
    }

    /// <summary>
    /// Checks if the player inputs the button for the movement context and gives the value to the movementInput variable
    /// </summary>
    /// <param name="context">A local variable wich stores the input for the movement</param>
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

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            crosshairUI.gameObject.SetActive(false);
            pauseUI.gameObject.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    /// <summary>
    /// Checks if the player inputs the button for the interaction context
    /// </summary>
    /// <param name="context">A local variable wich stores the input for the interaction</param>
    public void OnInteract(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (context.performed)
        {
            //creates 3 local variables for each mask that only holds the specified layer and shoots a ray to check if it hit that layer
            //it checks if the player is holding an item, if so it does nothing if not it puts the hit gameobject into the players hand
            LayerMask holdable = LayerMask.GetMask("Holdable");
            if(Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit, Mathf.Infinity, holdable))
            {
                if (handIsFull)
                {
                    return;
                }
                heldItem = Instantiate(glass);
                heldItem.transform.parent = handObj.transform;
                heldItem.transform.position = handObj.transform.position;
                heldItem.GetComponent<TestMug>().SetSize(hit.collider.GetComponent<Ingredient>().ingredientNumber);
                handIsFull = true;
            }
            
            //if the player is holding an item and interacts with the trashcan layer it destroys the held object and empties the hand
            LayerMask trashcan = LayerMask.GetMask("Trashcan"); 
            if (Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit, Mathf.Infinity, trashcan))
            {
                if (handIsFull)
                {
                    Destroy(heldItem);
                    handIsFull = false; 
                }
                if (hit.collider.TryGetComponent<MeshCollider>(out MeshCollider tap) == true)
                {
                    Debug.Log("works");
                }
            }
            
            LayerMask customer = LayerMask.GetMask("Customer");
            if (Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit, Mathf.Infinity, customer))
            {
                if (handIsFull)
                {
                    hit.collider.GetComponent<Customers>().CompareOrder(heldItem);
                    handIsFull = false;
                }
            }

            LayerMask ingredient = LayerMask.GetMask("Ingredient");
            if (Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit, Mathf.Infinity, ingredient))
            {
                Ingredient interactedIngredient = hit.collider.gameObject.GetComponent<Ingredient>();
                if (handIsFull)
                {
                    if (interactedIngredient.isDrink)
                    {
                        heldItem.GetComponent<TestMug>().AddDrink(hit.collider.GetComponent<Ingredient>().ingredientNumber);
                    }
                    else
                    {
                        heldItem.GetComponent<TestMug>().AddGarnish(hit.collider.GetComponent<Ingredient>().ingredientNumber);
                    }
                }         
            }

            LayerMask book = LayerMask.GetMask("Book");
            if(Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit, Mathf.Infinity, book))
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                confirmUI.gameObject.SetActive(true);
                crosshairUI.gameObject.SetActive(false);
            }

            LayerMask bell = LayerMask.GetMask("Bell");
            if (Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit, Mathf.Infinity, bell))
            {
                shiftManager.GetComponent<ShiftManager>().ShiftStart();
            }
        }
    }

}
