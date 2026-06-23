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
    [SerializeField] public Sprite[,] garnishImages = new Sprite[8,6];
    [SerializeField] private Sprite[] glassImagesEmpty;
    [SerializeField] private Sprite[] glassImagesFull;
    [SerializeField] private Sprite[] glassImages_0;
    [SerializeField] private Sprite[] glassImages_1;
    [SerializeField] private Sprite[] glassImages_2;
    [SerializeField] private Sprite[] glassImages_3;
    [SerializeField] private Sprite[] glassImages_4;
    [SerializeField] private Sprite[] glassImages_5;
    [SerializeField] private GameObject glass;
    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject canvas;

    private void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            garnishImages[0,i] = glassImages_0[i];
            garnishImages[1, i] = glassImages_1[i];
            garnishImages[2, i] = glassImages_2[i];
            garnishImages[3, i] = glassImages_3[i];
            garnishImages[4, i] = glassImages_4[i];
            garnishImages[5, i] = glassImages_5[i];
            garnishImages[6, i] = glassImagesFull[i];
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
            size = 4;
        }
        else if (type > 1 && type <= 2)
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
            drinkTypes[i] = 6;
        }
        glass.GetComponent<SpriteRenderer>().sprite = glassImagesEmpty[type];
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
            glass.GetComponent<SpriteRenderer>().sprite = glassImagesFull[glassType];
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
        glass.GetComponent<SpriteRenderer>().sprite = garnishImages[garnishType, glassType];
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