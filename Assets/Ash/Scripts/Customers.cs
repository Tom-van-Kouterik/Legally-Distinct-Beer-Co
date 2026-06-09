using System;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Customers : MonoBehaviour
{
    public int glass;
    public int garnish;
    public int[] drinks;
    private int seatNumber;
    private int delay;
    private int money;
    private int glassSize;
    private float patience;
    private bool isCorrect = false;
    private bool isDone = false;
    private GameObject me;
    private GameObject bord;
    private GameObject manager;
    public OrderUI visuals;
    [SerializeField] private Slider timer;
    [SerializeField] private Material happy;
    [SerializeField] private Material sad;


    private void Update()
    {
        if (patience >= 0)
        {
            patience -= Time.deltaTime;
            timer.value = patience;
        }
        else if (!isDone && patience <= 0)
        {
            StartCoroutine(nameof(Leave));
            isDone = true;
        }
    }
    //a simple tag compare, comparing the order they got and what they actually ordered, then acts based upon if it was the correct order or not
    public void CompareOrder(GameObject meal)
    {
        meal.transform.SetParent(bord.transform);
        meal.transform.position = meal.transform.parent.position;
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
        isDone = true;
        StartCoroutine(nameof(Leave));
    }

    // gets called when a new customer is created to give them their random variables
    public void SetVariables(int stool, GameObject cm, GameObject plate)
    {
        visuals = GetComponent<OrderUI>();
        glass = UnityEngine.Random.Range(0, 9);
        if (glass <= 3)
        {
            glassSize = 4;
        }
        else if (glass > 3 && glass <= 6)
        {
            glassSize = 5;
        }
        else
        {
            glassSize = 6;
        }
        money = UnityEngine.Random.Range(20, 30);
        patience = UnityEngine.Random.Range(5, 10);
        timer.maxValue = (int)patience;
        delay = UnityEngine.Random.Range(5, 10);
        garnish = UnityEngine.Random.Range(0, 9);
        drinks = new int[glassSize];
        for (int i = 0; i < glassSize; i++)
        {
            drinks[i] = UnityEngine.Random.Range(0, 9);
        }
        Array.Sort(drinks);
        seatNumber = stool;
        manager = cm;
        me = this.gameObject;
        bord = plate;
        visuals.DisplayOrder(glassSize, glass, garnish, drinks);
    }

    IEnumerator Leave()
    {
        if (isCorrect)
        {
            manager.GetComponent<CustomerManager>().CompleteOrder(money);
        }
        else
        {
            manager.GetComponent<CustomerManager>().CompleteOrder(0);
        }

        yield return new WaitForSeconds(delay);
        manager.GetComponent<CustomerManager>().DestroyCustomer(seatNumber);
        Destroy(me);
        if (isDone && !isCorrect)
        {
            yield break;
        }
        Destroy(bord.transform.GetChild(0).gameObject);
    }
}