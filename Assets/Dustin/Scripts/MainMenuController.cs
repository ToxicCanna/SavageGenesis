using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("NavigationScene");
    }

    public void OpenOptions()
    {
        NavigationContext.ReturnSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("OptionsMenu_Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
