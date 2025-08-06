using UnityEngine;

public class SecondDiggingLayer : DiggingLayer, IDiggingArea
{
    [SerializeField] private int maxRemovedGridValue = 60;
    [SerializeField] private float generateChance = 30f;

    private void OnEnable()
    {
        int generatedGrid = 0;

        for (int x = 0; x < tilemap.size.x; x++)
        {
            for (int y = 0; y < tilemap.size.y; y++)
            {
                if (generatedGrid >= maxRemovedGridValue)
                    break;

                float randomChance = Random.Range(1f, 100f);
                if (randomChance <= generateChance)
                {
                    tilemap.SetTile(new Vector3Int(x + tilemap.origin.x, y + tilemap.origin.y), null);
                    generatedGrid++;
                }
            }
        }
    }

}
