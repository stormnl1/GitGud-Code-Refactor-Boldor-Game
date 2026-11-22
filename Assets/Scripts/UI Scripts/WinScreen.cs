using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
   [SerializeField] private GameObject winScreen;

   public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 

    public void ToLevelSelect()
    {
        SceneManager.LoadScene(1);
    }
}

