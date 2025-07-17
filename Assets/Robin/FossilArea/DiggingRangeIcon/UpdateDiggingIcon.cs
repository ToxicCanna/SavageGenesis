using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UpdateDiggingIcon : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private MiningStateMachine miningStateMachine;

    private InputManager inputManager;
    private Vector2 mousePosition;
    private Tilemap tilemap;
    private BoundsInt bounds;

    [NonSerialized] public CircleCollider2D iconCollider;

    private void Start()
    {
        inputManager = InputManager.Instance;
        tilemap = miningStateMachine.diggingLayer.GetComponent<Tilemap>();
        bounds = tilemap.cellBounds;
    }

    #region Update Digging Icon
    private void Update()
    {
        UpdateIcon();
        UpdateObjectPosition();
    }

    private void UpdateObjectPosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(inputManager.GetInteractPosition());
        Vector2 newPosition = new Vector2
        (
            Mathf.Clamp(mousePosition.x, GetTilemapMinPos().x, GetTilemapMaxPos().x - 1f),
            Mathf.Clamp(mousePosition.y, GetTilemapMinPos().y, GetTilemapMaxPos().y - 1f)
        );

        transform.position = grid.LocalToCell(newPosition);
    }

    private void UpdateIcon()
    {
        foreach(var child in GetChildrenObjects.GetAllChildren(gameObject))
            child.SetActive(false);

        switch (miningStateMachine.playerDigging.currentDiggingTool)
        {
            case 0: //Brush
                transform.Find("BrushRange")?.gameObject.SetActive(true);
                iconCollider = transform.Find("BrushRange")?.gameObject.GetComponent<CircleCollider2D>();
                break;
            case (DiggingToolType)1: //Pickaxe
                transform.Find("PickaxeRange")?.gameObject.SetActive(true);
                iconCollider = transform.Find("PickaxeRange")?.gameObject.GetComponent<CircleCollider2D>();
                break;
            default:
                Debug.Log("Tool enum is not set!");
                break;
        }
    }
    #endregion

    private Vector3 GetTilemapMinPos()
    {
        return tilemap.CellToWorld(bounds.min);
    }

    private Vector3 GetTilemapMaxPos()
    {
        return tilemap.CellToWorld(bounds.max);
    }
}
