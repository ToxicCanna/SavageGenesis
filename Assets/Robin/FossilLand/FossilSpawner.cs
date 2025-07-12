using System.Collections.Generic;
using UnityEngine;

public class FossilSpawner : MonoBehaviour
{
    [SerializeField] private int spawnAttempts = 10;

    private Renderer _renderer;
    private Bounds spawnerBound;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        spawnerBound = _renderer.bounds;
    }

    public void SpawnFossilFromList(List<Fossil> spawnList)
    {
        for (int i = 0; i < spawnAttempts; i++)
        {
            if (spawnList.Count > 0)
            {
                //Spawn fossils for initialization
                var spawnObject = spawnList[Random.Range(0, spawnList.Count)];
                var spawnPos = GetRandomSpawnPosition();

                if (spawnObject != null)
                {
                    if (FindCollisions(spawnPos, spawnObject).Length <= 0)
                    {
                        spawnObject = Instantiate(spawnList[Random.Range(0, spawnList.Count)], spawnPos, Quaternion.identity);
                        Debug.Log($"{spawnObject} is spawned!");
                        return;
                    }
                }
            }
        }
        Debug.Log("Failed while attempt to spawn a fossil!");
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float randomXPos = Random.Range(spawnerBound.min.x, spawnerBound.max.x);
        float randomYPos = Random.Range(spawnerBound.min.y, spawnerBound.max.y);

        return new Vector2(randomXPos, randomYPos);
    }

    private Collider2D[] FindCollisions(Vector2 pos, Fossil fossil)
    {   
        return Physics2D.OverlapBoxAll(pos, fossil.transform.localScale, 0f);
    }
}
