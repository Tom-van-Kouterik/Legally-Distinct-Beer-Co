using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void YesPressed()
    {
        SceneManager.LoadScene("MainScreenPlaceholder");
    }

    public void NoPressed()
    {
        transform.parent.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
