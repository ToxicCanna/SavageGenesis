using UnityEngine;

public class DiggingLayer : BaseMiningLayer, IDiggingArea
{
    public float stability = 10f;    
    public void OnDigging(Vector2 diggingPos, DiggingToolType tool)
    {
        if (tilemap != null)
        {
            tilemap.SetTile(grid.LocalToCell(diggingPos), null);
            switch (tool)
            {
                case 0:
                    stability -= DiggingToolStrength.diggingStrength[0];
                    break;
                case (DiggingToolType)1:
                    stability -= DiggingToolStrength.diggingStrength[1];
                    break;
                default:
                    stability -= 1f;
                    break;
            }

            Debug.Log($"Current stability: {stability}");
        }
        else
            Debug.Log("Tilemap is not found!");
    }
}
