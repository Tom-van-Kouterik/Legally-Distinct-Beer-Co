using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> seats = new List<GameObject>();
    [SerializeField] private List<GameObject> plates = new List<GameObject>();
    [SerializeField] private GameObject Customer;

    public void SpawnCustomer()
    {
        //spawn the customer prefab and link it to the seat and plate of the asosiated spot and mark this spot as "taken"
    }

    public void DestroyCustomer()
    {
        //destroy the corosponding customer and mark their spot as "open"
    }

    public void CompleteOrder()
    {
        //either add or remove gold from gold total
    }
}