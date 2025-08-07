using System;
using System.Linq;
using UnityEngine;

public class PlayerDigging : BasePlayerController
{
    public DiggingToolType currentDiggingTool;
    [SerializeField] private float diggingDelay = 0.5f;
    [SerializeField] private MiningStateMachine miningStateMachine;

    public static float durability = 0f;
    public static bool isDigging = false;

    [SerializeField] private DiggingLayer[] layers;
    public DiggingLayer[] Layers => layers;

    public void Dig(GameObject diggingRange)
    {
        if (diggingRange.GetComponentInParent<UpdateDiggingIcon>().isOutOfRange) return;

        if (inputManager.GetInteractInput())
        {
            if (!isDigging)
                HandleDigging(diggingRange);

            Invoke(nameof(ResetDigging), diggingDelay);
        }  
    }

    private void HandleDigging(GameObject diggingRange)
    {
        isDigging = true;

        //Debug.Log(hit.collider.gameObject.name);
        var diggingSpriteList = GetChildrenObjects.GetAllChildren(diggingRange);

        foreach(var diggingSprite in diggingSpriteList)
        {
            foreach (var layer in layers)
            {
                layer.DigTile(diggingSprite.transform.position.x, diggingSprite.transform.position.y, currentDiggingTool, out var isDugOut);
                if (isDugOut) break;
            }
        }
        UpdateDurability();
    }

    private void UpdateDurability()
    {
        switch (currentDiggingTool)
        {
            case 0:
                durability -= DiggingToolStrength.diggingStrength[0];
                break;
            case (DiggingToolType)1:
                durability -= DiggingToolStrength.diggingStrength[1];
                break;
            default:
                durability -= 1f;
                break;
        }

        //Debug.Log($"Current stability: {durability}");

        if (miningStateMachine.uiManager != null)
        {
            miningStateMachine.uiManager.SetDurability(durability);
            //Debug.Log($"[Digging] Stability now {stability}");
        }
    }

    private void ResetDigging()
    {
        isDigging = false;
    }

    #region Switch Tool
    
    public void SwitchToBrush()
    {
        currentDiggingTool = DiggingToolType.Brush;
    }

    public void SwitchToPickaxe()
    {
        currentDiggingTool = DiggingToolType.Pickaxe;
    }
    #endregion
}
