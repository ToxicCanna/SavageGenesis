using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FossilSpawner : MonoBehaviour
{
    [SerializeField] private int spawnAttempts = 10;
    [SerializeField] private string fossilMeshName = "SpriteMesh";
    [SerializeField] private float spawnBoundsOffset = 0.2f;

    private Renderer _renderer;
    private Bounds spawnerBound;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        spawnerBound = _renderer.bounds;
    }

    public IEnumerator SpawnFossilFromList(List<Fossil> spawnList)
    {
        for (int i = 0; i < spawnAttempts; i++)
        {
            if (spawnList.Count > 0)
            {
                //Spawn fossils for initialization
                var fossilGet = spawnList[Random.Range(0, spawnList.Count)];
                var spawnPos = GetRandomSpawnPosition();

                if (fossilGet != null)
                {
                    var spawnedFossil = Instantiate(spawnList[Random.Range(0, spawnList.Count)], spawnPos, Quaternion.identity);

                    yield return spawnedFossil.WaitForCollisions();

                    if (!spawnedFossil.isColliding)
                        yield break;
                    else
                    {
                        //Debug.Log($"{spawnedFossil.gameObject.GetInstanceID()} is hitting something, deleting");
                        Destroy(spawnedFossil.gameObject);
                    }
                }
            }
        }

        //Debug.Log("Spawning failed");
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float randomXPos = Random.Range(spawnerBound.min.x + spawnBoundsOffset, spawnerBound.max.x - spawnBoundsOffset);
        float randomYPos = Random.Range(spawnerBound.min.y+spawnBoundsOffset, spawnerBound.max.y-spawnBoundsOffset);

        return new Vector2(randomXPos, randomYPos);
    }
}
