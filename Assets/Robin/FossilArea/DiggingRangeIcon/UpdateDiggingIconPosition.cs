using UnityEngine;

public class UpdateDiggingIconPosition : MonoBehaviour
{
    [SerializeField] private Grid grid;

    private InputManager inputManager;
    private Vector2 mousePosition;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(inputManager.GetInteractPosition());
        transform.position = grid.LocalToCell(mousePosition);
    }
}
