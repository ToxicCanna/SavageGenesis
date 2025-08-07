using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenFieldNotesButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(AttemptToOpenFieldNotes);
    }

    private void AttemptToOpenFieldNotes()
    {
        FieldNotesUIManager ui = Object.FindFirstObjectByType<FieldNotesUIManager>();
        if (ui != null)
        {
            ui.Show();
        }
        else
        {
            Debug.LogWarning("[OpenFieldNotesButton] No FieldNotesUIManager found in scene.");
        }
    }
}
