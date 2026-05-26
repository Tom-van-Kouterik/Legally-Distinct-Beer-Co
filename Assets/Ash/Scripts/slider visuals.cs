using UnityEngine;
using UnityEngine.UI;

public class slidervisuals : MonoBehaviour
{
    [SerializeField] private Image plane;
    [SerializeField] private Material[] visuals;

    public void VisualUpdate()
    {
        plane.color = visuals[((int)gameObject.GetComponent<Slider>().value)].color;
    }
}
