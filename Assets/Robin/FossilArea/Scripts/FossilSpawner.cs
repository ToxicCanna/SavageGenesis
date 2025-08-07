using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FossilSpawner : MonoBehaviour
{
    [SerializeField] private int spawnAttempts = 10;
    [SerializeField] private float spawnBoundsOffset = 0.2f;

    private MiningStateMachine miningStateMachine;

    private FossilLayer fossilLayer;
    private Renderer _renderer;
    private Bounds spawnerBound;

    private int fossilIndex = 1;

    private void Awake()
    {
        _renderer = GetComponent<TilemapRenderer>();
        fossilLayer = GetComponent<FossilLayer>();
        spawnerBound = _renderer.bounds;
        miningStateMachine = fossilLayer.miningStateMachine;
    }

    public IEnumerator SpawnFossilFromList(List<Fossil> spawnList)
    {
        //fossilIndex = 0;
        for (int i = 0; i < spawnAttempts; i++)
        {
            if (spawnList.Count > 0)
            {
                //Spawn fossils for initialization
                var fossilGet = spawnList[Random.Range(0, spawnList.Count)];
                var spawnPos = GetRandomSpawnPosition();

                if (fossilGet != null)
                {
                    var spawnedFossil = Instantiate(fossilGet, fossilLayer.grid.LocalToCell(spawnPos), Quaternion.identity);
                    miningStateMachine.fossilSpawnedList.Add(spawnedFossil);
                    spawnedFossil.name = fossilGet.name + "-" + fossilIndex;

                    yield return spawnedFossil.WaitForCollisions();

                    if (!spawnedFossil.isColliding)
                    {
                        fossilIndex++;
                        yield break;
                    }
                    else
                    {
                        //Debug.Log($"{spawnedFossil.gameObject.GetInstanceID()} is hitting something, deleting");
                        miningStateMachine.fossilSpawnedList.Remove(spawnedFossil);
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
