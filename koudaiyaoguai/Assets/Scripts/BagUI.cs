using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class BagUI : MonoBehaviour
{
    public Button bagBtn;
    public Button petBagBtn;
    public PlayerData playerData;
    public GameObject bagGrid;
    public ItemOnBag itemPrefab;
    public Text itemInformation;

    private GameObject bag;
    private GameObject petBag;
    private string openName;
    private static BagUI instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;

        bag = transform.Find("Bag").gameObject;
        petBag = transform.Find("PetBag").gameObject;
    }

    private void Start()
    {
        petBagBtn.onClick.AddListener(OpenPetBag);
        bagBtn.onClick.AddListener(OpenBag);
    }

    public static void UpdateItemInfo(string itemInfo)
    {
        instance.itemInformation.text = itemInfo;
    }
    public static void CreateNewItem(Item item)
    {
        ItemOnBag newItem = Instantiate(instance.itemPrefab, instance.bagGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.bagGrid.transform);
        newItem.item = item;
        newItem.image.sprite = item.itemImage;
        newItem.count.text = item.itemCount.ToString();
    }

    public static void RefreshItem()
    {
        for (int i = 0; i < instance.bagGrid.transform.childCount; i++)
        {
            if (instance.bagGrid.transform.childCount == 0)
                break;
            Destroy(instance.bagGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < instance.playerData.bag.Count; i++)
        {
            CreateNewItem(instance.playerData.bag[i]);
        }
    }

    private void OpenPetBag()
    {
        if (openName != null && openName != petBag.name)
        {
            transform.Find(openName).gameObject.SetActive(false);
        }

        if (petBag.activeSelf)
        {
            openName = null;
        }
        else
        {
            openName = petBag.name;
        }

        petBag.SetActive(!petBag.activeSelf);
    }

    private void OpenBag()
    {
        if (openName != null && openName != bag.name)
        {
            transform.Find(openName).gameObject.SetActive(false);
        }

        if (bag.activeSelf)
        {
            openName = null;
        }
        else
        {
            openName = bag.name;
        }

        bag.SetActive(!bag.activeSelf);
    }
}