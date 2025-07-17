using UnityEngine;

public class SkillSelection : MonoBehaviour
{
    [SerializeField] private Vector3 cursorLocationPresetOne;
    [SerializeField] private Vector3 cursorLocationPresetTwo;
    [SerializeField] private Vector3 cursorLocationPresetThree;
    [SerializeField] private Vector3 cursorLocationPresetFour;
    [SerializeField] private Vector3 cursorLocationPresetFive;

    [SerializeField] private LevelInfo levelInfo;

    private SkillSlot cursorHover;

    void OnEnable()
    {
        transform.localPosition = cursorLocationPresetOne;
        cursorHover = SkillSlot.One;
        PlayerController.Instance.SkillSelectionAddListener();
    }

    void OnDisable()
    {
        PlayerController.Instance.SkillSelectionRemoveListener();
    }

    public void PlayerPressedUp()
    {
        if (cursorHover == SkillSlot.One || cursorHover == SkillSlot.Two)
        {
            cursorHover = SkillSlot.Five;
            transform.localPosition = cursorLocationPresetFive;
        } else if (cursorHover == SkillSlot.Three)
        {
            cursorHover = SkillSlot.One;
            transform.localPosition = cursorLocationPresetOne;
        }
        else if (cursorHover == SkillSlot.Four)
        {
            cursorHover = SkillSlot.Two;
            transform.localPosition = cursorLocationPresetTwo;
        }
        else if (cursorHover == SkillSlot.Five)
        {
            cursorHover = SkillSlot.Three;
            transform.localPosition = cursorLocationPresetThree;
        }
    }

    public void PlayerPressedDown() {
        if (cursorHover == SkillSlot.One)
        {
            cursorHover = SkillSlot.Three;
            transform.localPosition = cursorLocationPresetThree;
        }
        else if (cursorHover == SkillSlot.Two)
        {
            cursorHover = SkillSlot.Four;
            transform.localPosition = cursorLocationPresetFour;
        }
        else if (cursorHover == SkillSlot.Three || cursorHover == SkillSlot.Four)
        {
            cursorHover = SkillSlot.Five;
            transform.localPosition = cursorLocationPresetFive;
        }
        else if (cursorHover == SkillSlot.Five)
        {
            cursorHover = SkillSlot.One;
            transform.localPosition = cursorLocationPresetOne;
        }
    }

    public void PlayerPressedLeft() {
        if (cursorHover == SkillSlot.One)
        {
            cursorHover = SkillSlot.Two;
            transform.localPosition = cursorLocationPresetTwo;
        }
        else if (cursorHover == SkillSlot.Two)
        {
            cursorHover = SkillSlot.One;
            transform.localPosition = cursorLocationPresetOne;
        }
        else if (cursorHover == SkillSlot.Three)
        {
            cursorHover = SkillSlot.Four;
            transform.localPosition = cursorLocationPresetFour;
        }
        else if (cursorHover == SkillSlot.Four)
        {
            cursorHover = SkillSlot.Three;
            transform.localPosition = cursorLocationPresetThree;
        }
    }

    public void PlayerPressedRight() {
        if (cursorHover == SkillSlot.One)
        {
            cursorHover = SkillSlot.Two;
            transform.localPosition = cursorLocationPresetTwo;
        }
        else if (cursorHover == SkillSlot.Two)
        {
            cursorHover = SkillSlot.One;
            transform.localPosition = cursorLocationPresetOne;
        }
        else if (cursorHover == SkillSlot.Three)
        {
            cursorHover = SkillSlot.Four;
            transform.localPosition = cursorLocationPresetFour;
        }
        else if (cursorHover == SkillSlot.Four)
        {
            cursorHover = SkillSlot.Three;
            transform.localPosition = cursorLocationPresetThree;
        }
    }

    public void PlayerPressedConfirm() {
        
    }

    public void PlayerPressedCancel() 
    {
        levelInfo.GetActionSelector().SetActive(true);
        levelInfo.GetActionSelectorCursor().SetActive(true);

        levelInfo.GetSkillSelector().SetActive(false);
        levelInfo.GetSkillSelectorCursor().SetActive(false);
        levelInfo.GetMoveTypeText().SetActive(false);
        levelInfo.GetMovePowerText().SetActive(false);
    }
}
