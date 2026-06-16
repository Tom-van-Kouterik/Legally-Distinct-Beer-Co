using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject glassCell;
    [SerializeField] private GameObject garnishCell;
    [SerializeField] private Material[] visuals;
    [SerializeField] private Sprite[] glasses;
    [SerializeField] private Sprite[] garnish;
    [SerializeField] private GameObject[] contents;
    [SerializeField] private GameObject cell;

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
        glassCell.GetComponent<Image>().sprite = glasses[glassType];
        garnishCell.GetComponent<Image>().sprite = garnish[garnishType];
    }
}