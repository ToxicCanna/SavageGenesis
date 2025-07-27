using UnityEngine;
using UnityEngine.UI;

public class SwitchSelection : MonoBehaviour
{
    [SerializeField] private Vector3 cursorLocationPresetOne;
    [SerializeField] private Vector3 cursorLocationPresetTwo;
    [SerializeField] private Vector3 cursorLocationPresetThree;
    [SerializeField] private Vector3 cursorLocationPresetFour;
    [SerializeField] private Vector3 cursorLocationPresetFive;

    [SerializeField] private LevelInfo levelInfo;

    [SerializeField] private Sprite emptySprite;

    private DinosaurSlot cursorHover;

    private bool isApplicationQuitting = false;

    public void LoadInventoryDinosaurs()
    {
        levelInfo.GetSwitchOne().GetComponent<Text>().text = levelInfo.GetPlayerDinoInventory().LoadCombatSlotOne().nickName;
        levelInfo.GetSwitchOneSprite().GetComponent<Image>().sprite = levelInfo.GetPlayerDinoInventory().LoadCombatSlotOne().dinoInfoRef.dinosaurInventorySprite;
        if (!levelInfo.GetPlayerDinoInventory().LoadCombatSlotTwo().IsEmpty())
        {
            levelInfo.GetSwitchTwo().GetComponent<Text>().text = levelInfo.GetPlayerDinoInventory().LoadCombatSlotTwo().nickName;
            levelInfo.GetSwitchTwoSprite().GetComponent<Image>().sprite = levelInfo.GetPlayerDinoInventory().LoadCombatSlotTwo().dinoInfoRef.dinosaurInventorySprite;
        }
        else {
            levelInfo.GetSwitchTwo().GetComponent<Text>().text = "--";
            levelInfo.GetSwitchTwoSprite().GetComponent<Image>().sprite = emptySprite;
        }

        if (!levelInfo.GetPlayerDinoInventory().LoadCombatSlotThree().IsEmpty())
        {
            levelInfo.GetSwitchThree().GetComponent<Text>().text = levelInfo.GetPlayerDinoInventory().LoadCombatSlotThree().nickName;
            levelInfo.GetSwitchThreeSprite().GetComponent<Image>().sprite = levelInfo.GetPlayerDinoInventory().LoadCombatSlotThree().dinoInfoRef.dinosaurInventorySprite;
        }
        else {
            levelInfo.GetSwitchThree().GetComponent<Text>().text = "--";
            levelInfo.GetSwitchThreeSprite().GetComponent<Image>().sprite = emptySprite;
        }

        if (!levelInfo.GetPlayerDinoInventory().LoadCombatSlotThree().IsEmpty())
        {
            levelInfo.GetSwitchFour().GetComponent<Text>().text = levelInfo.GetPlayerDinoInventory().LoadCombatSlotFour().nickName;
            levelInfo.GetSwitchFourSprite().GetComponent<Image>().sprite = levelInfo.GetPlayerDinoInventory().LoadCombatSlotFour().dinoInfoRef.dinosaurInventorySprite;
        }
        else{
            levelInfo.GetSwitchFour().GetComponent<Text>().text = "--";
            levelInfo.GetSwitchFourSprite().GetComponent<Image>().sprite = emptySprite;
        }
    }

    void OnEnable()
    {
        transform.localPosition = cursorLocationPresetOne;
        cursorHover = DinosaurSlot.One;
        PlayerController.Instance.SwitchSelectionAddListener();
        LoadInventoryDinosaurs();
    }

    void OnDisable()
    {
        if (isApplicationQuitting) return;

        PlayerController.Instance.SwitchSelectionRemoveListener();
    }

    void OnApplicationQuit()
    {
        isApplicationQuitting = true;
    }

    public void PlayerPressedUp()
    {
        if (cursorHover == DinosaurSlot.One || cursorHover == DinosaurSlot.Two)
        {
            cursorHover = DinosaurSlot.Back;
            transform.localPosition = cursorLocationPresetFive;
        }
        else if (cursorHover == DinosaurSlot.Three)
        {
            cursorHover = DinosaurSlot.One;
            transform.localPosition = cursorLocationPresetOne;
        }
        else if (cursorHover == DinosaurSlot.Four)
        {
            cursorHover = DinosaurSlot.Two;
            transform.localPosition = cursorLocationPresetTwo;
        }
        else if (cursorHover == DinosaurSlot.Back)
        {
            cursorHover = DinosaurSlot.Three;
            transform.localPosition = cursorLocationPresetThree;
        }
    }

