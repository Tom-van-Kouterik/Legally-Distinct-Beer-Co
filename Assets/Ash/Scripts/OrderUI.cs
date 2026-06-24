using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject glassCell;
    [SerializeField] private Material[] visuals;
    [SerializeField] public Sprite[,] garnishImages = new Sprite[6, 6];
    [SerializeField] private Sprite[] glassImages_0;
    [SerializeField] private Sprite[] glassImages_1;
    [SerializeField] private Sprite[] glassImages_2;
    [SerializeField] private Sprite[] glassImages_3;
    [SerializeField] private Sprite[] glassImages_4;
    [SerializeField] private Sprite[] glassImages_5;
    [SerializeField] private List <GameObject> contents;
    [SerializeField] private GameObject cell;

    private void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            garnishImages[0, i] = glassImages_0[i];
            garnishImages[1, i] = glassImages_1[i];
            garnishImages[2, i] = glassImages_2[i];
            garnishImages[3, i] = glassImages_3[i];
            garnishImages[4, i] = glassImages_4[i];
            garnishImages[5, i] = glassImages_5[i];
        }
    }
    public void DisplayOrder(int volume, int glassType, int garnishType, int[] liquids)
    {
        if (volume == 4)
        {
            for (int i = 0; i < 2; i++)
            {
                Destroy(contents[contents.Count - 1]);
                contents.RemoveAt(contents.Count - 1);
            }
        }
        else if (volume == 5)
        {
            Destroy(contents[contents.Count - 1]);
            contents.RemoveAt(contents.Count - 1);
        }

        for (int i = 0; i < contents.Count; i++)
        {
            contents[i].GetComponent<Image>().material = visuals[liquids[i]];
        }
        glassCell.GetComponent<Image>().sprite = garnishImages[garnishType, glassType];
    }
}