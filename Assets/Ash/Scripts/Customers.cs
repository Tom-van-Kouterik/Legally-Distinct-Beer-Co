using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Customers : MonoBehaviour
{
    public int glass;
    public int garnish;
    public int[] drinks;
    private int seatNumber;
    private int delay;
    private int money;
    private float patience;
    private bool isCorrect = false;
    private bool isDone = false;
    private bool isServed = false;
    private GameObject me;
    private GameObject bord;
    private GameObject manager;
    public OrderUI visuals;
    [SerializeField] private Slider timer;
    [SerializeField] private Material happy;
    [SerializeField] private Material sad;


    private void Update()
    {
        if (patience >= 0 && !isServed)
        {
            patience -= Time.deltaTime;
            timer.value = patience;
        }
        else if (!isDone && patience <= 0)
        {
            isDone = true;
            StartCoroutine(nameof(Leave));
        }
    }
    //a simple tag compare, comparing the order they got and what they actually ordered, then acts based upon if it was the correct order or not
    public void CompareOrder(GameObject meal)
    {
        isServed = true;
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
        StartCoroutine(nameof(Leave));
    }

    // gets called when a new customer is created to give them their random variables and asign relevant data
    public void SetVariables(int stool, GameObject cm, GameObject plate, int glassValue, int garnishValue, List<int> drinkValues)
    {
        visuals = GetComponent<OrderUI>();
        glass = glassValue;
        money = UnityEngine.Random.Range(20, 30);
        patience = UnityEngine.Random.Range(5, 10);
        timer.maxValue = (int)patience;
        delay = UnityEngine.Random.Range(5, 10);
        garnish = garnishValue;
        drinks = new int[drinkValues.Count];
        for (int i = 0; i < drinkValues.Count; i++)
        {
            drinks[i] = drinkValues[i];
        }
        Array.Sort(drinks);
        seatNumber = stool;
        manager = cm;
        me = this.gameObject;
        bord = plate;
        visuals.DisplayOrder(drinkValues.Count, glass, garnish, drinks);
    }

    IEnumerator Leave()
    {
        if (isDone)
        {
            manager.GetComponent<CustomerManager>().CompleteOrder(0);
            manager.GetComponent<CustomerManager>().DestroyCustomer(seatNumber);
            Destroy(me);
        }

        if (isCorrect)
        {
            manager.GetComponent<CustomerManager>().CompleteOrder(money + (int)patience);
            yield return new WaitForSeconds(delay);
            Destroy(bord.transform.GetChild(0).gameObject);
        }
        else
        {
            manager.GetComponent<CustomerManager>().CompleteOrder(0);
            yield return new WaitForSeconds(delay);
            Destroy(bord.transform.GetChild(0).gameObject);
        }

        yield return new WaitForSeconds(2);
        manager.GetComponent<CustomerManager>().DestroyCustomer(seatNumber);
        Destroy(me);
    }
}