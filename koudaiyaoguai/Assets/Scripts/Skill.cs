using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Inventory/New Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public Sprite skillPrefab;
    public string skillAttribute;
    public float skillPhDamage;
    public float skillMaDamage;
    public float skillHeal;
    public float skillPhAnti;
    public float skillMaAnti;
    [TextArea] public string skillInfo;
}