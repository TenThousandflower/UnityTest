using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Inventory/New PlayerData")]
public class PlayerData : ScriptableObject
{
    public string name;
    public List<Item> bag = new List<Item>();
    public List<Pet> petBag = new List<Pet>();
    public GameObject followPet;
}