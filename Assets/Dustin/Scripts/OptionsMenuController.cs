using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuController : MonoBehaviour
{
    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu_Scene");
    }
}
