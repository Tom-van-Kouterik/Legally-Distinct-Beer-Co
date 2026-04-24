using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> seats = new List<GameObject>();
    [SerializeField] private List<GameObject> plates = new List<GameObject>();
    [SerializeField] private List<bool> ocupation = new List<bool>() {false, false, false, false};
    [SerializeField] private string[] orderList;
    [SerializeField] private GameObject customer;
    [SerializeField] private GameObject beer;
    [SerializeField] private GameObject wine;
    private bool atMax = false;
    private Customers cd;
    private int profit;
    private int gold;
    private int wait;
    private int chosenOrder;

    // define order tags
    private void Start()
    {
        orderList = new string[2] {"Finish", "Respawn"};
    }

    // spawn the customer prefab and link it to the seat and plate of the asosiated spot and mark this spot as "taken"
    public void SpawnCustomer()
    {
        int spot;
        if (atMax)
        {

        }
        else if (!atMax)
        {
            spot = Random.Range(0, ocupation.Count);

            if (ocupation[spot] == true)
            {
                SpawnCustomer();
            }
            else if (ocupation[spot] == false)
            {
                Customers customerFunctions;
                gold = Random.Range(0, 5);
                wait = Random.Range(0, 5);
                chosenOrder = Random.Range(0, orderList.Length);
                GameObject newCustomer = Instantiate(customer, seats[spot].transform);
                customerFunctions = newCustomer.GetComponent<Customers>();
                customerFunctions.SetVariables(orderList[chosenOrder], wait, gold, spot);
                ocupation[spot] = true;
                if (CheckCapacity() == true)
                {
                    atMax = true;
                }
            }
        }
    }

    // called on from the customer in order completion to change the seat status
    public void DestroyCustomer(int seat)
    {
        ocupation[seat] = false;

        if (atMax)
        {
            atMax = false;
        }
    }

    // either add or remove gold from gold total depending on order completion
    public void CompleteOrder(bool result, int gold)
    {
        if (result)
        {
            profit += gold;
        }
        else if (!result)
        {
            profit -= gold;
        }
    }

    // sees if all seats are taken, if they are it turns on a bool to prevent feedbackloops
    private bool CheckCapacity()
    {
        int takenSeats = 0;

        for (int i = 0; i < seats.Count; i++)
        {
            Debug.Log(takenSeats);
            if (ocupation[i] == true)
            {
                takenSeats++;
            }
        }

        if (takenSeats == seats.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}