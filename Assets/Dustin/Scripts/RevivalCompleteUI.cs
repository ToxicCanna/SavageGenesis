using UnityEngine;
using UnityEngine.UI;

public class RevivalCompleteUI : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private GameObject confirmPanel;   
    [SerializeField] private Button closeButton;     

    private void Start()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(Hide);

        // Make sure it's hidden by default
        backgroundPanel.SetActive(false);
        confirmPanel.SetActive(false);
    }

    public void Show()
    {
        backgroundPanel.SetActive(true);
        confirmPanel.SetActive(true);
    }

    private void Hide()
    {
        backgroundPanel.SetActive(false);
        confirmPanel.SetActive(false);
    }
}
