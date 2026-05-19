using UnityEngine;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject glass;
    [SerializeField] private GameObject garnish;
    [SerializeField] private Material[] visuals;
    [SerializeField] private GameObject[] contents;
    [SerializeField] private GameObject[] pictures;
    private int size;

    private void Awake()
    {
        contents = new GameObject[1];
    }
    public void DisplayOrder(int volume, int glassType, int garnishType, int[] liquids)
    {
        if (volume <= 3)
        {
            size = 0;
        }
        else if (volume > 3 && volume <= 6)
        {
            size = 1;
        }
        else
        {
            size = 2;
        }
            MeshRenderer selected;
        contents = new GameObject[volume];
        for (int i = 0; i < contents.Length; i++)
        {

            selected = contents[i].GetComponent<MeshRenderer>();
            selected.material = visuals[liquids[i]];
        }
    }
}
