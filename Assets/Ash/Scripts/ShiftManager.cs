using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShiftManager : MonoBehaviour
{
    [SerializeField] private CustomerManager subManager;
    [SerializeField] private GameObject[] itemSpawns;
    [SerializeField] private bool[] drinks = new bool[6];
    [SerializeField] private bool[] glasses = new bool[6];
    [SerializeField] private bool[] garnishes = new bool[6];
    [SerializeField] private GameObject[] drinkPrefabs;
    [SerializeField] private GameObject[] glassPrefabs;
    [SerializeField] private GameObject[] garnishPrefabs;
    [SerializeField] private GameObject candle;
    [SerializeField] private GameObject canvas;
    private bool[] isTaken;
    public int score;
    public int wrong;
    public int correct;
    private int completedShifts = 0;
    private int spawnedItems = 0;
    private int maxTime = 161;
    private float timer;
    private bool isOn = false;
    private bool downTime = false;
    private bool spawnDelay = false;

    private void Start()
    {
        isTaken = new bool[itemSpawns.Length];
        timer = maxTime;
        SetUp();
    }

    private void Update()
    {
        if (timer >= 0 && isOn)
        {
            timer -= Time.deltaTime;
        }
        else if (timer <= 0 && !downTime)
        {
            isOn = false;
            ShiftEnd();
        }

        if (Mathf.Ceil(timer) % 20 == 0f && !spawnDelay && isOn)
        {
            subManager.SpawnCustomer();
            spawnDelay = true;
            StartCoroutine(nameof(Spawn));
        }

        if ((timer / maxTime) * 100 <= 100 && (timer / maxTime) * 100 > 75)
        {
            candle.transform.GetChild(0).gameObject.SetActive(true);
            candle.transform.GetChild(1).gameObject.SetActive(false);
            candle.transform.GetChild(2).gameObject.SetActive(false);
            candle.transform.GetChild(3).gameObject.SetActive(false);
        }
        else if ((timer / maxTime) * 100 <= 75 && (timer / maxTime) * 100 > 50)
        {
            candle.transform.GetChild(0).gameObject.SetActive(false);
            candle.transform.GetChild(1).gameObject.SetActive(true);
            candle.transform.GetChild(2).gameObject.SetActive(false);
            candle.transform.GetChild(3).gameObject.SetActive(false);
        }
        else if ((timer / maxTime) * 100 <= 55 && (timer / maxTime) * 100 > 25)
        {
            candle.transform.GetChild(0).gameObject.SetActive(false);
            candle.transform.GetChild(1).gameObject.SetActive(false);
            candle.transform.GetChild(2).gameObject.SetActive(true);
            candle.transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            candle.transform.GetChild(0).gameObject.SetActive(false);
            candle.transform.GetChild(1).gameObject.SetActive(false);
            candle.transform.GetChild(2).gameObject.SetActive(false);
            candle.transform.GetChild(3).gameObject.SetActive(true);
        }
    }
    public void ShiftStart()
    {
        if(completedShifts >= 3 || isOn)
        {
            return;
        }
        SpawnIngredients();
        downTime = false;
        timer = maxTime;
        isOn = true;
    }

    public void ShiftEnd()
    {
        for (int i = 0; i < subManager.isOcupied.Count; i++)
        {
            if (subManager.isOcupied[i] == true)
            {
                return;
            }
        }
        downTime = true;
        completedShifts++;
        if (completedShifts >= 3)
        {
            ShowScore();
        }
    }

    private void SpawnIngredients()
    {
        int rng = Random.Range(0, glasses.Length);
        int spawn = Random.Range(0, itemSpawns.Length);
        if (spawnedItems >= itemSpawns.Length || completedShifts == 0)
        {
            return;
        }

        while (glasses[rng])
        {
            rng = Random.Range(0, 6);
        }
        while (isTaken[spawn])
        {
            spawn = Random.Range(0, itemSpawns.Length);
        }
        Instantiate(glassPrefabs[rng], itemSpawns[spawn].transform.position, itemSpawns[spawn].transform.rotation, itemSpawns[spawn].transform.parent).transform.SetParent(itemSpawns[spawn].transform);
        isTaken[spawn] = true;
        glasses[rng] = true;
        spawnedItems++;

        while (garnishes[rng])
        {
            rng = Random.Range(0, 6);
        }
        while (isTaken[spawn])
        {
            spawn = Random.Range(0, itemSpawns.Length);
        }
        Instantiate(garnishPrefabs[rng], itemSpawns[spawn].transform.position, itemSpawns[spawn].transform.rotation, itemSpawns[spawn].transform.parent).transform.SetParent(itemSpawns[spawn].transform);
        isTaken[spawn] = true;
        garnishes[rng] = true;
        spawnedItems++;

        while (drinks[rng])
        {
            rng = Random.Range(0, 6);
        }
        while (isTaken[spawn])
        {
            spawn = Random.Range(0, itemSpawns.Length);
        }
        Instantiate(drinkPrefabs[rng], itemSpawns[spawn].transform.position, itemSpawns[spawn].transform.rotation, itemSpawns[spawn].transform.parent).transform.SetParent(itemSpawns[spawn].transform);
        isTaken[spawn] = true;
        drinks[rng] = true;
        spawnedItems++;
        UpdateLists();
    }

    private void SetUp()
    {
        int rng = Random.Range(0, glasses.Length);
        int spawn = Random.Range(0, itemSpawns.Length);

        Instantiate(glassPrefabs[rng], itemSpawns[spawn].transform.position, itemSpawns[spawn].transform.rotation, itemSpawns[spawn].transform.parent).transform.SetParent(itemSpawns[spawn].transform);
        isTaken[spawn] = true;
        glasses[rng] = true;
        spawnedItems++;
        rng = Random.Range(0, 6);
        spawn = Random.Range(0, itemSpawns.Length);
        while (isTaken[spawn])
        {
            spawn = Random.Range(0, itemSpawns.Length);
        }
        Instantiate(garnishPrefabs[rng], itemSpawns[spawn].transform.position, itemSpawns[spawn].transform.rotation, itemSpawns[spawn].transform.parent).transform.SetParent(itemSpawns[spawn].transform);
        isTaken[spawn] = true;
        garnishes[rng] = true;
        spawnedItems++;
        rng = Random.Range(0, 6);
        spawn = Random.Range(0, itemSpawns.Length);
        while (isTaken[spawn])
        {
            spawn = Random.Range(0, itemSpawns.Length);
        }
        Instantiate(drinkPrefabs[rng], itemSpawns[spawn].transform.position, itemSpawns[spawn].transform.rotation, itemSpawns[spawn].transform.parent).transform.SetParent(itemSpawns[spawn].transform);
        isTaken[spawn] = true;
        drinks[rng] = true;
        spawnedItems++;
        rng = Random.Range(0, 6);
        spawn = Random.Range(0, itemSpawns.Length);

        for (int i = 0; i < 3; i++)
        {
            while (glasses[rng])
            {
                rng = Random.Range(0, 6);
            }
            while (isTaken[spawn])
            {
                spawn = Random.Range(0, itemSpawns.Length);
            }
            Instantiate(glassPrefabs[rng], itemSpawns[spawn].transform.position, itemSpawns[spawn].transform.rotation, itemSpawns[spawn].transform.parent).transform.SetParent(itemSpawns[spawn].transform);
            isTaken[spawn] = true;
            glasses[rng] = true;
            spawnedItems++;

            while (garnishes[rng])
            {
                rng = Random.Range(0, 6);
            }
            while (isTaken[spawn])
            {
                spawn = Random.Range(0, itemSpawns.Length);
            }
            Instantiate(garnishPrefabs[rng], itemSpawns[spawn].transform.position, itemSpawns[spawn].transform.rotation, itemSpawns[spawn].transform.parent).transform.SetParent(itemSpawns[spawn].transform);
            isTaken[spawn] = true;
            garnishes[rng] = true;
            spawnedItems++;

            while (drinks[rng])
            {
                rng = Random.Range(0, 6);
            }
            while (isTaken[spawn])
            {
                spawn = Random.Range(0, itemSpawns.Length);
            }
            Instantiate(drinkPrefabs[rng], itemSpawns[spawn].transform.position, itemSpawns[spawn].transform.rotation, itemSpawns[spawn].transform.parent).transform.SetParent(itemSpawns[spawn].transform);
            isTaken[spawn] = true;
            drinks[rng] = true;
            spawnedItems++;
        }
        UpdateLists();
    }
    
    private void UpdateLists()
    {
        subManager.drinkAcces = drinks;
        subManager.glassAcces = glasses;
        subManager.garnishAcces = garnishes;
    }

    private void ShowScore()
    {
        canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ("SCORE: " + score);
        canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ("HAPPY CUSTOMERS: " + correct);
        canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ("ANGRY CUSTOMERS: " + wrong);
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);
        spawnDelay = false;
    }
}