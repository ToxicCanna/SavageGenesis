using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DinoPCUIManager : MonoBehaviour
{
    [Header("Prefabs & Containers")]
    [SerializeField] private GameObject dinoButtonPrefab;
    [SerializeField] private Transform dinoListContainer;

    [Header("Stat Panel")]
    [SerializeField] private GameObject statPanel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text[] moveTexts;
    [SerializeField] private TMP_Text moveDescriptionText;
    [SerializeField] private TMP_Text movesHeaderText;

    [Header("Move Descriptions")]
    [SerializeField] private string[] dummyMoveDescriptions;

    private List<DinoData> dummyDinos = new List<DinoData>();

    private void Start()
    {
        GenerateDummyData(); // Replace this with your own data source
        PopulateDinoList();
        statPanel.SetActive(false);
    }

    // Replace this method with the real dino list
    // Example (I'm not sure how the inventory is handled so don't use this comment as a guide), use InventoryManager.GetDinoList() or pass the data externally 
    private void GenerateDummyData()
    {
        dummyDinos.Add(new DinoData { dinoName = "Raptor", health = 80, attack = 25, moves = new[] { "> Pounce", "> Bite", "> Dash", "> Sneak" } });
        dummyDinos.Add(new DinoData { dinoName = "Stego", health = 120, attack = 15, moves = new[] { "> Tail Swipe", "> Stomp", "> Guard", "> Ram" } });
        dummyDinos.Add(new DinoData { dinoName = "T-Rex", health = 150, attack = 35, moves = new[] { "> Roar", "> Crunch", "> Slam", "> Thrash" } });

        // Make sure to have the same number of move descriptions if you use this!
        if (dummyMoveDescriptions.Length < 4)
        {
            dummyMoveDescriptions = new string[]
            {
                "A leaping attack with claws.",
                "A powerful bite to the neck.",
                "A quick dash forward.",
                "A stealthy approach to avoid detection."
            };
        }
    }

    // Spawns dino buttons into the list
    // Repalce 'dummyDinos' with your own dino data source
    private void PopulateDinoList()
    {
        foreach (var dino in dummyDinos)
        {
            GameObject btnObj = Instantiate(dinoButtonPrefab, dinoListContainer);
            TMP_Text txt = btnObj.GetComponentInChildren<TMP_Text>();
            txt.text = dino.dinoName;

            btnObj.GetComponent<Button>().onClick.AddListener(() => ShowStats(dino));
        }
    }

    // Displays the selected dino's stats and moves
    // Move descriptions come from dummpMoveDescriptions[], matched by index
    private void ShowStats(DinoData dino)
    {
        statPanel.SetActive(true);
        nameText.text = dino.dinoName;
        healthText.text = $"HP: {dino.health}";
        attackText.text = $"ATK: {dino.attack}";

        if (movesHeaderText != null)
        {
            movesHeaderText.text = "Moves:";
        }

        for (int i = 0; i < moveTexts.Length; i++)
        {
            if (i < dino.moves.Length)
            {
                string move = dino.moves[i];
                moveTexts[i].text = move;
                int moveIndex = i;
                moveTexts[i].gameObject.SetActive(true);

                // Hook move hover to display the description
                EventTriggerListener.Get(moveTexts[i].gameObject).SetHover(
                    () => moveDescriptionText.text = (moveIndex < dummyMoveDescriptions.Length) ? dummyMoveDescriptions[moveIndex] : "No description.",
                    () => moveDescriptionText.text = ""
                );
            }
            else
            {
                moveTexts[i].text = "";
                moveTexts[i].gameObject.SetActive(false);
            }
        }
    }
}
