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
    [SerializeField] private GameObject[] contents;
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
        contents = new GameObject[volume];
        Image cellVisual;
        GameObject selected;
        RectTransform UISpace;
        for (int i = 0; i < volume; i++)
        {
            selected = Instantiate(cell,canvas.transform,true);
            selected.transform.localScale = canvas.transform.localScale;
            UISpace = selected.GetComponent<RectTransform>();
            UISpace.transform.localPosition = new Vector3(0, (0.25f / 2 * i), 0);
            contents[i] = selected;
            cellVisual = contents[i].GetComponent<Image>();
            cellVisual.material = visuals[liquids[i]];
        }
        glassCell.GetComponent<Image>().sprite = garnishImages[garnishType,glassType];
    }
}