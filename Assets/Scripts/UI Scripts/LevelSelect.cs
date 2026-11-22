using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void backToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void loadLevel1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }

    public void loadLevel2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

    public void loadLevel3()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(6);
    }

    public void loadLevel4()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(7);
    }

    public void loadLevel5()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(8);
    }
}
