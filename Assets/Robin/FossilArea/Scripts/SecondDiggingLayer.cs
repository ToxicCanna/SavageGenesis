using UnityEngine;

public class SecondDiggingLayer : DiggingLayer, IDiggingArea
{
    public override void OnDigging(BoxCollider2D collision, DiggingToolType tool, out bool isDugOut)
    {
        base.OnDigging(collision, tool, out isDugOut);
        Debug.Log("Second Layer");
    }
}
