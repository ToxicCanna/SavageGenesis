using System.Collections.Generic;
using UnityEngine;

public class FossilLand : MonoBehaviour, IDiggingArea
{
    [SerializeField] private Fossil[] lootTable;
    [SerializeField] private float chanceForNothing = 75f;  //chance to fail on getting a loot

    private List<Fossil> possibleLootList = new List<Fossil>();   //Keep track of fossils that can be possibly dug out
    private float lootChance;   //chance to get a specific loot

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
        if (chanceForNothing < Random.Range(0, 100f))
        {
            lootChance = Random.Range(0, GetMaxChanceForFossils());

            foreach (Fossil fossil in lootTable)
            {
                if (lootChance < RarityValue.GetRarityValue(fossil.statObject.rarity))
                    possibleLootList.Add(fossil);
            }
        }
    }

    private float GetMaxChanceForFossils()
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
