using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(FossilSpawner))]
public class FossilLayer : BaseMiningLayer
{
    #region Input Variables
    [SerializeField] private Fossil[] lootTable;
    [SerializeField] private int maxFossils = 5;

    private FossilSpawner _fossilSpawner;
    private List<Fossil> _possibleLootList = new List<Fossil>();   //Keep track of fossils that can be possibly dug out

    [NonSerialized] public bool isInitializationComplete = false;
    #endregion

    private void Start()
    {
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
            _possibleLootList.Add(loot);
        }
    }

    /*private RarityLevel GetSpawnableRarityLevel()
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
    }*/
    #endregion
}
