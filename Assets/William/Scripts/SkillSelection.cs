using UnityEngine;
using UnityEngine.UI;

public class SkillSelection : MonoBehaviour
{
    [SerializeField] private Vector3 cursorLocationPresetOne;
    [SerializeField] private Vector3 cursorLocationPresetTwo;
    [SerializeField] private Vector3 cursorLocationPresetThree;
    [SerializeField] private Vector3 cursorLocationPresetFour;
    [SerializeField] private Vector3 cursorLocationPresetFive;

    [SerializeField] private LevelInfo levelInfo;

    private InventoryDinosaur combatSlotOne;

    private SkillSlot cursorHover;

    private bool isApplicationQuitting = false;

    public void LoadMoveSet()
    {
        combatSlotOne = levelInfo.GetPlayerDinoInventory().LoadCombatSlotOne();
        levelInfo.GetSkillOne().GetComponent<Text>().text = combatSlotOne.moveOne.moveName;
        if (combatSlotOne.moveTwoEmpty)
        {
            levelInfo.GetSkillTwo().GetComponent<Text>().text = "--";
            levelInfo.GetSkillThree().GetComponent<Text>().text = "--";
            levelInfo.GetSkillFour().GetComponent<Text>().text = "--";
            levelInfo.GetSkillFive().GetComponent<Text>().text = "--";
        }
        else if (combatSlotOne.moveThreeEmpty)
        {
            levelInfo.GetSkillTwo().GetComponent<Text>().text = combatSlotOne.moveTwo.moveName;
            levelInfo.GetSkillThree().GetComponent<Text>().text = "--";
            levelInfo.GetSkillFour().GetComponent<Text>().text = "--";
            levelInfo.GetSkillFive().GetComponent<Text>().text = "--";
        }
        else if (combatSlotOne.moveFourEmpty)
        {
            levelInfo.GetSkillTwo().GetComponent<Text>().text = combatSlotOne.moveTwo.moveName;
            levelInfo.GetSkillThree().GetComponent<Text>().text = combatSlotOne.moveThree.moveName;
            levelInfo.GetSkillFour().GetComponent<Text>().text = "--";
            levelInfo.GetSkillFive().GetComponent<Text>().text = "--";
        }
        else if (combatSlotOne.moveFiveEmpty)
        {
            levelInfo.GetSkillTwo().GetComponent<Text>().text = combatSlotOne.moveTwo.moveName; ;
            levelInfo.GetSkillThree().GetComponent<Text>().text = combatSlotOne.moveThree.moveName;
            levelInfo.GetSkillFour().GetComponent<Text>().text = combatSlotOne.moveFour.moveName;
            levelInfo.GetSkillFive().GetComponent<Text>().text = "--";
        }
    }

    void OnEnable()
    {
        transform.localPosition = cursorLocationPresetOne;
        cursorHover = SkillSlot.One;
        PlayerController.Instance.SkillSelectionAddListener();
        LoadMoveSet();
    }

    void OnDisable()
    {
        if (isApplicationQuitting) return;

        PlayerController.Instance.SkillSelectionRemoveListener();
    }

    void OnApplicationQuit()
    {
        isApplicationQuitting = true;
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
        if (combatSlotOne.moveTwoEmpty && cursorHover == SkillSlot.Two) return;
        if (combatSlotOne.moveThreeEmpty && cursorHover == SkillSlot.Three) return;
        if (combatSlotOne.moveFourEmpty && cursorHover == SkillSlot.Four) return;
        if (combatSlotOne.moveFiveEmpty && cursorHover == SkillSlot.Five) return;


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
