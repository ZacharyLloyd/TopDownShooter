using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSP : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.spawnpoints.Add(gameObject);
    }
}