using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemOnBag : MonoBehaviour
{
    public Item item;
    public Image image;
    public Text count;

    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(ItemOnClicked);
    }

    public void ItemOnClicked()
    {
        BagUI.UpdateItemInfo(item.itemInfo);
    }
}