using Unity.VisualScripting;
using UnityEngine;

public class ItemWobble : MonoBehaviour
{
    //List of all the variables this script uses
    [SerializeField]
    public bool isBeingLookedAt = false;
    public Vector3 defaultScale;
    private float pulseSpeed = 2.0f;
    private float minScale;
    private float maxScale = 0.05f;
    
    //On start the defaultScale gets set to the scale of the object
    void Start()
    {
        defaultScale = transform.localScale;
    }
    
    //While the player looks at the item it will pulsate between the defaultScale and defaultScale + maxScale over a certain amount of time
    //when the player isn't looking at the item it will reset to the defaultScale value
    void Update()
    {
        float scale = Mathf.Lerp(minScale = defaultScale.x, defaultScale.x + maxScale, (Mathf.Sin(Time.time * pulseSpeed) + 1.0f) / 2.0f);
        if (isBeingLookedAt)
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }
        else
        {
            transform.localScale = defaultScale;
        } 
    }
}
