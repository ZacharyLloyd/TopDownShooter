using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField, Range(0f, 100f), Tooltip("Current health")] public int currentHealth;
    [SerializeField, Range(0f, 100f), Tooltip("Current max health")] private int maxHealth;

    [Header("Weapon")]
    public Weapon startingWeapon;
    public Weapon weaponEquipped;
    public Weapon[] inventory = new Weapon[3];

    private void Start()
    {
        currentHealth = maxHealth;
    }
}
