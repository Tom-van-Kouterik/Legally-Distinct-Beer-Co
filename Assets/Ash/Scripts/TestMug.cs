using System;
using UnityEngine;
using UnityEngine.UI;

public class TestMug : MonoBehaviour
{
    public int glassType;
    public int garnishType;
    private bool isDecorated = false;
    private bool sizeSet = false;
    private bool isFull = false;
    public int[] drinkTypes;
    private int counter = 0;
    private int size;
    [SerializeField] private GameObject[] contents;
    [SerializeField] private Material[] visuals;
    [SerializeField] private Sprite[] spriteSheets;
    [SerializeField] private Sprite[] garnishImages = new Sprite[6];
    [SerializeField] private Sprite[] glassImages;
    [SerializeField] private GameObject garnish;
    [SerializeField] private GameObject glass;
    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject canvas;

    private void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            //garnishImages[i] = Resources.LoadAll<>()
        }
    }
    public void SetSize(int type)
    {
        if (type >= 6 || sizeSet)
        {
            return;
        }
        glassType = type;
        if (type <= 1)
        {
            this.size = 4;
        }
        else if (type > 1 && type <= 2)
        {
            this.size = 5;
        }
        else
        {
            this.size = 6;
        }
        drinkTypes = new int[this.size];
        for (int i = 0; i < drinkTypes.Length; i++)
        {
            drinkTypes[i] = 10;
        }
        glass.GetComponent<Image>().sprite = glassImages[type];
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
        if (counter >= drinkTypes.Length)
        {
            isFull = true;
        }
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
        if (isDecorated || type >= 6 || !isFull)
        {
            return;
        }
        garnishType = type;
        //garnish.GetComponent<Image>().sprite = garnishImages[glassType, garnishType];
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