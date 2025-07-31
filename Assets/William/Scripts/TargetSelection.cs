using UnityEngine;

public class TargetSelection : MonoBehaviour
{
    [SerializeField] private Vector3 cursorLocationPresetOne;
    [SerializeField] private Vector3 cursorLocationPresetTwo;
    [SerializeField] private Vector3 cursorLocationPresetThree;
    [SerializeField] private Vector3 cursorLocationPresetFour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private LevelInfo levelInfo;

    private CombatActors cursorHover;

    private bool isApplicationQuitting = false;

    private bool playerOneMadeChoice;

    void OnEnable()
    {
        transform.localPosition = cursorLocationPresetOne;
        cursorHover = CombatActors.EnemySlotOne;
        PlayerController.Instance.SwitchSelectionAddListener();
       

    }

    public void PlayerPressedLeft()
    {
        if (cursorHover == CombatActors.EnemySlotOne)
        {
            if (playerOneMadeChoice)
            {
                cursorHover = CombatActors.PlayerSlotOne;
            transform.localPosition = cursorLocationPresetThree;
            }
            else
            { 
                
            }

        }
        else if (cursorHover == CombatActors.EnemySlotTwo)
        {
            cursorHover = CombatActors.PlayerSlotOne;
            transform.localPosition = cursorLocationPresetOne;
        }
    }

    public void PlayerPressedRight()
    {
        /*if (cursorHover == DinosaurSlot.One)
        {
            cursorHover = DinosaurSlot.Two;
            transform.localPosition = cursorLocationPresetTwo;
        }
        else if (cursorHover == DinosaurSlot.Two)
        {
            cursorHover = DinosaurSlot.One;
            transform.localPosition = cursorLocationPresetOne;
        }*/
    }

    public void PlayerPressedConfirm()
    {
       /* if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVTwo)
        {
            if (cursorHover == CombatActors.EnemySlotOne)
            {
                if (playerOneMadeChoice)
                {
                    GameManager.Instance.playerChoice_MoveTwoTarget = CombatActors.EnemySlotOne;
                } else
                { 
                    GameManager.Instance.playerChoice_MoveOneTarget = CombatActors.EnemySlotOne;
                }


            }
            else if (cursorHover == DinosaurSlot.Two)
            { 

            }
        }
        else if (GameManager.Instance.GetCurrentGameMode() == GameMode.TwoVTwo)
        {
            
        }*/

    }

    public void PlayerPressedCancel()
    {
        /*if (GameManager.Instance.goToPlayerFaintedState) return;

        if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVTwo)
        {
            
        }
        else if (GameManager.Instance.GetCurrentGameMode() == GameMode.TwoVTwo)
        {
            
        }*/

    }

}
