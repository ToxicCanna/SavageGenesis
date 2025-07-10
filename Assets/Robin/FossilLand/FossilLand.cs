using System;
using System.Collections.Generic;
using UnityEngine;

public class FossilLand : MonoBehaviour, IDiggingArea
{
    [SerializeField] private FossilLoot[] lootTable;
    //[SerializeField] private float chanceForNothing = 75f;  //chance to fail on getting a loot
    [SerializeField] private int minFossils = 1;
    [SerializeField] private int maxFossils = 5;

    private List<Fossil> possibleLootList = new List<Fossil>();   //Keep track of fossils that can be possibly dug out
    private float _lootChance;   //chance to get a specific loot

    private void Start()
    {
        RandomSpawnFossilFromList();
    }

    public void OnDigging(DiggingToolType tool)
    {
        
    }

    public void FinishDigging()
    {
        /*var fossilGet = GetDigOutFossil();

        if (fossilGet != null)
        {
            fossilGet.GetFossil();
        }
        else
            Debug.Log("Nothing there...");*/
    }

    private Fossil GetDigOutFossil()
    {
        if (possibleLootList.Count > 0)
            return possibleLootList[UnityEngine.Random.Range(0, possibleLootList.Count)];
        else return null;
    }

    private void RandomSpawnFossilFromList()
    {
        SetFossilList();

        //Spawn fossils for initialization
        for (int i = 0; i < UnityEngine.Random.Range(minFossils, maxFossils); i++)
        {
            var spawnedFossil = GetDigOutFossil();
            if (spawnedFossil != null)
            {
                Debug.Log($"Spawn {spawnedFossil}");    //spawn logic will be applied there
                possibleLootList.Remove(spawnedFossil);
            }
            else break;
        }
        possibleLootList.Clear();
    }

    private void SetFossilList()
    {
        _lootChance = UnityEngine.Random.Range(0, GetMaxChanceForFossils());

        foreach (FossilLoot fossilLoot in lootTable)
        {
            if (_lootChance < RarityValue.GetRarityValue(fossilLoot.fossil.statObject.rarity))
            {
                for (int i = 0; i < fossilLoot.maxFossilNumber; i++)
                    possibleLootList.Add(fossilLoot.fossil);
            }
                
        }
    }

    private float GetMaxChanceForFossils()
    {
        var currentMaxChance = 0f;
        
        foreach (FossilLoot fossilLoot in lootTable)
        {
            if (currentMaxChance < RarityValue.GetRarityValue(fossilLoot.fossil.statObject.rarity))
                currentMaxChance = RarityValue.GetRarityValue(fossilLoot.fossil.statObject.rarity);
        }

        return currentMaxChance;
    }
}
