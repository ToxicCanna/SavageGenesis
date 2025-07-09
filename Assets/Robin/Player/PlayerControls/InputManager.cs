using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    public SavageGenesis_PlayerInput playerInputs;

    #region Instance Setup
    protected override void Awake()
    {
        base.Awake();
        playerInputs = new SavageGenesis_PlayerInput();
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }
    #endregion

    #region Player Actions
    public Vector2 GetPlayerMovement()
    {
        return playerInputs.Player.Movement.ReadValue<Vector2>();
    }

    public bool GetInteraction()
    {
        return playerInputs.Player.Interaction.triggered;
    }
    #endregion

}
