using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField, Range(0f, 100f), Tooltip("Current health")] public int currentHealth;
    [SerializeField, Range(0f, 100f), Tooltip("Current max health")] private int maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
}
