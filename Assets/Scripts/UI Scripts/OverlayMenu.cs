using UnityEngine;
using UnityEngine.SceneManagement;

public class OverlayMenu : MonoBehaviour
{   
    public GameObject overlayMenu;

    public void ResumeGame()
    {
        overlayMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryLevel()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