    public void PlayerPressedDown()
    {
        if (cursorHover == DinosaurSlot.One)
        {
            cursorHover = DinosaurSlot.Three;
            transform.localPosition = cursorLocationPresetThree;
        }
        else if (cursorHover == DinosaurSlot.Two)
        {
            cursorHover = DinosaurSlot.Four;
            transform.localPosition = cursorLocationPresetFour;
        }
        else if (cursorHover == DinosaurSlot.Three || cursorHover == DinosaurSlot.Four)
        {
            cursorHover = DinosaurSlot.Back;
            transform.localPosition = cursorLocationPresetFive;
        }
        else if (cursorHover == DinosaurSlot.Back)
        {
            cursorHover = DinosaurSlot.One;
            transform.localPosition = cursorLocationPresetOne;
        }
    }

    public void PlayerPressedLeft()
    {
           if (cursorHover == DinosaurSlot.One)
           {
               cursorHover = DinosaurSlot.Two;
               transform.localPosition = cursorLocationPresetTwo;
           }
           else if (cursorHover == DinosaurSlot.Two)
           {
               cursorHover = DinosaurSlot.One;
               transform.localPosition = cursorLocationPresetOne;
           }
           else if (cursorHover == DinosaurSlot.Three)
           {
               cursorHover = DinosaurSlot.Four;
               transform.localPosition = cursorLocationPresetFour;
           }
           else if (cursorHover == DinosaurSlot.Four)
           {
               cursorHover = DinosaurSlot.Three;
               transform.localPosition = cursorLocationPresetThree;
           }
    }

    public void PlayerPressedRight()
    {
          if (cursorHover == DinosaurSlot.One)
          {
              cursorHover = DinosaurSlot.Two;
              transform.localPosition = cursorLocationPresetTwo;
          }
          else if (cursorHover == DinosaurSlot.Two)
          {
              cursorHover = DinosaurSlot.One;
              transform.localPosition = cursorLocationPresetOne;
          }
          else if (cursorHover == DinosaurSlot.Three)
          {
              cursorHover = DinosaurSlot.Four;
              transform.localPosition = cursorLocationPresetFour;
          }
          else if (cursorHover == DinosaurSlot.Four)
          {
              cursorHover = DinosaurSlot.Three;
              transform.localPosition = cursorLocationPresetThree;
          }
    }

    public void PlayerPressedConfirm()
    {
        /*if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVOne)
        {
            if (combatSlotOne.moveTwoEmpty && cursorHover == SkillSlot.Two) return;
            if (combatSlotOne.moveThreeEmpty && cursorHover == SkillSlot.Three) return;
            if (combatSlotOne.moveFourEmpty && cursorHover == SkillSlot.Four) return;
            if (combatSlotOne.moveFiveEmpty && cursorHover == SkillSlot.Five) return;

            if (cursorHover == SkillSlot.One)
            {
                GameManager.Instance.playerChoice_MoveInfo = combatSlotOne.moveOne;
                GameManager.Instance.PlayerFinishedSelection();
            }
            else if (cursorHover == SkillSlot.Two)
            {
                GameManager.Instance.playerChoice_MoveInfo = combatSlotOne.moveTwo;
                GameManager.Instance.PlayerFinishedSelection();
            }
            else if (cursorHover == SkillSlot.Three)
            {
                GameManager.Instance.playerChoice_MoveInfo = combatSlotOne.moveThree;
                GameManager.Instance.PlayerFinishedSelection();
            }
            else if (cursorHover == SkillSlot.Four)
            {
                GameManager.Instance.playerChoice_MoveInfo = combatSlotOne.moveFour;
                GameManager.Instance.PlayerFinishedSelection();
            }
            else if (cursorHover == SkillSlot.Five)
            {
                GameManager.Instance.playerChoice_MoveInfo = combatSlotOne.moveFive;
                GameManager.Instance.PlayerFinishedSelection();
            }
        }
        */



    }

    public void PlayerPressedCancel()
    {
        if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVOne)
        {
            levelInfo.GetActionSelector().SetActive(true);
            levelInfo.GetActionSelectorCursor().SetActive(true);

            levelInfo.GetSwitchSelector().SetActive(false);
            levelInfo.GetSwitchSelectorCursor().SetActive(false);
        }

    }
}
