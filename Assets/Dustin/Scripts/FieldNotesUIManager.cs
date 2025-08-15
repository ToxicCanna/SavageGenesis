using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FieldNotesUIManager : MonoBehaviour
{
    [SerializeField] private GameObject rootPanel;
    [SerializeField] private TMP_Text dinoNameText;
    [SerializeField] private TMP_Text dinoDescriptionText;
    [SerializeField] private Image dinoImage;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button closeButton;

    private int currentIndex = 0;
    //private List<DinoData> dummyDinos = new List<DinoData>();
    [SerializeField] private DinosaurDex dinoDex;
    private void Start()
    {
        //GenerateDummyDinos();

        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
        closeButton.onClick.AddListener(Hide);

        Hide(); // Hide on start
    }

    /*private void GenerateDummyDinos()
    {
        dummyDinos.Add(new DinoData { dinoName = "Velociraptor", moves = new[] { "Pounce", "Flank", "Slash" } });
        dummyDinos.Add(new DinoData { dinoName = "Triceratops", moves = new[] { "Charge", "Stomp", "Brace" } });
        dummyDinos.Add(new DinoData { dinoName = "Tyrannosaurus Rex", moves = new[] { "Roar", "Crunch", "Slam" } });
    }*/

    private void UpdateDisplay()
    {
        if (dinoDex.info.Length == 0) return;

        //DinoData current = dummyDinos[currentIndex];
        DinosaurInfo current = dinoDex.info[currentIndex];
        dinoNameText.text = current.dinosaurName;
        dinoDescriptionText.text = current.dinosaurDescription;
        dinoImage.sprite = current.dinosaurCombatSprite;
    }

    private void NextPage()
    {
        currentIndex = (currentIndex + 1) % dinoDex.info.Length;
        UpdateDisplay();
    }

    private void PrevPage()
    {
        currentIndex = (currentIndex - 1 + dinoDex.info.Length) % dinoDex.info.Length;
        UpdateDisplay();
    }

    public void Show()
    {
        rootPanel.SetActive(true);
        UpdateDisplay();
    }

    public void Hide()
    {
        rootPanel.SetActive(false);
    }
}
