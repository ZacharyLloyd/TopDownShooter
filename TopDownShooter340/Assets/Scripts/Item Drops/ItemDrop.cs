﻿
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDrop : MonoBehaviour
{

    public List<GameObject> items = new List<GameObject>();
    WeightedItemDrop weightedDrop;

    public Pawn itemSource;

    void Awake()
    {
        weightedDrop = GetComponent<WeightedItemDrop>();
        itemSource = GetComponent<Pawn>();
    }

    public void AddItem(GameObject _obj)
    {
        if (_obj != null)
            items.Add(_obj);
    }

    /// <summary>
    /// Drop items that pawn carries
    /// </summary>
    public void DropAllItems()
    {

        //If a pawn hasn't dropped anything, drop it, making sure that only one item drops
        if (!itemSource.droppedSomething)
        {
            weightedDrop.OnDrop();
            foreach (GameObject item in items)
            {
                Instantiate(item, itemSource.transform.position, Quaternion.identity);

            }
            itemSource.droppedSomething = true;
        }
    }
}
