using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ISS : MonoBehaviour
{
    public static ISS Instance;
    public Transform enemysLocation;
    public List<float?> distances = new List<float?>();
    void Awake()
    {
        #region
        //Setup the Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion
    }
    //This function is used to find the location of the enemy so we can take that data and use it to find a spawn point best suited to the slider value
    public static Transform GetEnemyPosition(Transform t)
    {
        foreach (GameObject enemy in GameManager.instance.enemies)
        {
            if (t == enemy.transform)
            {
                return enemy.transform; 
            }
            Instance.enemysLocation = enemy.transform;
        }
        return null;
    }
    //This function will be used to find the distance between the enemy and the spawn points
    List<float?> FindDistances()
    {
        foreach(GameObject spawnpoint in GameManager.instance.spawnpoints)
        {
            float? distance = Vector3.Distance(spawnpoint.transform.position, enemysLocation.position);
            distances.Add(distance);
        }
        distances.Sort();
        return distances;
    }
    //This function is used to find a spawn point that relates to the slider value of how far the player wants to be from the enemy
    //taking the distance found from FindDistance
    void FindSpawnPoint()
    {
        int index = -1;
        if(GameManager.instance.prefSlider != null)
        {
            float? x = distances.Min() + GameManager.instance.prefSlider.value * (distances.Max() - distances.Min());
            index = distances.BinarySearch(x);
        }
        Debug.Log(index);
    }
    //This function is used to actually instantiate the player into the game once everything has been calculated
    void SpawnPlayer()
    {

    }
}
