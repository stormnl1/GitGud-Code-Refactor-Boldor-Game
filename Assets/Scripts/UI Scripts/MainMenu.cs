using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LevelSelectScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    private const string SkinKey = "PlayerSkinIndex";

    [Header("Optional: menu preview character")]
    [SerializeField] private PlayerSkin previewPlayerSkin;

    // Hook these up to your 4 buttons: SelectSkin(0), SelectSkin(1), ...
    public void SelectSkin(int index)
    {
        PlayerPrefs.SetInt(SkinKey, index);
        PlayerPrefs.Save(); // forces write to disk now

        // Optional: update preview model in the menu
        if (previewPlayerSkin != null)
            previewPlayerSkin.ApplySkin(index);
    }

}
