using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    PlayerInput playerI;
    void OnEnable()
    {
        playerI = new PlayerInput();
        if (playerI != null)
        {
            //add in attack here once I am ready.
            playerI.Combat.Up.performed += (val) => PlayerController.Instance.OnUp();
            playerI.Combat.Down.performed += (val) => PlayerController.Instance.OnDown();
            playerI.Combat.Left.performed += (val) => PlayerController.Instance.OnLeft();
            playerI.Combat.Right.performed += (val) => PlayerController.Instance.OnRight();
            playerI.Combat.Confirm.performed += (val) => PlayerController.Instance.OnConfirm();
            playerI.Combat.Cancel.performed += (val) => PlayerController.Instance.OnCancel();
        }
        playerI.Enable();
    }

    private void OnDisable()
    {
        if (playerI != null)
        {
            playerI.Disable();
        }
    }
}