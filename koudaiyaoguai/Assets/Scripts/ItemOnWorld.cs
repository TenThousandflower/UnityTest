using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item item;
    public PlayerData playerData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddNewItem();
        }
    }

    private void AddNewItem()
    {
        if (!playerData.bag.Contains(item))
        {
            playerData.bag.Add(item);
            item.itemCount += 1;
            BagUI.CreateNewItem(item);
        }
        else
        {
            item.itemCount += 1;
            BagUI.RefreshItem();
        }

        Destroy(gameObject);
    }
}