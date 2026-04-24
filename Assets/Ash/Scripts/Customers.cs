using System.Collections;
using UnityEngine;

public class Customers : MonoBehaviour
{
    private string order;
    private CustomerManager manager;
    private int seatNumber;
    private int delay;
    private int money;
    private bool isCorrect;

    //a simple tag compare, comparing the order they got and what they actually ordered, then acts based upon if it was the correct order or not
    private void CompareOrder(GameObject meal)
    {
        if (order == meal.tag)
        {
            isCorrect = true;
            StartCoroutine(nameof(Leave));
        }
        else if (order != meal.tag)
        {
            isCorrect = false;
            StartCoroutine(nameof(Leave));
        }
    }

    // gets called when a new customer is created to give them their random variables
    public void SetVariables(string id, int time, int gain, int stool)
    {
        order = id;
        delay = time;
        money = gain;
        seatNumber = stool;
    }

    IEnumerator Leave()
    {
        manager.CompleteOrder(isCorrect, money);
        yield return new WaitForSecondsRealtime(delay);
        manager.DestroyCustomer(seatNumber);
        Destroy(this);
    }
}
