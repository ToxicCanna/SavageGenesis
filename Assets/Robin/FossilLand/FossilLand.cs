using System.Collections.Generic;
using UnityEngine;

public class FossilLand : MonoBehaviour, IDiggingArea
{
    [SerializeField] private Fossil[] lootTable;

    private List<Fossil> possibleLootList = new List<Fossil>();   //Keep track of fossils that can be possibly dug out
    private float diggingChance;

    public void OnDigging(DiggingToolType tool)
    {
        HandleDigging();
    }

    public void FinishDigging()
    {
        var fossilGet = GetDigOutFossil();

        if (fossilGet != null)
        {
            Debug.Log($"Hey, I found a {fossilGet}!");
        }
        else
            Debug.Log("Nothing there...");

        possibleLootList.Clear();
    }

    private Fossil GetDigOutFossil()
    {
        if (possibleLootList.Count > 0)
            return possibleLootList[Random.Range(0, possibleLootList.Count)];
        else return null;
    }

    private void HandleDigging()
    {
        diggingChance = Random.Range(0, GetMaxChance());

        foreach (Fossil fossil in lootTable)
        {
            if (diggingChance < RarityValue.GetRarityValue(fossil.statObject.rarity))
                possibleLootList.Add(fossil);
        }

        possibleLootList.Add(null);
    }

    private float GetMaxChance()
    {
        var currentMaxChance = 0f;
        
        foreach (Fossil fossil in lootTable)
        {
            if (currentMaxChance < RarityValue.GetRarityValue(fossil.statObject.rarity))
                currentMaxChance = RarityValue.GetRarityValue(fossil.statObject.rarity);
        }

        return currentMaxChance;
    }
}
