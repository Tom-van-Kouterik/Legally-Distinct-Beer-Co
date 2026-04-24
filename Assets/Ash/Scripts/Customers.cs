using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Customers : MonoBehaviour
{
    private string order;
    private CustomerManager manager;
    private int seatNumber;
    private int delay;
    private int money;
    private bool isCorrect;
    private GameObject me;

    //a simple tag compare, comparing the order they got and what they actually ordered, then acts based upon if it was the correct order or not
    public void CompareOrder(GameObject meal)
    {
        if (order == meal.tag)
        {
            Debug.Log("correct");
            isCorrect = true;
            StartCoroutine(nameof(Leave));
        }
        else if (order != meal.tag)
        {
            Debug.Log("wrong");
            isCorrect = false;
            StartCoroutine(nameof(Leave));
        }
    }

    // gets called when a new customer is created to give them their random variables
    public void SetVariables(string id, int time, int gain, int stool, GameObject cm)
    {
        order = id;
        delay = time;
        money = gain;
        seatNumber = stool;
        manager = cm.GetComponent<CustomerManager>();
        me = this.gameObject;
    }

    IEnumerator Leave()
    {
        Debug.Log("return started");
        manager.CompleteOrder(isCorrect, money);
        yield return new WaitForSecondsRealtime(delay);
        manager.DestroyCustomer(seatNumber);
        Destroy(me);
    }
}
