using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

[RequireComponent(typeof(FossilSpawner))]
public class FossilLayer : BaseMiningLayer
{
    #region Input Variables
    [SerializeField] private FossilLoot[] lootTable;
    [SerializeField] private int maxFossils = 5;

    private FossilSpawner _fossilSpawner;
    private List<Fossil> _possibleLootList = new List<Fossil>();   //Keep track of fossils that can be possibly dug out
    private float _lootChance;   //chance to get a specific loot

    [NonSerialized] public bool isInitializationComplete = false;
    #endregion

    protected override void Start()    //Initialization
    {
        base.Start();       
        _fossilSpawner = GetComponent<FossilSpawner>();

        StartCoroutine(InitializeLayer());
    }

    private IEnumerator InitializeLayer()
    {
        for (int i = 0; i < maxFossils; i++)
        {
            SetPossibleFossilList();

            yield return StartCoroutine(_fossilSpawner.SpawnFossilFromList(_possibleLootList));

            _possibleLootList.Clear();
        }

        isInitializationComplete = true;
        yield break;
    }

    #region Setup Possible Loot List
    private void SetPossibleFossilList()
    {
        foreach (var loot in lootTable)
        {
            if (loot.fossil.statObject.rarity == GetSpawnableRarityLevel())
            {
                for (int i = 0; i < loot.lootNumber; i++)
                    _possibleLootList.Add(loot.fossil);
            }
        }
    }

    private RarityLevel GetSpawnableRarityLevel()
    {
        _lootChance = UnityEngine.Random.Range(0, RarityValue.rarityValues.Sum());

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
