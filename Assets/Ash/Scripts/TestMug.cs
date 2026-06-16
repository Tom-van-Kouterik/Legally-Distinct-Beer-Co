using System;
using UnityEngine;
using UnityEngine.UI;

public class TestMug : MonoBehaviour
{
    public int glassType;
    public int garnishType;
    private bool isDecorated = false;
    private bool sizeSet = false;
    public int[] drinkTypes;
    private int counter = 0;
    private int size;
    [SerializeField] private GameObject[] contents;
    [SerializeField] private Material[] visuals;
    [SerializeField] private GameObject garnish;
    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject canvas;

    public void SetSize(int glass)
    {
        if (glass >= 6 || sizeSet)
        {
            return;
        }
        glassType = glass;
        if (glass <= 1)
        {
            size = 4;
        }
        else if (glass > 1 && glass <= 2)
        {
            size = 5;
        }
        else
        {
            size = 6;
        }
        drinkTypes = new int[size];
        for (int i = 0; i < drinkTypes.Length; i++)
        {
            drinkTypes[i] = 10;
        }
        SpawnCells();
        sizeSet = true;
    }

    public void AddDrink(int type)
    {
        if (counter >= drinkTypes.Length || type >= 6)
        {
            return;
        }

        drinkTypes[counter] = type;
        UpdateCells();
        counter++;
    }

    private void UpdateCells()
    {
        Array.Sort(drinkTypes);
        for (int i = 0; i < drinkTypes.Length; i++)
        {
            contents[i].GetComponent<Image>().material = visuals[drinkTypes[i]];
        }
    }

    public void AddGarnish(int type)
    {
        if (isDecorated || type >= 6)
        {
            return;
        }
        garnishType = type;
        garnish.GetComponent<Image>().material = visuals[garnishType];
        isDecorated = true;
    }

    private void SpawnCells()
    {
        contents = new GameObject[size];
        GameObject selected;
        RectTransform UISpace;
        for (int i = 0; i < drinkTypes.Length; i++)
        {
            selected = Instantiate(cell, canvas.transform, true);
            selected.transform.localScale = canvas.transform.localScale;
            UISpace = selected.GetComponent<RectTransform>();
            UISpace.transform.localPosition = new Vector3(0, (0.25f / 2 * i) - 0.25f, 0);
            selected.transform.rotation = canvas.transform.rotation;
            UISpace.transform.rotation = canvas.transform.rotation;
            contents[i] = selected;
        }
        UpdateCells();
    }
}