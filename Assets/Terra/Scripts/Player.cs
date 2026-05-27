using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    private Camera myCam;
    public Vector3 movementInput = Vector3.zero;
    public float movementSpeed;

    [SerializeField]
    private bool handIsFull = false;
    [SerializeField]
    private GameObject handObj;
    [SerializeField]
    private GameObject heldItem;
    private GameObject interactedIngredient;
    LayerMask layerMask;

    /// <summary>
    /// Sets the camera to the "myCam" variable and adds the necessary layers to the LayerMask
    /// </summary>
    void Start()
    {
        layerMask = LayerMask.GetMask("Holdable", "Trashcan", "Customer", "Ingredient");  
        myCam = Camera.main;
    }

    /// <summary>
    /// Creates a new Vector3 called move and calculates the speed at wich it moves and then moves the player
    /// </summary>
    void Update()
    {
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y).normalized * movementSpeed;
        transform.position = transform.position + move;
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
            if(Physics.Raycast(myCam.transform.position, transform.forward, out hit, Mathf.Infinity, holdable))
            {
                if (handIsFull)
                {
                    return;
                }
                heldItem = hit.collider.gameObject;
                heldItem.transform.parent = handObj.transform;
                heldItem.transform.position = handObj.transform.position;   
                handIsFull = true;
            }
            
            //if the player is holding an item and interacts with the trashcan layer it destroys the held object and empties the hand
            LayerMask trashcan = LayerMask.GetMask("Trashcan"); 
            if (Physics.Raycast(myCam.transform.position, transform.forward, out hit, Mathf.Infinity, trashcan))
            {
                if (handIsFull)
                {
                    Destroy(heldItem);
                    handIsFull = false; 
                }
            }
            
            LayerMask customer = LayerMask.GetMask("Customer");
            if (Physics.Raycast(myCam.transform.position, transform.forward, out hit, Mathf.Infinity, customer))
            {
                
            }

            LayerMask ingredient = LayerMask.GetMask("Ingredient");
            if (Physics.Raycast(myCam.transform.position, transform.forward, out hit, Mathf.Infinity, ingredient))
            {
                Ingredient interactedIngredient = hit.collider.gameObject.GetComponent<Ingredient>();
                DrinkLogic glassScript = heldItem.GetComponent<DrinkLogic>();
                if (handIsFull)
                {
                    if (glassScript.heldIngredients.Count >= glassScript.maxSize)
                    {
                        return;
                    }
                    heldItem.GetComponent<DrinkLogic>().heldIngredients.Add(interactedIngredient.ingredientNumber);
                }         
            }
        }
    }

}
