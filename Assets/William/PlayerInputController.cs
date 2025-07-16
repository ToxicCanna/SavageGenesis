using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    void OnEnable()
    {
        PlayerInput playerI = new PlayerInput();
        if (playerI != null)
        {
            //add in attack here once I am ready.
            playerI.Combat.Up.performed += (val) => PlayerController.Instance.OnJump();
            playerI.Combat.Down.performed += (val) => PlayerController.Instance.JumpReleased();
            playerI.Combat.Left.performed += (val) => PlayerController.Instance.JumpReleased();
        }
        playerI.Enable();
    }
}