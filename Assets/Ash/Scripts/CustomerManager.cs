using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> seats = new List<GameObject>();
    [SerializeField] private List<GameObject> plates = new List<GameObject>();
    [SerializeField] public List<GameObject> people = new List<GameObject>();
    [SerializeField] private List<bool> isOcupied = new List<bool>() {false, false, false, false};
    [SerializeField] private GameObject customer;
    private bool atMax = false;

    /// <summary>
    /// spawn the customer prefab and link it to the seat and plate of the asosiated spot and mark this spot as "taken"
    /// </summary>
    public void SpawnCustomer()
    {
        if (atMax)
        {
            return;
        }

        int spot = Random.Range(0, isOcupied.Count);

        while (isOcupied[spot])
        {
            spot = Random.Range(0, isOcupied.Count);
        }

        Customers customerFunctions;
        GameObject newCustomer = Instantiate(customer, seats[spot].transform);
        customerFunctions = newCustomer.GetComponent<Customers>();
        customerFunctions.SetVariables(spot, gameObject, plates[spot]);
        people[spot] = newCustomer;
        isOcupied[spot] = true;

        if (CheckCapacity() == true)
        {
            atMax = true;
        }
              
    }

    /// <summary>
    /// called on from the customer in order completion to change the seat status
    /// </summary>
    /// <param name="seat">seat ID</param>
    public void DestroyCustomer(int seat)
    {
        isOcupied[seat] = false;

        if (atMax)
        {
            atMax = false;
        }
    }

    /// <summary>
    /// either add or remove gold from gold total depending on order completion
    /// </summary>
    public void CompleteOrder(bool result)
    {
        Debug.Log(result);
    }

    /// <summary>
    /// sees if all seats are taken, if they are it turns on a bool to prevent feedbackloops
    /// </summary>
    /// <returns></returns>
    private bool CheckCapacity()
    {
        int takenSeats = 0;

        for (int i = 0; i < seats.Count; i++)
        {
            if (isOcupied[i] == true)
            {
                takenSeats++;
            }
        }

        return takenSeats == seats.Count;
    }
}