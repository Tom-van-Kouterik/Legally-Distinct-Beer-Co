using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> seats = new();
    [SerializeField] private List<GameObject> plates = new();
    [SerializeField] public List<GameObject> people = new();
    [SerializeField] private List<bool> isOcupied = new() {false, false, false, false};
    [SerializeField] private bool[] drinkAcces = new bool[6];
    [SerializeField] private bool[] glassAcces = new bool[6];
    [SerializeField] private bool[] garnishAcces = new bool[6];
    private int orderGlass;
    private int orderGarnish;
    private List<int> orderDrinks = new();
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

        if (!GenerateOrder())
        {
            return;
        }

        Customers customerFunctions;
        GameObject newCustomer = Instantiate(customer, seats[spot].transform);
        customerFunctions = newCustomer.GetComponent<Customers>();
        customerFunctions.SetVariables(spot, gameObject, plates[spot],orderGlass, orderGarnish, orderDrinks);
        people[spot] = newCustomer;
        isOcupied[spot] = true;

        if (CheckCapacity() == true)
        {
            atMax = true;
        }
              
    }

    public bool GenerateOrder()
    {
        int valid = 0;
        int volume = 0;
        for (int i = 0; i < drinkAcces.Length; i++)
        {
            if (drinkAcces[i] == false)
            {
                valid++;
            }
        }

        if (valid >= drinkAcces.Length)
        {
            return(false);
        }

        if (orderDrinks.Count != 0)
        {
            orderDrinks.Clear();
        }
        orderGlass = Random.Range(0,6);
        orderGarnish = Random.Range(0,6);
        while (!garnishAcces[orderGarnish])
        {
            orderGarnish = Random.Range(0, 6);
        }

        while (!glassAcces[orderGlass])
        {
            orderGlass = Random.Range(0, 6);
        }

        int size;
        if (orderGlass <= 1)
        {
            size = 4;
        }
        else if (orderGlass == 2 || orderGlass == 3)
        {
            size = 5;
        }
        else
        {
            size = 6;
        }
        while (volume < size)
        {
            int drink = Random.Range(0, 6);
            if (drinkAcces[drink])
            {
                orderDrinks.Add(drink);
                volume++;
            }
        }
        return(true);
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
    public void CompleteOrder(int profit, GameObject customer)
    {
        Debug.Log(profit);
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