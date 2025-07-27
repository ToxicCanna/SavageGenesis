using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DR_Debug : MonoBehaviour
{
    // NOTICE: This is a debug script for the DinoRevival scene, to test and ensure the functionality of the buttons when clicked, to add and remove fossils, or in this case, the letter F. Also useful to see how to hook into and access the UI to add/remove fossils via clicking the buttons. Optional to follow, of course.

    private DinoRevivalUIManager ui;
    private bool[] hasFossil;

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
        ui.OnFossilSlotClicked += ToggleDebugText;
        ui.OnReviveClicked += SimulateRevival;
    }

    private void ToggleDebugText(int index)
    {
        hasFossil[index] = !hasFossil[index];
        ui.currentFossilObjects[index] = hasFossil[index] ? $"DebugFossil{index}" : null;

        // Update button text
        TMP_Text tmpText = ui.fossilSlotButtons[index].GetComponentInChildren<TMP_Text>();
        if (tmpText != null)
        {
            tmpText.text = hasFossil[index] ? "F" : "";
        }
    }

    private void SimulateRevival()
    {
        Debug.Log("=== [DR_Debug] Reviving Fossils ===");

        object[] fossils = ui.GetCurrentFossilContents();

        for (int i = 0; i < fossils.Length; i++)
        {
            if (fossils[i] != null)
            {
                Debug.Log($"[DR_Debug] Slot {i}: Revived Fossil");
            }
        }

        ui.ClearAllFossilSlots();
    }
}
