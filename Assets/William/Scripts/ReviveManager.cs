using UnityEngine;
using UnityEngine.UI;

public class ReviveManager : Code.Scripts.Managers.Singleton<ReviveManager>
{
    private DinoRevivalUIManager ui;
    private bool[] hasFossil;

    private Button selectedSlot;
    private int selectedIndex;
    private RarityLevel[] revivalRaritys;
    private DinosaurType[] revivalFossilTypes;

    [SerializeField] private Image[] spritesForSlots;

    private SlotValue[] numOfFossils;
    private FossilStat[] fossils;

    private void Start()
    {
        //ui = FindObjectOfType<DinoRevivalUIManager>();
        ui = FindFirstObjectByType<DinoRevivalUIManager>();

        if (ui == null)
        {
            Debug.LogError("[DR_Debug] DinoRevivalUIManager not found in scene");
            return;
        }

        hasFossil = new bool[ui.fossilSlotButtons.Length];

        // Subscribe to UI events
        ui.OnFossilSlotClicked += SelectSlot;
        ui.OnReviveClicked += StartRevival;

        revivalRaritys = new RarityLevel[5];
        revivalFossilTypes = new DinosaurType[5];
        numOfFossils = new SlotValue[5];
        fossils = new FossilStat[5];
    }

    private void SelectSlot(int index)
    {
        //hasFossil[index] = !hasFossil[index];
        //ui.currentFossilObjects[index] = hasFossil[index] ? $"DebugFossil{index}" : null;

        selectedSlot = ui.fossilSlotButtons[index].GetComponent<Button>();
        selectedIndex = index;
    }

    public void SelectFossil(FossilStat fossilS, SlotValue sv)
    {
        if (hasFossil[selectedIndex] == false)
        {
            hasFossil[selectedIndex] = true;
            numOfFossils[selectedIndex] = sv;
            sv.RemoveOne();
        }
        else {
            numOfFossils[selectedIndex].AddOne();
            numOfFossils[selectedIndex] = sv;
            sv.RemoveOne();
        }

        spritesForSlots[selectedIndex].sprite = fossilS.itemImage;
        spritesForSlots[selectedIndex].enabled = true;

        fossils[selectedIndex] = fossilS;
        //for debug
        revivalRaritys[selectedIndex] = fossilS.rarity;
        revivalFossilTypes[selectedIndex] = fossilS.fossilType;
    }

    private void StartRevival()
    {
        bool ready = true;
        for (int i = 0; i < 5; i++)
        {
            if (!hasFossil[i])
            { ready = false; }
        }

        if (ready)
        {
            Debug.Log("Rarity: " + revivalRaritys[0] + " "+ revivalRaritys[1] + " " + revivalRaritys[2] + " " + revivalRaritys[3] + " " + revivalRaritys[4]);
            Debug.Log("Type: " + revivalFossilTypes[0] + " " + revivalFossilTypes[1] + " " + revivalFossilTypes[2] + " " + revivalFossilTypes[3] + " " + revivalFossilTypes[4]);

            DinoHolder.Instance.addDino(FossilToDino.Instance.MakeDino(fossils));

            CleanUp();
            ui.ClearAllFossilSlots();
        }

    }

    private void CleanUp()
    {
        for (int i = 0; i < 5; i++)
        {
            hasFossil[i] = false;
            spritesForSlots[i].enabled = false;
        }
        revivalRaritys = new RarityLevel[5];
        revivalFossilTypes = new DinosaurType[5];

    }

}
