using UnityEngine;
using UnityEngine.SceneManagement;

public class DinoRevivalBackHandler : MonoBehaviour
{
    [Tooltip("Name of the scene to return to when the BackButton is pressed.")]
    public string returnSceneName = "NavigationScene";

    private DinoRevivalUIManager ui;

    private void Start()
    {
        ui = FindFirstObjectByType<DinoRevivalUIManager>();
        if (ui == null)
        {
            Debug.LogError("[DinoRevivalBackHandler] Could not find DinoRevivalUIManager in scene.");
            return;
        }

        ui.OnBackClicked += HandleBackPressed;
    }

    private void HandleBackPressed()
    {
        Debug.Log("[DinoRevivalBackHandler] Back pressed. Returning fossils and switching scenes...");
        ui.ReturnUnrevivedFossils();

        if (!string.IsNullOrEmpty(returnSceneName))
        {
            SceneManager.LoadScene(returnSceneName);
        }
        else
        {
            Debug.LogWarning("[DinoRevivalBackHandler] No return scene name specified.");
        }
    }
}
