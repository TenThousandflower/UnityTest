using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataList
{
    public static string[] Players = {"p1", "p2", "p3", "p4"};
    public static string[] Pets = {"pet1", "pet2"};
    public static string[] Appearances = {"ap1", "ap2", "ap3"};
    public static string[] Bombs = {"b1", "b2", "b3", "b4"};

    public enum States
    {
        speed,
        bombNum,
        bombPower,
    }
    
    public static List<int[]> GameScene01 = new List<int[]>
    {
        new int[] {1, 2},
        
    };
}