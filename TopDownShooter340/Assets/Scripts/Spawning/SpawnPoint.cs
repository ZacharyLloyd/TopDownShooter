using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Stats stats;
    public List<Pawn> actorsSpawned;
    public float spawnRate;
    public Transform startPoint;
    public Transform locationToSpawn;
    public int spawnMax;
    public bool spawningFull;

    public float time;

    public const int RESET = 0;

    public IEnumerator spawnCycle;



    // Start is called before the first frame update
    void Start()
    {
        //We get our start point
        startPoint = transform;

        //The spawning location by default wil be the start point
        locationToSpawn = startPoint;

        spawnCycle = SpawnCycle();
        StartCoroutine(spawnCycle);
    }

    private void Update()
    {
        if (!spawningFull)
        {
            time += Time.deltaTime;
        }
    }

    bool CheckIfMax()
    {
        if (actorsSpawned != null)
        {
            bool max;
            //Debug.Log(actorsToSpawn.Count);
            max = (actorsSpawned.Count >= spawnMax);
            ManageList();
            return max;
        }
        return false;
    }
    public void SpawnObjects()
    {
        Pawn newPawn = Instantiate(stats.gameObject, locationToSpawn).GetComponentInChildren<Pawn>();
        actorsSpawned.Add(newPawn);
    }

    /// <summary>
    /// Manage the list, and change size during run time.
    /// </summary>
    public void ManageList()
    {
        if (actorsSpawned.Count != 0)
        {
            //Exception handling to ignore null object enumeration
            try
            {
                //First, analyze the list of actors, and add it to what needs to be cleared out
                foreach (Pawn obj in actorsSpawned)
                {
                    if (obj == null)
                    {
                        actorsSpawned.Remove(obj);
                        //Debug.Log("A null object has been found...");
                    }
                }
            }
            catch {/*Do nothing*/}

            if (actorsSpawned.Count != spawnMax)
            {
                spawningFull = false;
            }

        }
    }

    private IEnumerator SpawnCycle()
    {
        while (true)
        {
            spawningFull = CheckIfMax();
            if (!spawningFull && time >= spawnRate)
            {
                SpawnObjects();
                time = RESET;
            }
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
    }

}
