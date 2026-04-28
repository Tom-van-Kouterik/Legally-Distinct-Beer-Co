using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> seats = new List<GameObject>();
    [SerializeField] private List<GameObject> plates = new List<GameObject>();
    [SerializeField] private List<GameObject> people = new List<GameObject>();
    [SerializeField] private List<bool> isOcupied = new List<bool>() {false, false, false, false};
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
    private bool test;
    private int select;

    // define order tags
    private void Start()
    {
        orderList = new string[2] {"Finish", "Respawn"};
    }

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
        gold = Random.Range(0, 5);
        wait = Random.Range(0, 5);
        chosenOrder = Random.Range(0, orderList.Length);
        GameObject newCustomer = Instantiate(customer, seats[spot].transform);
        customerFunctions = newCustomer.GetComponent<Customers>();
        customerFunctions.SetVariables(orderList[chosenOrder], wait, gold, spot, this.gameObject);
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
    /// <param name="result"></param>
    /// <param name="gold"></param>
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

    /// <summary>
    /// sees if all seats are taken, if they are it turns on a bool to prevent feedbackloops
    /// </summary>
    /// <returns></returns>
    private bool CheckCapacity()
    {
        int takenSeats = 0;

        for (int i = 0; i < seats.Count; i++)
        {
            Debug.Log(takenSeats);
            if (isOcupied[i] == true)
            {
                takenSeats++;
            }
        }

        return takenSeats == seats.Count;
    }

    public void Switch()
    {
        select++;
        if (select == isOcupied.Count)
        {
            select = 0;
        }
    }

    public void ChangeOrder()
    {
        if (test)
        {
            test = false;
        }
        else
        {
            test = true;
        }
    }
    public void Serve()
    {
        Customers selectC;
        selectC = people[select].GetComponent<Customers>();

        if (test)
        {
            selectC.CompareOrder(beer);
        }
        else if (!test)
        {
            selectC.CompareOrder(wine);
        }
    }
}