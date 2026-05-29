using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections;

public class DevTools : MonoBehaviour
{
    private int selected;
    private GameObject highlightedCustomer;
    private GameObject heldGlass;
    [SerializeField] private Slider glassValue;
    [SerializeField] private Slider drinkValue;
    [SerializeField] private Slider garnishValue;
    private TestMug data;
    [SerializeField] private Material[] states;
    [SerializeField] private GameObject glassPrefab;
    [SerializeField] private CustomerManager manager;
    [SerializeField] private GameObject glassSpawn;

    //copy customer selection/switch

    private void Awake()
    {
        StartCoroutine(nameof(Spawn));
    }
    public void Switch()
    {
        MeshRenderer selection;
        if (manager.people[selected] != null)
        {
            selection = manager.people[selected].GetComponent<MeshRenderer>();
            selection.material = states[0];
        }

        selected++;
        if (selected >= manager.people.Count)
        {
            selected = 0;
        }

        if (manager.people[selected] == null)
        {
            return;
        }
        else
        {
            highlightedCustomer = manager.people[selected];
            selection = manager.people[selected].GetComponent<MeshRenderer>();
            selection.material = states[1];
        }
    }
    //call compare order from customer
    public void Serve()
    {
        Customers cScript = highlightedCustomer.GetComponent<Customers>();
        cScript.CompareOrder(heldGlass);
        heldGlass = null;
    }

    //spawning glass
    public void SpawnGlass()
    {
        if (heldGlass != null || ((int)glassValue.value) >= 10)
        {
            return;
        }
        heldGlass = Instantiate(glassPrefab, glassSpawn.transform);
        data = heldGlass.GetComponent<TestMug>();
        data.SetSize((int)glassValue.value);
    }
    public void AddDrink()
    {
        if (heldGlass == null || ((int)drinkValue.value) >= 10)
        {
            return;
        }
        data.AddDrink((int)drinkValue.value);
    }

    public void AddGarnish()
    {
        if (heldGlass == null || ((int)garnishValue.value) >= 10)
        {
            return;
        }
        data.AddGarnish((int)garnishValue.value);
    }

    IEnumerator Spawn()
    {
        manager.SpawnCustomer();
        yield return new WaitForSeconds(10);
        StartCoroutine(nameof(Spawn));
    }
}
