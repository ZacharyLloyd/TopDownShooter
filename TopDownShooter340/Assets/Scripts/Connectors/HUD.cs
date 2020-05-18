using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public Image healthBar;
    public InventorySlot[] inventorySlots = new InventorySlot[3];
    public Text currentAmmoInfo;
    public Stats stats;
    private void Start()
    {
        GetInventorySlots();
    }
    void GetInventorySlots()
    {

        inventorySlots = GetComponentsInChildren<InventorySlot>();

    }

}
