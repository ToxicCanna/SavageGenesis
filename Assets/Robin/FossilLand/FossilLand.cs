using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FossilLand : MonoBehaviour, IDiggingArea
{
    [SerializeField] private FossilLoot[] lootTable;
    //[SerializeField] private float chanceForNothing = 75f;  //chance to fail on getting a loot
    [SerializeField] private int minFossils = 1;
    [SerializeField] private int maxFossils = 5;

    private List<Fossil> _possibleLootList = new List<Fossil>();   //Keep track of fossils that can be possibly dug out
    private float _lootChance;   //chance to get a specific loot

    private void Start()
    {
        RandomSpawnFossilFromList();
    }

    private void RandomSpawnFossilFromList()
    {
        SetPossibleFossilList();

        if (_possibleLootList.Count > 0 && _possibleLootList[0] != null)
        {
            //Spawn fossils for initialization
            for (int i = 0; i < UnityEngine.Random.Range(minFossils, maxFossils + 1); i++)
            {
                var spawnObject = _possibleLootList[UnityEngine.Random.Range(0, _possibleLootList.Count)];
                Debug.Log($"{spawnObject} is spawned!");
            }
        }
        _possibleLootList.Clear();
    }

    #region Digging
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

    /*private Fossil GetDigOutFossil()
    {
        if (_possibleLootList.Count > 0)
            return _possibleLootList[UnityEngine.Random.Range(0, _possibleLootList.Count)];
        else return null;
    }*/
    #endregion

    #region Setup Possible Loot List
    private void SetPossibleFossilList()
    {
        foreach (var loot in lootTable)
        {
            for(int i = 0; i < loot.maxFossilNumber; i++)
            {
                if (loot.fossil.statObject.rarity == GetRarityLevelRandomly())
                    _possibleLootList.Add(loot.fossil);
            }
        }
    }

    private RarityLevel GetRarityLevelRandomly()
    {
        _lootChance = UnityEngine.Random.Range(0, 100);

        float cumulative = 0f;

        for (int i = 0; i < RarityValue.rarityValues.Length; i++)
        {
            cumulative += RarityValue.rarityValues[i];
            if (_lootChance < cumulative)
            {
                return (RarityLevel)i;
            }

        }

        return 0;
    }
    #endregion
}
