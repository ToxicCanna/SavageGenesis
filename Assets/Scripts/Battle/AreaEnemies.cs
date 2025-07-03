using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemies : MonoBehaviour
{
    [SerializeField] public List<Enemy> localEnemies;

    public Enemy GetRandomLocalEnemy()
    {
        var localEnemy = localEnemies[Random.Range(0, localEnemies.Count)];
        localEnemy.Init();
        return localEnemy;
    }
}
