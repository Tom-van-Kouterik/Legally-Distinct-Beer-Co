using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework;

public class DevTools : MonoBehaviour
{
    private int selected;
    private GameObject highlightedCustomer;
    private GameObject heldGlass;
    [SerializeField] private Material[] states;
    [SerializeField] private TextMeshProUGUI[] drinkContentsDisplay;
    [SerializeField] private TMP_InputField[] values;
    [SerializeField] private GameObject glassPrefab;
    [SerializeField] private CustomerManager manager;
    [SerializeField] private GameObject basicUI;
    [SerializeField] private GameObject drinkUI;
    [SerializeField] private GameObject glassSpawn;
    private bool isMenuOpen = false;

    //copy customer selection/switch
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
    }
    //drink making UI
    public void SwitchUI()
    {
        if (isMenuOpen)
        {
            drinkUI.SetActive(false);
            basicUI.SetActive(true);
            isMenuOpen = false;
        }
        else
        {
            drinkUI.SetActive(true);
            basicUI.SetActive(false);
            isMenuOpen = true;
        }
    }

    //spawning glass
    public void SpawnGlass()
    {
        if (heldGlass != null)
        {
            return;
        }
        heldGlass = Instantiate(glassPrefab, glassSpawn.transform);
    }

    //drink making logic
    public void SetContents()
    {
        if (heldGlass == null)
        {
            return;
        }
        TestMug held = heldGlass.GetComponent<TestMug>();
        held.glassType = int.Parse(values[0].text);
        held.garnishType = int.Parse(values[1].text);
        for (int i = 0; i < int.Parse(values[2].text); i++)
        {
            held.drinkTypes[i] = int.Parse(values[3].text);
        }
        drinkContentsDisplay[0].text = ("glass" + held.glassType);
        drinkContentsDisplay[1].text = ("garnish" + held.garnishType);
        for (int i = 0; i < held.drinkTypes.Length; i++)
        {
        drinkContentsDisplay[i+2].text = ("glass" + held.drinkTypes[i]);
        }
        Debug.Log("fully cleared");
    }

    public void MakeOrder()
    {
        Customers C = highlightedCustomer.GetComponent<Customers>();
        C.glass = Random.Range(0,9);
        C.garnish = Random.Range(0, 9);
        for (int i = 0;i < 3;i++)
        {
            C.drinks[i] = Random.Range(0, 9);
        }
    }
}
