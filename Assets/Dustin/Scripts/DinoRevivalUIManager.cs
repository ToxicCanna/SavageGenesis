using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class DinoRevivalUIManager : MonoBehaviour
{
    [Header("UI References")]
    public Button[] fossilSlotButtons;
    public Button backButton;
    public Button reviveButton;

    // Visual layers for spinning overlays or slot effects, ignore/comment out if we don't go with the spinning idea
    public GameObject[] spinningOverlays;

    // Callback actions for inventory system to subscribe to
    public Action<int> OnFossilSlotClicked;
    public Action OnReviveClicked;
    public Action OnBackClicked;

    // Called when a fossil should be returned to the inventory (such as when the player presses the 'back' button but has not recollected their fossils
    public Action<object> OnFossilReturned;

    // Holds the fossil data in each slot. Replace 'object' with a real/other type if required
    [HideInInspector] public object[] currentFossilObjects = new object[5];

    // Optional delay before revive is complete, set to 0 by default
    public float revivalDelay = 0f;

    [SerializeField] private RevivalCompleteUI confirmationUI;

    private void Start()
    {
        for (int i = 0; i < fossilSlotButtons.Length; i++)
        {
            int index = i;
            fossilSlotButtons[i].onClick.AddListener(() => HandleSlotClick(index));
        }

        backButton.onClick.AddListener(() => OnBackClicked?.Invoke());
        reviveButton.onClick.AddListener(() => StartRevivalProcess());
    }

    private void HandleSlotClick(int index)
    {
        Debug.Log($"[DinoRevivalUIManager] Slot {index} clicked.");
        OnFossilSlotClicked?.Invoke(index);
    }

    // Toggle spin overlays
    public void ToggleSpinOverlay(int index, bool enabled)
    {
        if (spinningOverlays != null && index < spinningOverlays.Length)
        {
            spinningOverlays[index].SetActive(enabled);
        }
    }

    // Returns current fossil contents for inventory access
    public object[] GetCurrentFossilContents()
    {
        return currentFossilObjects;
    }

    // Clears slot data and UI display
    public void ClearAllFossilSlots()
    {
        for (int i = 0; i < fossilSlotButtons.Length; i++)
        {
            currentFossilObjects[i] = null;

            // Clears the current TMP lable if required
            TMP_Text tmpText = fossilSlotButtons[i].GetComponentInChildren<TMP_Text>();
            if (tmpText != null)
            {
                tmpText.text = "";
            }
        }
    }

    // Returns fossils from slots back to inventory system, if unused
    public void ReturnUnrevivedFossils()
    {
        for (int i = 0; i < currentFossilObjects.Length; i++)
        {
            if (currentFossilObjects[i] != null)
            {
                Debug.Log($"[DinoRevivalUIManager] Returning fossil from slot {i}");
                OnFossilReturned?.Invoke(currentFossilObjects[i]); // Notifies the inventory
                currentFossilObjects[i] = null;
            }
        }

        ClearAllFossilSlots();
    }

    // Handles optional delay before revival logic is called
    public void StartRevivalProcess()
    {
        if (revivalDelay > 0f)
        {
            StartCoroutine(DelayedRevival());
        }
        else
        {
            OnReviveClicked?.Invoke();

            if (confirmationUI != null)
            {
                confirmationUI.Show();
            }
        }
    }

    // Delayed invoke of revive, only used if delay is set
    private IEnumerator DelayedRevival()
    {
        yield return new WaitForSeconds(revivalDelay);
        OnReviveClicked?.Invoke();

        if (confirmationUI != null)
        {
            confirmationUI.Show();
        }
    }
}
