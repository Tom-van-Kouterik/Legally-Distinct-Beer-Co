using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool[] isTaken;
    public int score;
    public int wrong;
    public int correct;
    private int completedShifts = 0;
    private int spawnedItems = 0;
    private int maxTime = 601;
    private float timer;
    private bool isOn = false;
    private bool spawnDelay = false;

    private void Start()
    {
        isTaken = new bool[itemSpawns.Length];
        timer = maxTime;
        SetUp();
        ShiftStart();
    }

    private void Update()
    {
        if (timer >= 0 && isOn)
        {
            timer -= Time.deltaTime;
        }
        else if (timer <= 0 && isOn)
        {
            ShiftEnd();
        }

        if (Mathf.Ceil(timer) % 5 == 0f && !spawnDelay && isOn)
        {
            subManager.SpawnCustomer();
            spawnDelay = true;
            StartCoroutine(nameof(Spawn));
        }
    }
    public void ShiftStart()
    {
        SpawnIngredients();
        timer = maxTime;
        isOn = true;
    }

    public void ShiftEnd()
    {
        completedShifts++;
        isOn = false;
    }

    private void SpawnIngredients()
    {
        int rng = Random.Range(0, glasses.Length);
        int spawn = Random.Range(0, itemSpawns.Length);
        if (spawnedItems >= itemSpawns.Length || completedShifts == 0)
        {
            return;
        }

        for (int i = 0; i < 2; i++)
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
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);
        spawnDelay = false;
    }
}