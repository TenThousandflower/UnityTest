using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//保存每一波敌人生成所需要的属性
[System.Serializable] //可以被序列化，显示在unity GameObject属性中
public class Wave
{
    public GameObject enemyPrefab;
    public int count;
    public float rate;
}
