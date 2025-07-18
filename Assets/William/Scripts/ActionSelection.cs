using UnityEngine;

public class ActionSelection : MonoBehaviour
{
    [SerializeField] private Vector3 cursorLocationPresetOne;
    [SerializeField] private Vector3 cursorLocationPresetTwo;
    [SerializeField] private Vector3 cursorLocationPresetThree;
    [SerializeField] private LevelInfo levelInfo;

    private ActionType cursorHover;

    private bool isApplicationQuitting = false;

    void OnEnable()
    { 
        transform.localPosition = cursorLocationPresetOne;
        cursorHover = ActionType.Attack;
        PlayerController.Instance.ActionSelectionAddListener();
    }

    void OnDisable()
    {
        if (isApplicationQuitting) return;
        
        PlayerController.Instance.ActionSelectionRemoveListener();
    }

    void OnApplicationQuit()
    {
        isApplicationQuitting = true;
    }

    public void PlayerPressedUp()
    {
        if (cursorHover == ActionType.Attack)
        {
            cursorHover = ActionType.Item;
            transform.localPosition = cursorLocationPresetThree;
        }
        else if (cursorHover == ActionType.Switch)
        {
            cursorHover = ActionType.Attack;
            transform.localPosition = cursorLocationPresetOne;
        } else if (cursorHover == ActionType.Item)
        {
            cursorHover = ActionType.Switch;
            transform.localPosition = cursorLocationPresetTwo;
        }
    }

    public void PlayerPressedDown()
    {
        if (cursorHover == ActionType.Attack)
        {
            
            cursorHover = ActionType.Switch;
            transform.localPosition = cursorLocationPresetTwo;
        }
        else if (cursorHover == ActionType.Switch)
        {
            cursorHover = ActionType.Item;
            transform.localPosition = cursorLocationPresetThree;

        }
        else if (cursorHover == ActionType.Item)
        {
            cursorHover = ActionType.Attack;
            transform.localPosition = cursorLocationPresetOne;
        }
    }

    public void PlayerPressedConfirm()
    {
        if (cursorHover == ActionType.Attack)
        {
            levelInfo.GetSkillSelector().SetActive(true);
            levelInfo.GetSkillSelectorCursor().SetActive(true);
            levelInfo.GetMoveTypeText().SetActive(true);
            levelInfo.GetMovePowerText().SetActive(true);

            levelInfo.GetActionSelector().SetActive(false);
            levelInfo.GetActionSelectorCursor().SetActive(false);
        }

    }

}
