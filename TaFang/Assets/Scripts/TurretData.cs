using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData
{
    public GameObject turretPrefab;
    public int cost;
    public GameObject turretUpgradePrefab;
    public int costUpgrade;
    public TurretType turretType;
}

public enum TurretType
{
    Turret1,
    Turret2,
    Turret3
}