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
    private int score;
    private int wrong;
    private int correct;
    private int spawnedItems = 0;

    private void Start()
    {
        isTaken = new bool[itemSpawns.Length];
        SpawnIngredients();
        SpawnIngredients();
    }
    private void ShiftStart()
    {

    }

    private void ShiftEnd()
    {

    }

    private void SpawnIngredients()
    {
        int rng = Random.Range(0, glasses.Length);
        int spawn = Random.Range(0, itemSpawns.Length);
        if(spawnedItems >= itemSpawns.Length)
        {
            return;
        }

        if(spawnedItems == 0)
        {
            Instantiate(glassPrefabs[rng], itemSpawns[spawn].transform.position, itemSpawns[spawn].transform.rotation, itemSpawns[spawn].transform.parent).transform.SetParent(itemSpawns[spawn].transform);
            isTaken[spawn] = true;
            glasses[rng] = true;
            spawnedItems++;
            rng = Random.Range(0, 6);
            spawn = Random.Range(0, itemSpawns.Length);
            while (isTaken[spawn])
            {
                spawn = Random.Range(0, 6);
            }
            Instantiate(garnishPrefabs[rng], itemSpawns[spawn].transform.position, itemSpawns[spawn].transform.rotation, itemSpawns[spawn].transform.parent).transform.SetParent(itemSpawns[spawn].transform);
            isTaken[spawn] = true;
            garnishes[rng] = true;
            spawnedItems++;
            rng = Random.Range(0, 6);
            spawn = Random.Range(0, itemSpawns.Length);
            while (isTaken[spawn])
            {
                spawn = Random.Range(0, 6);
            }
            Instantiate(glassPrefabs[rng], itemSpawns[spawn].transform.position, itemSpawns[spawn].transform.rotation, itemSpawns[spawn].transform.parent).transform.SetParent(itemSpawns[spawn].transform);
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
        }
        else
        {
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
        }
    }
}