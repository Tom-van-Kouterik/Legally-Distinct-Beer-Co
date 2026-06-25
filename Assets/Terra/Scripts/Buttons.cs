using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Canvas crosshairUI;
    [SerializeField] private Canvas pauseUI;

    [SerializeField] private Canvas tutorialUI;
    [SerializeField] private Canvas creditsUI;
    public void YesPressed()
    {
        SceneManager.LoadScene("0");
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

    public void GameScene()
    {
       SceneManager.LoadScene("1");
    }

    public void CreditsButton()
    {
        creditsUI.gameObject.SetActive(true);
    }

    public void CreditsBackButton()
    {
        creditsUI.gameObject.SetActive(false);
    }

    public void CloseTutorial()
    {
        crosshairUI.gameObject.SetActive(true);
        tutorialUI.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
