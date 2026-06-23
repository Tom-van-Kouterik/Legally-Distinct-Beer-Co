using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class DrinkLogic : MonoBehaviour
{
    

    public int maxSize;
    [SerializeField]
    public List<int> heldIngredients = new List<int>();
    private void Start()
    {
        
    }
}
