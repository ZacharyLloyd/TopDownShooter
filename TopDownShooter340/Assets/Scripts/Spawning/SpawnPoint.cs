using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Stats pawn;
    public List<GameObject> actorsToSpawn;
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
        time += Time.deltaTime;
        if (time >= spawnRate)
            Debug.Log("HI FLAGGSSS!");

    }

    bool CheckIfMax(out bool _maxVar)
    {
        Debug.Log(actorsToSpawn.Count);
        _maxVar = (actorsToSpawn.Count >= spawnMax);
        ManageList();
        return _maxVar;
    }
    public void SpawnObjects()
    {
        GameObject newPawn = Instantiate(pawn.gameObject, locationToSpawn);
        actorsToSpawn.Add(newPawn);
    }

    /// <summary>
    /// Manage the list, and change size during run time.
    /// </summary>
    public void ManageList()
    {
        if(actorsToSpawn.Count != 0)
        {
            //We want to find all objects that are null, and remove them;
            List<GameObject> objectsToRemove = new List<GameObject>();

            //First, analyze the list of actors, and add it to what needs to be cleared out
            foreach(GameObject obj in actorsToSpawn)
            {
                if (obj == null)
                {
                    actorsToSpawn.Remove(obj);
                    Debug.Log("A null object has been found...");
                }
            }
        }
    }

    private IEnumerator SpawnCycle()
    {
        while (true)
        {
            if (!CheckIfMax(out spawningFull) && time >= spawnRate)
            {
                SpawnObjects();
                time = RESET;
            }


            yield return null;
        }
    }

}
