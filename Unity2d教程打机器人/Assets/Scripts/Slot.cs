using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slot",menuName = "Inventory/New Slot")]
public class Slot : ScriptableObject
{
    public String name;
    public int level;
    public int exp;
}
