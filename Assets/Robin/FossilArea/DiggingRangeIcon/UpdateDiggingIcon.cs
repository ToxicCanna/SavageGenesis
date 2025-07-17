using UnityEngine;
using UnityEngine.Tilemaps;

public class UpdateDiggingIcon : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private DiggingLayer diggingLayer;

    private InputManager inputManager;
    private Vector2 mousePosition;
    private Tilemap tilemap;
    private BoundsInt bounds;

    private void Start()
    {
        inputManager = InputManager.Instance;
        tilemap = diggingLayer.GetComponent<Tilemap>();
        bounds = tilemap.cellBounds;
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(inputManager.GetInteractPosition());
        Vector2 newPosition = new Vector2
        (
            Mathf.Clamp(mousePosition.x, GetTilemapMinPos().x, GetTilemapMaxPos().x - 1f),
            Mathf.Clamp(mousePosition.y, GetTilemapMinPos().y, GetTilemapMaxPos().y - 1f)
        );

        transform.position = grid.LocalToCell(newPosition);
    }

    private Vector3 GetTilemapMinPos()
    {
        return tilemap.CellToWorld(bounds.min);
    }

    private Vector3 GetTilemapMaxPos()
    {
        return tilemap.CellToWorld(bounds.max);
    }
}
