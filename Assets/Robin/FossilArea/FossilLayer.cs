using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(FossilSpawner))]
public class FossilLayer : MonoBehaviour
{
    #region Input Variables
    [SerializeField] private FossilLoot[] lootTable;
    [SerializeField] private int maxFossils = 5;

    private FossilSpawner _fossilSpawner;
    private List<Fossil> _possibleLootList = new List<Fossil>();   //Keep track of fossils that can be possibly dug out
    private float _lootChance;   //chance to get a specific loot
    #endregion

    private void Start()    //Initialization
    {
        _fossilSpawner = GetComponent<FossilSpawner>();

        for (int i = 0; i < maxFossils; i++)
        {
            SetPossibleFossilList();
            _fossilSpawner.SpawnFossilFromList(_possibleLootList);
            _possibleLootList.Clear();
        }
    }

    #region Setup Possible Loot List
    private void SetPossibleFossilList()
    {
        foreach (var loot in lootTable)
        {
            if (loot.fossil.statObject.rarity == GetSpawnableRarityLevel())
            {
                for (int i = 0; i < loot.maxFossilNumber; i++)
                    _possibleLootList.Add(loot.fossil);
            }
        }
    }

    private RarityLevel GetSpawnableRarityLevel()
    {
        _lootChance = Random.Range(0, RarityValue.rarityValues.Sum());

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
