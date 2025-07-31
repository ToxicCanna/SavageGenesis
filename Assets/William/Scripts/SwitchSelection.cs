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

        if (levelInfo.GetPlayerDinoInventory().LoadCombatSlotOne().isFainted)
        {
            levelInfo.GetSwitchOneFaintSprite().SetActive(true);
        }
        else
        {
            levelInfo.GetSwitchOneFaintSprite().SetActive(false);
        }

        if (!levelInfo.GetPlayerDinoInventory().LoadCombatSlotTwo().IsEmpty())
        {
            levelInfo.GetSwitchTwo().GetComponent<Text>().text = levelInfo.GetPlayerDinoInventory().LoadCombatSlotTwo().nickName;
            levelInfo.GetSwitchTwoSprite().GetComponent<Image>().sprite = levelInfo.GetPlayerDinoInventory().LoadCombatSlotTwo().dinoInfoRef.dinosaurInventorySprite;
            if (levelInfo.GetPlayerDinoInventory().LoadCombatSlotTwo().isFainted)
            {
                levelInfo.GetSwitchTwoFaintSprite().SetActive(true);
            }
            else {
                levelInfo.GetSwitchTwoFaintSprite().SetActive(false);
            }
        }
        else {
            levelInfo.GetSwitchTwo().GetComponent<Text>().text = "--";
            levelInfo.GetSwitchTwoSprite().GetComponent<Image>().sprite = emptySprite;
        }

        if (!levelInfo.GetPlayerDinoInventory().LoadCombatSlotThree().IsEmpty())
        {
            levelInfo.GetSwitchThree().GetComponent<Text>().text = levelInfo.GetPlayerDinoInventory().LoadCombatSlotThree().nickName;
            levelInfo.GetSwitchThreeSprite().GetComponent<Image>().sprite = levelInfo.GetPlayerDinoInventory().LoadCombatSlotThree().dinoInfoRef.dinosaurInventorySprite;

            if (levelInfo.GetPlayerDinoInventory().LoadCombatSlotThree().isFainted)
            {
                levelInfo.GetSwitchThreeFaintSprite().SetActive(true);
            }
            else
            {
                levelInfo.GetSwitchThreeFaintSprite().SetActive(false);
            }

        }
        else {
            levelInfo.GetSwitchThree().GetComponent<Text>().text = "--";
            levelInfo.GetSwitchThreeSprite().GetComponent<Image>().sprite = emptySprite;
        }

        if (!levelInfo.GetPlayerDinoInventory().LoadCombatSlotFour().IsEmpty())
        {
            levelInfo.GetSwitchFour().GetComponent<Text>().text = levelInfo.GetPlayerDinoInventory().LoadCombatSlotFour().nickName;
            levelInfo.GetSwitchFourSprite().GetComponent<Image>().sprite = levelInfo.GetPlayerDinoInventory().LoadCombatSlotFour().dinoInfoRef.dinosaurInventorySprite;

            if (levelInfo.GetPlayerDinoInventory().LoadCombatSlotFour().isFainted)
            {
                levelInfo.GetSwitchFourFaintSprite().SetActive(true);
            }
            else
            {
                levelInfo.GetSwitchFourFaintSprite().SetActive(false);
            }
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
        if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVOne)
        {
            if (cursorHover == DinosaurSlot.Two && levelInfo.GetPlayerDinoInventory().LoadCombatSlotTwo().IsEmpty()) return; 
            if (cursorHover == DinosaurSlot.Three && levelInfo.GetPlayerDinoInventory().LoadCombatSlotThree().IsEmpty()) return;
            if (cursorHover == DinosaurSlot.Four && levelInfo.GetPlayerDinoInventory().LoadCombatSlotFour().IsEmpty()) return;


            if (cursorHover == DinosaurSlot.Two)
            {
                GameManager.Instance.switchFrom = DinosaurSlot.One;
                GameManager.Instance.switchTo = DinosaurSlot.Two;
                GameManager.Instance.PlayerFinishedSelection();
            }
            else if (cursorHover == DinosaurSlot.Three)
            {
                GameManager.Instance.switchFrom = DinosaurSlot.One;
                GameManager.Instance.switchTo = DinosaurSlot.Three;
                GameManager.Instance.PlayerFinishedSelection();
            }
            else if (cursorHover == DinosaurSlot.Four)
            {
                GameManager.Instance.switchFrom = DinosaurSlot.One;
                GameManager.Instance.switchTo = DinosaurSlot.Four;
                GameManager.Instance.PlayerFinishedSelection();
            }

        }

    }

    public void PlayerPressedCancel()
    {
        if (GameManager.Instance.goToPlayerFaintedState) return;

        if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVOne)
        {
            levelInfo.GetActionSelector().SetActive(true);
            levelInfo.GetActionSelectorCursor().SetActive(true);

            levelInfo.GetSwitchSelector().SetActive(false);
            levelInfo.GetSwitchSelectorCursor().SetActive(false);
        }

    }
}
