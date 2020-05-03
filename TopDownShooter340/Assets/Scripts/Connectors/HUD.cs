using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public Image healthBar;
    public InventorySlot[] weaponSlots = new InventorySlot[3];
    public TextMeshProUGUI currentAmmoInfo;
    public TextMeshProUGUI maxAmmoInfo;
}
