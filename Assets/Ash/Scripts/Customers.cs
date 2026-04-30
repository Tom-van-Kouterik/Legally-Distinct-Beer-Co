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
    private MeshRenderer billboard;
    private GameObject bord;
    [SerializeField] private Material[] want;
    [SerializeField] private GameObject sign;

    //a simple tag compare, comparing the order they got and what they actually ordered, then acts based upon if it was the correct order or not
    public void CompareOrder(GameObject meal)
    {
        meal.transform.SetParent(bord.transform);
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

    //primitive way to show what the customer wants. will be updated
    private void DisplayOrder()
    {
        if (order == "Wine")
        {
            billboard.material = want[0];
        }
        else
        {
            billboard.material = want[1];
        }
    }

    // gets called when a new customer is created to give them their random variables
    public void SetVariables(string id, int time, int gain, int stool, GameObject cm, GameObject plate)
    {
        billboard = sign.GetComponent<MeshRenderer>();
        order = id;
        delay = time;
        money = gain;
        seatNumber = stool;
        manager = cm.GetComponent<CustomerManager>();
        me = this.gameObject;
        bord = plate;
        DisplayOrder();
    }

    IEnumerator Leave()
    {
        manager.CompleteOrder(isCorrect, money);
        yield return new WaitForSecondsRealtime(delay);
        manager.DestroyCustomer(seatNumber);
        Destroy(me);
    }
}
