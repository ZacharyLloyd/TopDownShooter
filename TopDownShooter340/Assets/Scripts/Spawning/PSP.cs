using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSP : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.spawnpoints.Add(this.gameObject);
    }
}