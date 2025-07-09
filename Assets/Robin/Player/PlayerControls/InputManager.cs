using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public SavageGenesis_PlayerInput playerInputs;

    private static InputManager _instance;

    #region Instance Setup
    public static InputManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
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
