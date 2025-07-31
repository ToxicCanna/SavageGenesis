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

    [NonSerialized] public GameObject currentToolRange;

    private void Start()
    {
        inputManager = InputManager.Instance;
        tilemap = miningStateMachine.diggingLayer.GetComponent<Tilemap>();
        bounds = tilemap.cellBounds;
        currentToolRange = GetChildrenObjects.GetAllChildren(gameObject)[0];

        foreach (var child in GetChildrenObjects.GetAllChildren(gameObject))
            child.SetActive(false);
    }

    #region Update Digging Icon
    private void Update()
    {
        UpdateIcon();
        HandleIconOutOfRange();
        UpdateObjectPosition();
    }

    private void HandleIconOutOfRange()
    {
        if (
            mousePosition.x < GetTilemapMinPos().x 
            || mousePosition.x >= GetTilemapMaxPos().x 
            || mousePosition.y < GetTilemapMinPos().y 
            || mousePosition.y >= GetTilemapMaxPos().y
        )
            currentToolRange.SetActive(false);
    }

    private void UpdateObjectPosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(inputManager.GetInteractPosition());

        transform.position = grid.LocalToCell(mousePosition);
    }

    private void UpdateIcon()
    {
        currentToolRange.SetActive(false);

        var newToolRange = transform.Find(miningStateMachine.playerDigging.currentDiggingTool.ToString() + "Range")?.gameObject;

        if(newToolRange)
        {
            newToolRange.SetActive(true);
            currentToolRange = newToolRange;
        }
        else
        {
            Debug.Log("Tool enum is not found!");
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
