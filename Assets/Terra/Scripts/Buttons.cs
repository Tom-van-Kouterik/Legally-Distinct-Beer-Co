using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Canvas crosshairUI;
    [SerializeField] private Canvas pauseUI;
    public void YesPressed()
    {
        SceneManager.LoadScene("MainScreenPlaceholder");
    }

    public void NoPressed()
    {
        transform.parent.gameObject.SetActive(false);
        crosshairUI.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;   
    }

    public void ContinuePressed()
    {
        Time.timeScale = 1f;
        pauseUI.gameObject.SetActive(false);    
        Cursor.lockState = CursorLockMode.Locked;
        crosshairUI.gameObject.SetActive(true);
    }
}
