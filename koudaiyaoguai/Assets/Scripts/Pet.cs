using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum attribute
{
    putong,
    shui,
    huo,
    cao,
    dian,
    bing,
    chong,
    youling
};

public enum studyState
{
    PhDamage,
    MaDamage,
    PhAnti,
    MaAnti
};

[CreateAssetMenu(fileName = "New Pet", menuName = "Inventory/New Pet")]
public class Pet : ScriptableObject
{
    public string petName;
    public Sprite petImage;
    public int petLevel;
    public int petExp;
    public int petEvolution;
    public List<int> petLevelUp = new List<int>();
    public int petMaxHp;
    public int petCurrentHp;
    public string petAttribute;
    public float petPhDamage;
    public float petMaDamage;
    public float petPhAnti;
    public float petMaAnti;
    public string petStudyState;
    public int petLevelUpAdd;
    public List<Skill> petSkill = new List<Skill>();
    

    [TextArea] public string petInfo;
}