using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavigationMenu : MonoBehaviour
{
    [Header("Scene Names")]
    public string digSceneName = "DigScene";
    public string reviveSceneName = "ReviveScene";
    public string stadiumSceneName = "StadiumScene";
    public string fightingPitSceneName = "FightingPitScene";
    // Feel free to set the scene name defaults either here, or set it via the inspector


    [Header("Navigation Buttons")]
    public Button digButton;
    public Button reviveButton;
    public Button stadiumButton;
    public Button fightingPitButton;

    [Header("Pause")]
    public Button pauseButton;
    public GameObject pausePanel; // UI panel to show/hide
    private bool isPaused = false;

    private void Start()
    {
        // Navigation buttons
        if (digButton != null)
        {
            digButton.onClick.AddListener(() => LoadScene(digSceneName));
        }

        if (reviveButton != null)
        {
            reviveButton.onClick.AddListener(() => LoadScene(reviveSceneName));
        }

        if (stadiumButton != null)
        {
            stadiumButton.onClick.AddListener(() => LoadScene(stadiumSceneName));
        }

        if (fightingPitButton != null)
        {
            fightingPitButton.onClick.AddListener(() => LoadScene(fightingPitSceneName));
        }

        // Pause button
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }

        // Ensure pause panel starts hidden
        if (pauseButton != null)
        {
            pausePanel.SetActive(false);
        }
    }

    private void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is not assigned.");
        }
    }

    public void TogglePause()
    {
        // NOTICE: To ensure the pause functionality actually works, do not attach the "TogglePause()" method to the "Pause Menu" button in the actul scene. Only the "Resume" button needs to call this method.
        
        isPaused = !isPaused;
        Debug.Log("TogglePause called. Paused = " + isPaused);

        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
            Debug.Log("Pause Panel toggled.");
        }
        else
        {
            Debug.LogWarning("Pause Panel is not assigned!");
        }
    }
}
