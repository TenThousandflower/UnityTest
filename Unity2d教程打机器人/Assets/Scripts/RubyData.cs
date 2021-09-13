using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RubyData",menuName = "Inventory/New RubyData")]
public class RubyData : ScriptableObject
{
    public String name;
    public List<Slot> slots = new List<Slot>();
}
