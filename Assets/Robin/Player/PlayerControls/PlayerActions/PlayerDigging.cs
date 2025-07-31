using System.Linq;
using UnityEngine;

//For now just testing how digging function work
public class PlayerDigging : BasePlayerController
{
    public DiggingToolType currentDiggingTool;
    [SerializeField] private float diggingDelay = 0.5f;

    private IDiggingArea iDiggingArea;
    private bool _isDigging = false;

    public void Dig(GameObject diggingRange)
    {
        if (inputManager.GetInteractInput())
        {
            if (!_isDigging)
                Digging(diggingRange);

            Invoke(nameof(ResetDigging), diggingDelay);
        }  
    }

    private void Digging(GameObject diggingRange)
    {
        _isDigging = true;

        //Debug.Log(hit.collider.gameObject.name);
        var diggingSpriteList = GetChildrenObjects.GetAllChildren(diggingRange);

        foreach(var diggingSprite in diggingSpriteList)
        {
            var spriteCollider = diggingSprite.GetComponent<BoxCollider2D>();
            var hits = Physics2D.OverlapBoxAll(diggingSprite.transform.position, spriteCollider.size, 0f);
            bool isDugOut = false;

            if (hits.Length <= 0) break;

            foreach (var hit in hits.Reverse())
            {
                iDiggingArea = hit.gameObject.GetComponentInChildren<IDiggingArea>();
                if (iDiggingArea != null)
                    iDiggingArea.OnDigging(diggingSprite.GetComponent<BoxCollider2D>(), currentDiggingTool, out isDugOut);

                if (isDugOut)
                    break;
            }

        }

        if (iDiggingArea != null)
            iDiggingArea.UpdateDurability(currentDiggingTool);
    }


    private void ResetDigging()
    {
        _isDigging = false;
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
