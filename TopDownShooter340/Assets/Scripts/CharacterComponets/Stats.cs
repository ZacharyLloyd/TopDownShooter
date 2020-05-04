using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stats : MonoBehaviour
{
    public Pawn pawn;

    [Header("Health")]
    [SerializeField, Range(0f, 100f), Tooltip("Current health")] public float currentHealth;
    [SerializeField, Range(0f, 100f), Tooltip("Current max health")] private float maxHealth = 100f;
    public Slider healthFill;

    [Header("AmmoDisplay")]
    public TextMeshProUGUI currentAmmoText;
    public TextMeshProUGUI maxAmmoText;

    [Header("Weapon")]
    public Weapon startingWeapon;
    public Weapon weaponEquipped;
    public Weapon[] inventory = new Weapon[3];

    [Header("Pistol")]
    public int pistolAmmoMax;
    public int pistolAmmoCurrent;
    public Transform pistolSpawnPoint;

    [Header("SMG")]
    public int smgAmmoMax;
    public int smgAmmoCurrent;
    public Transform smgSpawnPoint;

    [Header("Rifle")]
    public int rifleAmmoMax;
    public int rifleAmmoCurrent;
    public Transform rifleSpawnPoint;

    private void Awake()
    {
        pawn = GetComponentInChildren<Pawn>();
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
        currentHealth -= damageToTake;
        HealthUIUpdate();
        if (currentHealth == 0)
        {
            pawn.isDead = true;
            Destroy(gameObject);
        }
    }
    //Filling the health ui to match the players health
    void HealthUIUpdate()
    {
        if (healthFill != null)
        {
            //fill amount is equal to current health
            healthFill.value = currentHealth / maxHealth;
        }
    }
    /// <summary>
    /// switch the ammo current and max displayed based on the current weapon we have equipped
    /// </summary>
    void AmmoUIUpdate()
    {
        if (currentAmmoText != null && maxAmmoText != null)
        {
            //figure out what weapon is equipped and display text based off that
            if (weaponEquipped.currentWeaponType == Weapon.weaponType.pistol)
            {
                //currentammotext being switched to pistol ammo count
                currentAmmoText.text = pistolAmmoCurrent.ToString();
                maxAmmoText.text = pistolAmmoMax.ToString();
            }
            if (weaponEquipped.currentWeaponType == Weapon.weaponType.smg)
            {
                //currentammotext being switched to smg ammo count
                currentAmmoText.text = smgAmmoCurrent.ToString();
                maxAmmoText.text = smgAmmoMax.ToString();
            }
            if (weaponEquipped.currentWeaponType == Weapon.weaponType.rifle)
            {
                //currentammotext being switched to rifle ammo count
                currentAmmoText.text = rifleAmmoCurrent.ToString();
                maxAmmoText.text = rifleAmmoMax.ToString();
            }
        }
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
    public void inventoryUIUpdate()
    {
        ////HUD is what is used to display weapons in inventory
        //HUD display = GameManager.instance.headsUpDisplay;
        //int j = 1;
        ////Cycle through inventory and display ui accordingly
        //for (int i = 0; i < inventory.Length; i++)
        //{
        //    if (inventory[i] != null)
        //    {
        //        if (weaponEquipped != null)
        //        {
        //            if (weaponEquipped.GetType() == inventory[i].GetType())
        //            {
        //                display.weaponSlots[0].slotImage.sprite = weaponEquipped.weaponSprite;
        //                display.weaponSlots[0].keybindNumber.text = (i + 1).ToString();
        //                display.weaponSlots[0].gameObject.SetActive(true);
        //            }
        //            else
        //            {
        //                display.weaponSlots[j].slotImage.sprite = inventory[i].weaponSprite;
        //                display.weaponSlots[j].keybindNumber.text = (i + 1).ToString();
        //                display.weaponSlots[j].gameObject.SetActive(true);
        //                j++;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        display.weaponSlots[i].gameObject.SetActive(false);
        //    }
        //}
    }
}
