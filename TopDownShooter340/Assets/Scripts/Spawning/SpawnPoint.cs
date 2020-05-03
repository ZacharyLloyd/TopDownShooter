using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Pawn pawn;
    public List<Pawn> actorsToSpawn;
    public float spawnRate;
    public Transform startPoint;
    public Transform locationToSpawn;
    public int spawnMax;
    public bool spawningFull;

    public IEnumerator spawnCycle;

    private IEnumerator SpawnCycle()
    {
        while (true)
        {
            if (actorsToSpawn.Count < spawnMax)
            {
                SpawnObjects();
            }
            yield return null;
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SpawnObjects()
    {
        Instantiate(pawn.gameObject, transform);
    }
}
