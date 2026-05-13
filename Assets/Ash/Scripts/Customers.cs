using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Customers : MonoBehaviour
{
    public int glass;
    public int garnish;
    public int[] drinks;
    private CustomerManager manager;
    private int seatNumber;
    private int delay;
    private int money;
    private bool isCorrect;
    private GameObject me;
    private MeshRenderer billboard;
    private GameObject bord;
    [SerializeField] private Material happy;
    [SerializeField] private Material sad;
    [SerializeField] private Material[] want;
    [SerializeField] private GameObject sign;

    private void Awake()
    {
        drinks = new int[4];
    }

    //a simple tag compare, comparing the order they got and what they actually ordered, then acts based upon if it was the correct order or not
    public void CompareOrder(GameObject meal)
    {
        //meal.transform.SetParent(bord.transform);
        TestMug order = meal.GetComponent<TestMug>();
        if (glass != order.glassType)
        {
            StartCoroutine(nameof(Leave));
            return;
        }
        if (garnish != order.garnishType)
        {
            StartCoroutine(nameof(Leave));
            return;
        }
        for (int i = 0; i < order.drinkTypes.Length; i++)
        {
            if (drinks[i] != order.drinkTypes[i])
            {
                StartCoroutine(nameof(Leave));
                return;
            }
        }
        isCorrect = true;
        StartCoroutine(nameof(Leave));
    }

    //primitive way to show what the customer wants. will be updated
    private void DisplayOrder()
    {
        //needs updating with the ned gameplay
    }

    // gets called when a new customer is created to give them their random variables
    public void SetVariables(int time, int gain, int stool, GameObject cm, GameObject plate)
    {
        billboard = sign.GetComponent<MeshRenderer>();
        glass = Random.Range(0, 9);
        garnish = Random.Range(0, 9);
        for (int i = 0; i < 3; i++)
        {
            drinks[i] = Random.Range(0, 9);
        }
        delay = time;
        money = gain;
        seatNumber = stool;
        manager = cm.GetComponent<CustomerManager>();
        me = this.gameObject;
        bord = plate;
    }

    IEnumerator Leave()
    {
        manager.CompleteOrder(isCorrect, money);
        yield return new WaitForSecondsRealtime(delay);
        manager.DestroyCustomer(seatNumber);
        Destroy(me);
    }
}