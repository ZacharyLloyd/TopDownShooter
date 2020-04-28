using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stats : MonoBehaviour
{
    public PlayerPawn pawn;

    [Header("Health")]
    [SerializeField, Range(0f, 100f), Tooltip("Current health")] public int currentHealth;
    [SerializeField, Range(0f, 100f), Tooltip("Current max health")] private int maxHealth;
    public Image healthFill;

    [Header("AmmoDisplay")]
    public TMPro.TextMeshProUGUI currentAmmoText;
    public TMPro.TextMeshProUGUI maxAmmoText;

    [Header("Weapon")]
    public Weapon startingWeapon;
    public Weapon weaponEquipped;
    public Weapon[] inventory = new Weapon[3];

    [Header("Pistol")]
    public int pistolAmmoMax;
    public int pistolAmmoCurrent;

    [Header("SMG")]
    public int smgAmmoMax;
    public int smgAmmoCurrent;

    [Header("Rifle")]
    public int rifleAmmoMax;
    public int rifleAmmoCurrent;

    private void Awake()
    {
        pawn = GetComponent<PlayerPawn>();
        currentHealth = maxHealth;
        pistolAmmoCurrent = pistolAmmoMax;
        smgAmmoCurrent = smgAmmoMax;
        rifleAmmoCurrent = rifleAmmoMax;
        inventory[0] = startingWeapon;
    }

    /// <summary>
    /// Reduce health or shield by damageToTake parameter
    /// If our health is less than or equal to 0 then kill the pawn
    /// </summary>
    /// <param name="damageToTake">Damage passed in by the projectile we were hit by</param>
    public void TakeDamage(float damageToTake)
    {
        //TODO : fill this in
    }

    void HealthUIUpdate()
    {
        if(healthFill != null)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
        }
    }
    /// <summary>
    /// switch the ammo current and max displayed based on the current weapon we have equipped
    /// </summary>
    void AmmoUIUpdate()
    {

    }
    /// <summary>
    /// Update the inventory when new items to the inventory or when actor is spawned
    /// int j holds positions of our secondary set of arrays, this is used for performance, so we don't need to do a second loop when we don't need to move x and y in a 2D array
    /// loop through inventory
    /// if the inventory slot is not null then check if current slot iteration is the active weapon
    ///     if it is the active weapon then set 0 slot on HUD to this and set the keybind to the inventory slot iteration
    ///     if the active weapon is not then set hud slot j to this image and set the keybind the the inventory slot iteration
    /// if the inventory slot is null then deactivate the slot, so we don't clutter the UI
    /// </summary>
    public void InventoryUIUpdate()
    {
        //TODO : Fill this in
    }
}
